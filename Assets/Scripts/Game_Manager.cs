using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private UI_Manager ui_Manager;
    [SerializeField] private Level_Manager level_Manager;
    public static GameObject playerCamera;
    public static GameObject menuCamera;
    public GameObject playerCameraLocal;
    public GameObject menuCameraLocal;
    [SerializeField] private GameObject Player;
    public bool Paused;
    //public GameObject ShowControls;
    //public GameObject ShowPauseControls;
    private bool controlsActive = false;
    private bool pauseControlsActive = false;
    //public GameObject Inventory;
    private bool inventoryActive;

    public enum GameState { MainMenu, GamePlay1, Options, GameOver, GameWin }
    public GameState gameState;

    public delegate void GameStateChange();
    public static event GameStateChange OnMainMenu;
    public static event GameStateChange OnGamePlay1;
    public static event GameStateChange OnGamePlay2;
    public static event GameStateChange OnGameOver;
    public static event GameStateChange OnGameWin;

    private void Awake() // Awake runs before start and again when scenes change.
                         // This reallocates both cameras to the new cameras across scenes where start would not.
    {
        playerCamera = playerCameraLocal;
        menuCamera = menuCameraLocal;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (gameState == GameState.GamePlay1))
        {
            if (!Paused) // If not paused, pause the game.
            {
                PauseTrigger();
            }
            else // If already paused, resume game.
            {
                ResumeGame();
            }
        }
    }
    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.GamePlay1:
                GamePlay1();
                break;
            case GameState.Options:
                Options();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameWin:
                GameWin();
                break;
            default:
                MainMenu();
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

    public void StartGameTrigger()
    {
        gameState = GameState.GamePlay1;
        ChangeGameState(gameState);
    }

    public void PauseTrigger()
    {
        MenuIs(true);
        ui_Manager.PausedUI();
        Time.timeScale = 0.0f;
        Paused = true;
    }

    public void ResumeGame()
    {
        MenuIs(false);
        ui_Manager.GamePlayUI();
        Time.timeScale = 1.0f;
        ChangeCamera(true);
        Paused = false;
    }

    public void OptionsTrigger()
    {
        gameState = GameState.Options;
        ChangeGameState(gameState);
    }

    //public void ShowInventoryTrigger()
    //{
    //    inventoryActive = !inventoryActive;
    //    Inventory.SetActive(inventoryActive);
    //}

    //public void ShowControlsTrigger()
    //{
    //    controlsActive = !controlsActive;
    //    ShowControls.SetActive(controlsActive);

    //    pauseControlsActive = !pauseControlsActive;
    //    ShowPauseControls.SetActive(pauseControlsActive);
    //}

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

    #endregion
    public void ReloadScene() // Loads selected scene
    {
        Scene currentScene = SceneManager.GetActiveScene();
        switch (currentScene.name)
        {
            case "MainMenu":
                MainMenu();
                break;
            case "GamePlay1":
                GamePlay1();
                break;
            default:
                MainMenu();
                break;
        }
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    private bool MenuIs(bool open)
    // If a menu is open and the menu camera is turned off it is turned on and the player camera is turned off.
    // If a menu isn't open and the player camera is turned off it is turned on and the menu camera is turned off.
    {
        if (!menuCamera.activeSelf && open)
        {
            ChangeCamera();
            return true;
        }
        else if (!playerCamera.activeSelf && !open)
        {
            ChangeCamera();
            return false;
        }
        else return false;
    }
    // Swaps between player camera and menu camera when a menu is opened
    public static void ChangeCamera(bool forceGameplayCam = false)
    {
        if (forceGameplayCam)
        {
            menuCamera.SetActive(false);
            playerCamera.SetActive(true);
        }
        else if (menuCamera.activeSelf)
        {
            menuCamera.SetActive(false);
            playerCamera.SetActive(true);
        }
        else if (playerCamera.activeSelf)
        {
            menuCamera.SetActive(true);
            playerCamera.SetActive(false);
        }
    }
    #endregion

    #region States
    #region States that trigger scene Managers
    private void MainMenu()
    {
        OnMainMenu?.Invoke();
        level_Manager.LoadMainMenu();
        Time.timeScale = 1.0f;
        MenuIs(true);
    }

    private void GamePlay1()
    {
        level_Manager.LoadGamePlay1();
        MenuIs(false);
        OnGamePlay1?.Invoke();
    }
    #endregion
    private void Options()
    {
        ui_Manager.OptionsUI();
        MenuIs(true);
    }

    private void GameOver()
    {
        OnGameOver?.Invoke();
        MenuIs(true);
    }

    private void GameWin()
    {
        OnGameWin?.Invoke();
        MenuIs(true);
    }
    #endregion
}