using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Level_Manager : MonoBehaviour 
{
    [SerializeField] private UpgradeEventManager upgradeManager;

    [SerializeField] Game_Manager gameManager;

    public event EventHandler CreateBridgeSectionDuringIntro;

    public GameObject Extension = null;

    public Transform playerTransform = null;

    public PlayerHealthSystem playerHealth;

    [SerializeField] CityHealthSystem cityHealthSystem;

    public Transform respawn;

    private List<Transform> extensions = new List<Transform>();

    public bool Win = GlobalSettings.globalPlayerWin;
    public bool Lose = GlobalSettings.globalPlayerLose;
    static bool isSubscribed;

    private void Awake()
    {
        if (isSubscribed) return;

        SceneManager.activeSceneChanged += OnSceneChanged;
        isSubscribed = true;
    }

    private void Update()
    {
        if (gameManager.hasHitEndWaveTrigger)
        {
            ResetLevel(gameManager);
        }
        if (WaveUI.FinalWaveConlcuded)
        {
            Win = true;
            CheckWinClause();
            WaveUI.FinalWaveConlcuded = false;
        }
    }

    #region Upgrade event executions

    private void Start()
    {
        upgradeManager.UpdateUpgradeHealth += UpgradeEventManager_UpdateUpgradeHealth;
        upgradeManager.UpdateUpgradeMagic += UpgradeEventManager_UpdateUpgradeMagic;
        upgradeManager.UpdateUpgradeRange += UpgradeEventManager_UpdateUpgradeRange;
        upgradeManager.UpdateUpgradeCityHealth += UpgradeEventManager_UpdateUpgradeCityHealth;
        upgradeManager.UpdateUpgradeBlock += UpgradeEventManager_UpdateUpgradeBlock;
        upgradeManager.UpdateUpgradeSideToSide += UpgradeEventManager_UpdateUpgradeSideToSide;
    }

    private void UpgradeEventManager_UpdateUpgradeBlock(object sender, UpgradeEventArgs e)
    {
        PlayerAttack blocker = FindObjectOfType<PlayerAttack>(true);
        ItemDisplay blockUpgrade = e.Item;
        if (blocker != null) { blocker.UpgradeBlocker(blockUpgrade.Modifer); }
    }

    private void UpgradeEventManager_UpdateUpgradeHealth(object sender, UpgradeEventArgs e)
    {
        PlayerHealthSystem healthSystem = FindObjectOfType<PlayerHealthSystem>(true);
        ItemDisplay healthPotion = e.Item;
        if (healthSystem != null) { healthSystem.HpEvent(healthPotion); }
    }

    private void UpgradeEventManager_UpdateUpgradeMagic(object sender, UpgradeEventArgs e)
    {
        MagicManager magicManager = FindObjectOfType<MagicManager>(true);
        ItemDisplay magicUpgrade = e.Item;
        if (magicManager != null) { magicManager.LevelUpMagic(magicUpgrade.Modifer); }
    }

    private void UpgradeEventManager_UpdateUpgradeCityHealth(object sender, UpgradeEventArgs e)
    {
        CityHealthSystem cityHP = FindObjectOfType<CityHealthSystem>(true);
        ItemDisplay cityHealthUpgrade = e.Item;
        if (cityHP != null) { cityHP.UpgradeCityHealth(cityHealthUpgrade.Modifer); }
    }

    private void UpgradeEventManager_UpdateUpgradeRange(object sender, UpgradeEventArgs e)
    { 
        WeaponDestroyerRange range = FindObjectOfType<WeaponDestroyerRange>(true);
        ItemDisplay rangeUpgrade = e.Item;
        if (range != null) { range.RangeUp();}
    }

    private void UpgradeEventManager_UpdateUpgradeSideToSide(object sender, UpgradeEventArgs e)
    {
        PlayerSideToSide sideToSide = FindObjectOfType<PlayerSideToSide>(true);
        ItemDisplay sideToSideUpgrade = e.Item;
        if (sideToSide != null) { sideToSide.UpgradeMovement(sideToSideUpgrade.Modifer); }
    }

    private void OnDisable()
    {
        //LevelManager is part of a singleton pattern and thus shouldn't ever be disabled, this shouldn't execute.
        //In the event it does execute this will prevent memory leaks
        upgradeManager.UpdateUpgradeHealth -= UpgradeEventManager_UpdateUpgradeHealth;
        upgradeManager.UpdateUpgradeMagic -= UpgradeEventManager_UpdateUpgradeMagic;
        upgradeManager.UpdateUpgradeRange -= UpgradeEventManager_UpdateUpgradeRange;
        upgradeManager.UpdateUpgradeCityHealth -= UpgradeEventManager_UpdateUpgradeCityHealth;
        upgradeManager.UpdateUpgradeBlock -= UpgradeEventManager_UpdateUpgradeBlock;
        upgradeManager.UpdateUpgradeSideToSide -= UpgradeEventManager_UpdateUpgradeSideToSide;
    }

    #endregion

    #region SceneCalls

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadLevel_1()
    {
        SceneManager.LoadScene("Level_1");
    }

    #endregion

    #region Win-Conditionals

    public void CheckWinClause()
    {
        if (Singleton.instance != null)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            {
                if (Win && currentScene.name == "Level_1")
                {
                    gameManager.GameWinTrigger();
                }
                else if (Lose && currentScene.name == "Level_1")
                {
                    gameManager.GameOverTrigger();
                }
            }
        }
    }

    #endregion

    #region Scene Prep

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        WaveUI.FinalWaveConlcuded = false;
        ResetCurrency();
        PrepareScene(previousScene, newScene);
    }

    private void PrepareScene(Scene previousScene, Scene newScene)
    {
        switch (newScene.name)
        {
            case "Level_1":
                SetupGameplayScene();
                break;
            case "MainMenu":
                SetupMainMenu();
                break;
            default:
                Debug.LogWarning($"No specific setup for scene: {newScene.name}");
                break;
        }
    }

    private void SetupGameplayScene()
    {
        ResetAllUpgrades();
        ResetWave();
        GlobalSettings.globalPauseOverride = false;
        GlobalSettings.projectileSpawnerActive = true;
    }

    private void SetupMainMenu()
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
        gameManager.EnableGameplayCamera(false);
    }

    #endregion

    #region Resets

    public void ResetLevel(Game_Manager gameManager) 
    {
        CreateBridgeSectionDuringIntro?.Invoke(this, EventArgs.Empty);
        ResetPlayerPosition();
        ResetWave();
        ResetCity();
        gameManager.hasHitEndWaveTrigger = false;
    }

    public void ResetPlayerPosition()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        GameObject Respawn = GameObject.FindWithTag("Respawn");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
            respawn = Respawn.transform;
            playerTransform.position = respawn.position;
            playerHealth.ResetPlayerHealth();
        }
    }

    public void ResetCity()
    {
        if (cityHealthSystem != null)
        {
            cityHealthSystem.ResetCity();
        }
    }

    public void ResetWave()
    {
        WaveUI waveUI = FindObjectOfType<WaveUI>();
        EnemyProjectileManager projManager = FindObjectOfType<EnemyProjectileManager>();

        if (waveUI != null)
        {
            if (projManager != null)
            {
                projManager.DestroyAllProjectiles();
                waveUI.RestartWave();
            }
        }
        WaveUI.FinalWaveConlcuded = false;
    }

    public void ResetAllUpgrades()
    {
        MagicManager magicManager = FindObjectOfType<MagicManager>(true);
        WeaponDestroyerRange range = FindObjectOfType<WeaponDestroyerRange>(true);
        ItemPurchaseCountReset itemReseter = FindObjectOfType<ItemPurchaseCountReset>(true);
        if (itemReseter != null) { itemReseter.ResetItemPurchaseCount(); }
        if (magicManager != null) { magicManager.ResetMagicUpgrade(); }
        if (range != null) { range.ResetRange(); }

        upgradeManager.upgradesHaveBeenReset = true;

        cityHealthSystem.ResetCityHealthUpgrade();

        playerHealth.ResetPlayerHealthUpgrade();
    }

    public void ResetCurrency()
    {
        GlobalSettings.globalDrachma = 0;
    }

    #endregion
}