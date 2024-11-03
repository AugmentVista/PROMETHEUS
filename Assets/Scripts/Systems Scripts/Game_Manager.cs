using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private UI_Manager ui_Manager;
    [SerializeField] private Level_Manager level_Manager;
    [SerializeField] private GameObject meteorVFX;

    public GameObject userInterfaceCamera;
    //public GameObject gameplayCamera;
    //public GameObject menuCamera;
    //public GameObject CameraHolder;

    public bool Paused;

    public enum GameState { MainMenu, Level1, GameOver, GameWin, DoNothing, Upgrades, Results } 
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

    private void Awake() // Awake runs before start and again when scenes change.
    {

    }

    private void Start()
    {
        gameState = GameState.DoNothing;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (gameState == GameState.Level1))
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

        if (gameState == GameState.Level1 && !Paused)
        {
            Cursor.visible = false;
        }
        else if (gameState != GameState.Level1 || Paused) { Cursor.visible = true; }

    }
    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.DoNothing:
                Default();
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
            default:
                Default();
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
    }

    public void UpgradesMenuTrigger()
    { 
        gameState = GameState.Upgrades;
        ChangeGameState(gameState);
    }

    public void StartGameTrigger()
    {
        if ( gameState != GameState.Level1 )
        {
            gameState = GameState.Level1;
            ChangeGameState(gameState);
        }
        else if (gameState == GameState.Level1) // if we are jumping back into the same game 
        {
            ResumeGameTrigger();
        }
    }

    public void OptionsTrigger()
    {
        ui_Manager.OptionsUI();
        IsMenuOpen(true);
    }

    public void IntroductionMenuTrigger()
    {
        OnIntroduction?.Invoke();
        IsMenuOpen(true);
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
        Paused = true;
    }

    public void ResumeGameTrigger()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        ResumeGame(thisScene);
    }

    private void ResumeGame(Scene scene)
    {
        if (scene.name != "Level_1") 
        {
            Debug.LogError("Did Resume run?");
            ReloadScene();
            IsMenuOpen(true);
        }
        else if (scene.name == "Level_1")
        {
            IsMenuOpen(false);
            ui_Manager.GamePlayUI();
            //OnLevel1?.Invoke();
            //EnableGameplayCamera(true);
            GlobalSettings.projectileSpawnerActive = true;
            Paused = false;
        }
    }

    public void ReloadScene()
    {
        Paused = false;
        Scene currentScene = SceneManager.GetActiveScene();
        switch (currentScene.name)
        {
            case "MainMenu":
                MainMenu();
                break;
            case "Level_1":
                Level_1();
                break;
            case "GameWin":
                GameWin();
                break;
            case "GameOver":
                GameOver();
                break;
            default:
                MainMenu();
                break;
        }
    }

#endregion
    

    public void GameQuit()
    {
        Application.Quit();
    }

    private void IsMenuOpen(bool open)
    {
        EnableGameplayCamera(!open);
        //// If a menu is open and the menu camera is turned off, turn it on and turn off the interface cam.
        //if (!menuCamera.activeSelf && open) 
        //{
        //    EnableGameplayCamera(open);
        //}
        ////if a menu is open and the menu camera is turned on, return
        //else if (menuCamera.activeSelf && open)
        //{
        //    EnableGameplayCamera(open);
        //}
        //// If a menu isn't open and the interface cam is turned off, turn it on and turn off the menu camera.
        //else if (!userInterfaceCamera.activeSelf && !open)
        //{
        //    EnableGameplayCamera(open);
        //}
        //// if a menu isn't open and the interface cam is turned on return
        //else if (userInterfaceCamera.activeSelf && !open) 
        //{
        //    EnableGameplayCamera(open);
        //}
    }

    // Swaps between cameras
    public void EnableGameplayCamera(bool shouldGamePlayCamOpen)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Level_1" && !Paused)
        {
            userInterfaceCamera.SetActive(false);
        }
        else 
        { 
            userInterfaceCamera.SetActive(true); 
            meteorVFX.SetActive(!shouldGamePlayCamOpen);
        }



        //if (menuCamera != null && userInterfaceCamera != null || menuCamera != null && gameplayCamera != null)
        //{
        //    if (currentScene.name == "Level_1" && !Paused)
        //    {
        //        gameplayCamera.SetActive(true);

        //        if (userInterfaceCamera.activeSelf) { userInterfaceCamera.SetActive(false); }
        //        if (menuCamera.activeSelf) { menuCamera.SetActive(false); }
        //    }
        //    else
        //    {
        //        if (gameplayCamera.activeSelf) { gameplayCamera.SetActive(false); } // deals with case that gamePlayCam is still on

        //        if (shouldGamePlayCamOpen)
        //        {
        //            userInterfaceCamera.SetActive(true);
        //            if (menuCamera.activeSelf) { menuCamera.SetActive(false); }
        //        }
        //        else if (!shouldGamePlayCamOpen)
        //        {
        //            if (userInterfaceCamera.activeSelf) { userInterfaceCamera.SetActive(false); }
        //            menuCamera.SetActive(true);
        //        }
        //    }
        //}
    }

    #endregion



    private void Default()
    {

    }

    private void UpgradesMenu()
    {
        IsMenuOpen(true);
        OnUpgrades?.Invoke();
    }

    private void Introduction()
    {

        IsMenuOpen(true);
        OnIntroduction?.Invoke();
    }

    private void ResultsMenu()
    { 
        IsMenuOpen (true);
        OnResults?.Invoke();
    }

    private void MainMenu()
    {
       
        IsMenuOpen(true);
        level_Manager.LoadMainMenu();
        OnMainMenu?.Invoke();
    }

    private void Level_1()
    {
       
        IsMenuOpen(false);
        level_Manager.LoadLevel_1();
        OnLevel1?.Invoke();
    }
    
    private void GameOver()
    {
       
        IsMenuOpen(true);
        level_Manager.LoadGameOver();
        OnGameOver?.Invoke();
    }

    private void GameWin()
    {
       
        IsMenuOpen(true);
        level_Manager.LoadGameWin();
        OnGameWin?.Invoke();
    }

}