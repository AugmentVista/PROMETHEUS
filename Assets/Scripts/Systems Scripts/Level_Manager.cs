using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour 
{
    [SerializeField] private UpgradeEventManager upgradeManager;

    public TimerController Timer;

    public bool Win = GlobalSettings.globalPlayerWin;
    public bool Lose = GlobalSettings.globalPlayerLose;
    static bool isSubscribed;

    private void Awake()
    {
        if (isSubscribed) return;

        SceneManager.activeSceneChanged += OnSceneChanged;
        isSubscribed = true;
    }

    private void Start()
    {
        upgradeManager.UpdateUpgradeHealth += UpgradeEventManager_UpdateUpgradeHealth;
        upgradeManager.UpdateUpgradeSprintSpeed += UpgradeEventManager_UpdateUpgradeSprintSpeed;
        upgradeManager.UpdateUpgradeAttackSpeed += UpgradeEventManager_UpdateUpgradeAttackSpeed;
        upgradeManager.UpdateUpgradeHammer += UpgradeEventManager_UpdateUpgradeHammer;
        upgradeManager.UpdateUpgradeStamina += UpgradeEventManager_UpdateUpgradeStamina;
        upgradeManager.UpdateUpgradeBlock += UpgradeEventManager_UpdateUpgradeBlock;
    }

    private void UpgradeEventManager_UpdateUpgradeBlock(object sender, UpgradeEventArgs e)
    { 
        // BLOCK WILL NOT BE AVAILABLE BY ALPHA
    }

    private void UpgradeEventManager_UpdateUpgradeHealth(object sender, UpgradeEventArgs e)
    {
        PlayerHealthSystem healthSystem = FindObjectOfType<PlayerHealthSystem>(true);
        ItemDisplay healthPotion = e.Item;
        Debug.Log($"Upgrade purchased of type {healthSystem}");
        if (healthSystem != null) { healthSystem.HpEvent(healthPotion); }
    }

    private void UpgradeEventManager_UpdateUpgradeSprintSpeed(object sender, UpgradeEventArgs e)
    {
        SprintBoost sprint = FindObjectOfType<SprintBoost>(true);
        ItemDisplay sprintUpgrade = e.Item;
        Debug.Log($"Upgrade purchased of type {sprint}");
        if (sprint != null) { sprint.IncreaseSprint(sprintUpgrade.Modifer);}
    }

    private void UpgradeEventManager_UpdateUpgradeStamina(object sender, UpgradeEventArgs e)
    {
        SprintBoost stamina = FindObjectOfType<SprintBoost>(true);
        ItemDisplay staminaUpgrade = e.Item;
        Debug.Log($"Upgrade purchased of type {stamina}");
        if (stamina != null) { stamina.IncreaseStamina(staminaUpgrade.Modifer); }
    }

    private void UpgradeEventManager_UpdateUpgradeAttackSpeed(object sender, UpgradeEventArgs e)
    { 
        PlayerAttackHitBox attackHitBox = FindObjectOfType<PlayerAttackHitBox>(true);
        ItemDisplay attackSpeedUpgrade = e.Item;
        Debug.Log($"Upgrade purchased of type {attackSpeedUpgrade}");
        if (attackHitBox != null) { attackHitBox.UpdateAttackSpeed(attackSpeedUpgrade.Modifer);}
    }

    private void UpgradeEventManager_UpdateUpgradeHammer(object sender, UpgradeEventArgs e)
    { 
        PlayerAttackHitBox hammerHitBox = FindObjectOfType<PlayerAttackHitBox>(true);
        ItemDisplay hammerUpgrade = e.Item;
        Debug.Log($"Upgrade purchased of type {hammerUpgrade}");
        if (hammerHitBox != null) { hammerHitBox.UpdateHammer(hammerUpgrade.Modifer);}
    }


    private void OnDisable()
    {
        upgradeManager.UpdateUpgradeHealth -= UpgradeEventManager_UpdateUpgradeHealth;
        upgradeManager.UpdateUpgradeSprintSpeed -= UpgradeEventManager_UpdateUpgradeSprintSpeed;
        upgradeManager.UpdateUpgradeAttackSpeed -= UpgradeEventManager_UpdateUpgradeAttackSpeed;
        upgradeManager.UpdateUpgradeHammer -= UpgradeEventManager_UpdateUpgradeHammer;
        upgradeManager.UpdateUpgradeStamina -= UpgradeEventManager_UpdateUpgradeStamina;
        upgradeManager.UpdateUpgradeBlock -= UpgradeEventManager_UpdateUpgradeBlock;
    }

    private void Update()
    {
        if (Timer != null) 
        {
            if (Timer.TimerOver)
            {
                //Debug.Log("DING DING, TIMER IS UP!");
                //Lose = true;
                //CheckWinClause();
            }
        }
        
    }

    #region SceneCalls

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadLevel_1()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void LoadGameWin()
    {
        SceneManager.LoadScene("GameWin");
    }
    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
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
                    gameManager.GameOverTrigger(); // same call as Scene_Transition
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
        PrepareScene(newScene);
    }

    private void PrepareScene(Scene scene)
    {
        switch (scene.name)
        {
            case "Level_1":
                SetupGameplayScene(scene);
                break;
            case "MainMenu":
                SetupMainMenu();
                break;
            case "GameWin":
                SetupGameWin();
                break;
            case "GameOver":
                SetupGameOver();
                break;
            default:
                Debug.LogWarning($"No specific setup for scene: {scene.name}");
                break;
        }
    }

    private void SetupGameplayScene(Scene scene)
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
        gameManager.Paused = false;
        // This doesn't work but the camera system works without it switching?
        // Only god knows these cameras work right now. The Game_Reference doesn't change but the angles are correct
        // If it ain't broke, don't touch it.
        //if (gameManager.gameplayCamera == null && scene.name == "Level_1") { gameManager.gameplayCamera = GameObject.FindGameObjectWithTag("PlayerCamera"); }
        gameManager.EnableGameplayCamera(true);
    }

    private void SetupMainMenu()
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
        gameManager.userInterfaceCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        gameManager.EnableGameplayCamera(false);
    }

    private void SetupGameWin()
    {

    }

    private void SetupGameOver()
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
        // Logic for handling what happens in the Game Lose scene
        // example, activate lose UI, display scores, play defeat animations, etc.
        gameManager.EnableGameplayCamera(false); // Ensure menu camera is active
    }
    #endregion
}