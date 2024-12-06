using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private UI_Manager ui_Manager;
    [SerializeField] private Level_Manager level_Manager;
    [SerializeField] private GameObject meteorVFX;

    [SerializeField] private ResultsKeeper resultsKeeper;

    public GameObject userInterfaceCamera;
    public bool hasHitEndWaveTrigger = false;
    bool hasReadIntroduction = false;


    public bool Paused = GlobalSettings.globalPauseOverride;

    public enum GameState { MainMenu, Level1, GameOver, GameWin, DoNothing, Upgrades, Results, Introduction }
    public GameState gameState;

    public delegate void GameStateChange();
    public static event GameStateChange OnMainMenu;
    public static event GameStateChange OnLevel1;
    public static event GameStateChange OnGameOver;
    public static event GameStateChange OnGameWin;
    public static event GameStateChange OnDoNothing;
    public static event GameStateChange OnResults;
    public static event GameStateChange OnUpgrades;
    public static event GameStateChange OnIntroduction;
    private void Start()
    {
        gameState = GameState.DoNothing;
        level_Manager.CreateBridgeSectionDuringIntro += Level_Manager_CreateBridgeSectionDuringIntro;
    }

    private void Level_Manager_CreateBridgeSectionDuringIntro(object sender, EventArgs _)
    {
        GameObject bridgeScriptHolder = GameObject.Find("Bridge Script Holder");
        SpawnBridge bridge = bridgeScriptHolder.GetComponent<SpawnBridge>();
        if (bridge != null)
            bridge.CreateBridge();
        ResultsMenuTrigger();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale = 0.0f;
            }
        }


        Scene thisScene = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.Escape) && (thisScene.name == "Level_1"))
        {
            if (!Paused) // If not paused, pause the game.
            {
                PauseTrigger();
            }
            else // If already paused, resume game.
            {
                ResumeGameTrigger();
            }
        }
        if (gameState != GameState.Level1 || Paused || thisScene.name != "Level_1") 
        {
            Cursor.visible = true; 
        }
        else if (thisScene.name == "Level_1" && !Paused)
        {
            Cursor.visible = false;
        }
        Paused = GlobalSettings.globalPauseOverride;
    }
    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.DoNothing:
                Debug.Log("Nothing State");
                break;
            case GameState.Introduction:
                Introduction();
                break;
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.Upgrades:
                UpgradesMenu();
                break;
            case GameState.Level1:
                Level_1();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameWin:
                GameWin();
                break;
            case GameState.Results:
                ResultsMenu();
                break;
        }
    }

    #region Non states

    #region UI Triggers
    public void MainMenuTrigger()
    {
        gameState = GameState.MainMenu;
        ChangeGameState(gameState);
    }

    public void ResultsMenuTrigger()
    {
        gameState = GameState.Results;
        ChangeGameState(gameState);
        GlobalSettings.globalPauseOverride = true;
    }

    public void UpgradesMenuTrigger()
    {
        gameState = GameState.Upgrades;
        ChangeGameState(gameState);
    }

    public void Button_Upgrades_To_Gameplay()
    {
        gameState = GameState.Level1;
        ChangeGameState(gameState);
        level_Manager.ResetLevel(this);
        ResumeGameTrigger();
    }

    public void OptionsTrigger()
    {
        IsMenuOpen(true);
        ui_Manager.OptionsUI();
    }

    public void StartGameTrigger()
    {
        Time.timeScale = 1.0f;
        Scene thisScene = SceneManager.GetActiveScene();
        if (thisScene.name != "Level_1")
        {
            gameState = GameState.Level1;
            ChangeGameState(gameState);
        }
        else if (thisScene.name == "Level_1") // if we are jumping back into the same game 
        {
            ResumeGameTrigger();
        }
    }
   
    public void IntroductionReturn()
    {
        Introduction();

        StartCoroutine(IntroductionCoroutine());
    }

    private IEnumerator IntroductionCoroutine()
    {
        GameObject IntroPlayButton;

        IntroPlayButton = ui_Manager.introductionUI.transform.Find("Play BG").gameObject;

        if (!hasReadIntroduction)
        {
            if (IntroPlayButton != null && IntroPlayButton.activeSelf)
            {
                IntroPlayButton.SetActive(false);
            }
            float duration = 1f;

            yield return new WaitForSeconds(duration);

            IntroPlayButton.SetActive(true);
            hasReadIntroduction = true;
        }
        else 
        {
            IntroPlayButton.SetActive(true);
        }

    }

    public void GameOverTrigger()
    {
        gameState = GameState.GameOver;
        ChangeGameState(gameState);
    }

    public void GameWinTrigger()
    {
        gameState = GameState.GameWin;
        ChangeGameState(gameState);
    }

    public void PauseTrigger()
    {
        IsMenuOpen(true);
        ui_Manager.PausedUI();
        GlobalSettings.projectileSpawnerActive = false;
        GlobalSettings.globalPauseOverride = true;
    }

    public void ResumeGameTrigger()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        ResumeGame(thisScene);
    }

    private void ResumeGame(Scene scene)
    {
        GlobalSettings.projectileSpawnerActive = true;
        GlobalSettings.globalPauseOverride = false;
        if (scene.name != "Level_1") 
        {
            ReloadScene();
        }
        else if (scene.name == "Level_1")
        {
            Level_1();
        }
    }

    public void ReloadScene()
    {
        GlobalSettings.globalPauseOverride = false;
        Scene currentScene = SceneManager.GetActiveScene();
        switch (currentScene.name)
        {
            case "MainMenu":
                MainMenu();
                break;
            case "Level_1":
                level_Manager.LoadLevel_1();
                Level_1();
                break;
            default:
                MainMenu();
                break;
        }
    }

    #endregion

    private void IsMenuOpen(bool open)
    {
        if (open)
        {
            Cursor.visible = open;
            meteorVFX.SetActive(!open);
        }
        else 
        {
            Cursor.visible = open;
        }
        EnableGameplayCamera(!open);
    }

    // Swaps between cameras
    public void EnableGameplayCamera(bool shouldGamePlayCamOpen)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Level_1" && !Paused)
        {
            userInterfaceCamera.SetActive(false);
        }
        else if (currentScene.name != "Level_1")
        {
            if (userInterfaceCamera == null /*&& currentScene.name == "Main Menu")*/ )
            {
                GameObject userInterfaceCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
                if (userInterfaceCamera != null)
                { 
                    userInterfaceCamera.SetActive(true);
                }
            }
            else
            {
                userInterfaceCamera.SetActive(true);
            }
            meteorVFX.SetActive(!shouldGamePlayCamOpen);
        }
    }

    #endregion

    #region Private Invoke calls

    private void Introduction() // INVOKING DOES NOT CHANGE GAMESTATE, GAMESTATE IS MANUALLY CHANGED
    {
        IsMenuOpen(true);
        OnIntroduction?.Invoke(); // does not change gameState, only invokes the event.
    }

    private void UpgradesMenu()
    {
        IsMenuOpen(true);
        OnUpgrades?.Invoke();
    }

    private void ResultsMenu()
    { 
        IsMenuOpen (true);
        OnResults?.Invoke();
        resultsKeeper.ShowResults();
    }

    private void MainMenu()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        level_Manager.LoadMainMenu();
        IsMenuOpen(true);
        OnMainMenu?.Invoke();
    }

    private void Level_1()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "Level_1") { level_Manager.LoadLevel_1(); }
        IsMenuOpen(false);
        OnLevel1?.Invoke();
        GlobalSettings.globalPauseOverride = false;
    }

    private void GameOver()
    {
        IsMenuOpen(true);
        OnGameOver?.Invoke();
    }

    private void GameWin()
    {
        Time.timeScale = 0.0f;
        IsMenuOpen(true);
        OnGameWin?.Invoke();
    }

    #endregion
    public void GameQuit()
    {
        Application.Quit();
    }
}