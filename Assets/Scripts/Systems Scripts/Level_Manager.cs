using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Level_Manager : MonoBehaviour 
{
    [SerializeField] private UpgradeEventManager upgradeManager;

    public event EventHandler CreateBridgeSectionDuringIntro;

    public GameObject Extention = null;

    public Transform playerTransform = null;

    public PlayerHealthSystem playerHealth;

    [SerializeField] CityHealthSystem cityHealthSystem;

    public Transform respawn;

    private List<Transform> Extentions = new List<Transform>();

    public bool Win = GlobalSettings.globalPlayerWin;
    public bool Lose = GlobalSettings.globalPlayerLose;
    static bool isSubscribed;

    private void Awake()
    {
        if (isSubscribed) return;

        SceneManager.activeSceneChanged += OnSceneChanged;
        isSubscribed = true;
    }

    #region Upgrade event executions

    private void Start()
    {
        upgradeManager.UpdateUpgradeHealth += UpgradeEventManager_UpdateUpgradeHealth;
        upgradeManager.UpdateUpgradeMagic += UpgradeEventManager_UpdateUpgradeMagic;
        upgradeManager.UpdateUpgradeAttackSpeed += UpgradeEventManager_UpdateUpgradeAttackSpeed;
        upgradeManager.UpdateUpgradeHammer += UpgradeEventManager_UpdateUpgradeHammer;
        upgradeManager.UpdateUpgradeCityHealth += UpgradeEventManager_UpdateUpgradeCityHealth;
        upgradeManager.UpdateUpgradeBlock += UpgradeEventManager_UpdateUpgradeBlock;
    }

    private void UpgradeEventManager_UpdateUpgradeBlock(object sender, UpgradeEventArgs e)
    {
        Blocker blocker = FindObjectOfType<Blocker>(true);
        Debug.Log($"Found Blocker Script on {blocker.gameObject.name}");
        ItemDisplay blockUpgrade = e.Item;
        Debug.Log($"Upgrade purchased of type {blocker}");
        if (blocker != null) { blocker.UpgradeBlocker(blockUpgrade.Modifer); }
    }

    private void UpgradeEventManager_UpdateUpgradeHealth(object sender, UpgradeEventArgs e)
    {
        PlayerHealthSystem healthSystem = FindObjectOfType<PlayerHealthSystem>(true);
        ItemDisplay healthPotion = e.Item;
        Debug.Log($"Upgrade purchased of type {healthSystem}");
        if (healthSystem != null) { healthSystem.HpEvent(healthPotion); }
    }

    private void UpgradeEventManager_UpdateUpgradeMagic(object sender, UpgradeEventArgs e)
    {
        MagicManager magicManager = FindObjectOfType<MagicManager>(true);
        ItemDisplay magicUpgrade = e.Item;
        Debug.Log($"Upgrade purchased of type {magicManager}");
        if (magicManager != null) { magicManager.LevelUpMagic(magicUpgrade.Modifer); }
    }

    private void UpgradeEventManager_UpdateUpgradeCityHealth(object sender, UpgradeEventArgs e)
    {
        CityHealthSystem cityHP = FindObjectOfType<CityHealthSystem>(true);
        ItemDisplay cityHealthUpgrade = e.Item;
        Debug.Log($"Upgrade purchased of type {cityHealthUpgrade}");
        if (cityHP != null) { cityHP.UpgradeCityHealth(cityHealthUpgrade.Modifer); }
    }

    private void UpgradeEventManager_UpdateUpgradeAttackSpeed(object sender, UpgradeEventArgs e)
    { 
        PlayerAttack attackHitBox = FindObjectOfType<PlayerAttack>(true);
        ItemDisplay attackSpeedUpgrade = e.Item;
        Debug.Log($"Upgrade purchased of type {attackSpeedUpgrade}");
        //if (attackHitBox != null) { attackHitBox.UpdateAttackSpeed(attackSpeedUpgrade.Modifer);}
    }

    private void UpgradeEventManager_UpdateUpgradeHammer(object sender, UpgradeEventArgs e)
    { 
        PlayerAttack hammerHitBox = FindObjectOfType<PlayerAttack>(true);
        ItemDisplay hammerUpgrade = e.Item;
        Debug.Log($"Upgrade purchased of type {hammerUpgrade}");
        //if (hammerHitBox != null) { hammerHitBox.UpdateHammer(hammerUpgrade.Modifer);}
    }

    private void OnDisable()
    {
        upgradeManager.UpdateUpgradeHealth -= UpgradeEventManager_UpdateUpgradeHealth;
        upgradeManager.UpdateUpgradeMagic -= UpgradeEventManager_UpdateUpgradeMagic;
        upgradeManager.UpdateUpgradeAttackSpeed -= UpgradeEventManager_UpdateUpgradeAttackSpeed;
        upgradeManager.UpdateUpgradeHammer -= UpgradeEventManager_UpdateUpgradeHammer;
        upgradeManager.UpdateUpgradeCityHealth -= UpgradeEventManager_UpdateUpgradeCityHealth;
        upgradeManager.UpdateUpgradeBlock -= UpgradeEventManager_UpdateUpgradeBlock;
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
            Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
            Scene currentScene = SceneManager.GetActiveScene();
            if (gameManager != null)
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
            else
            {
                Debug.LogError("Game_Manager component not found on Singleton instance.");
            }
        }
        else
        {
            Debug.LogError("Singleton instance not found.");
        }
    }

    #endregion

    #region Scene Prep

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        WaveUI.FinalWaveConlcuded = false;
        PrepareScene(previousScene, newScene);
        ResetCurrency();
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
    }

    private void SetupMainMenu()
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
        //gameManager.userInterfaceCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
       // Debug.LogError($"The player camera is: {gameManager.userInterfaceCamera.name}");
        gameManager.EnableGameplayCamera(false);
    }

    private void SetupGameWin()
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
        gameManager.EnableGameplayCamera(false); // Ensure menu camera is active
    }

    private void SetupGameOver()
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
        gameManager.EnableGameplayCamera(false); // Ensure menu camera is active
    }
    #endregion

    private void Update()
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
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

    #region Resets

    public void ResetLevel(Game_Manager gameManager) 
    {
        Extention = GameObject.Find("Extention");
        Transform ExtentionTransform = Extention.transform;

        CreateBridgeSectionDuringIntro?.Invoke(this, EventArgs.Empty);
        foreach (Transform child in ExtentionTransform)
        {
            Extentions.Add(child.gameObject.transform);
            child.gameObject.SetActive(false);
        }
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
            Debug.Log($"Found player object: {playerTransform.name}");
        }
    }

    public void ResetCity()
    {
        if (cityHealthSystem != null)
        {
            cityHealthSystem.ResetCity();
            Debug.Log($"City has been reset: {cityHealthSystem.gameObject.name}");
        }
    }

    public void ResetWave()
    {
        WaveUI waveUI = FindObjectOfType<WaveUI>();
        EnemyProjectileManager projManager = FindObjectOfType<EnemyProjectileManager>();

        Debug.Log($"WaveUI Status: {waveUI.name}, EnemyProjectileManager status: {projManager.name}");

        if (waveUI != null)
        {
            Debug.Log($"waveUI  is not null, it is {waveUI.name}");
            if (projManager != null)
            {
                Debug.Log($"projManager  is not null, it is {projManager.name}");
                projManager.DestroyAllProjectiles();
                waveUI.RestartWave();
            }
        }
        WaveUI.FinalWaveConlcuded = false;
    }

    void ResetAllUpgrades()
    {
        MagicManager magicManager = FindObjectOfType<MagicManager>(true);
        if (magicManager != null) { magicManager.ResetMagicUpgrade(); }

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