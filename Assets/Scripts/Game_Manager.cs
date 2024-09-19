using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private UI_Manager ui_Manager;
    [SerializeField] private Level_Manager level_Manager;

    private static GameObject playerCamera;
    private static GameObject menuCamera;
    [SerializeField] private GameObject Player;

    public bool Paused;

    //private bool controlsActive = false;
    //private bool pauseControlsActive = false;
    //private bool inventoryActive;

    //public GameObject ShowControls;
    //public GameObject ShowPauseControls;
    //public GameObject Inventory;

    public enum GameState { MainMenu, GamePlay1, Options, GameOver, GameWin }
    public GameState gameState;

    public delegate void GameStateChange();
    public static event GameStateChange OnMainMenu;
    public static event GameStateChange OnGamePlay1;
    public static event GameStateChange OnGamePlay2;
    public static event GameStateChange OnGameOver;
    public static event GameStateChange OnGameWin;

    private void Awake() // Awake runs before start and again when scenes change.
    {
        playerCamera = GameObject.Find("PlayerCamera");
        menuCamera = GameObject.Find("MenuCamera");

        if (playerCamera == null || menuCamera == null)
        {
            Debug.LogError("Camera references not found!");
        }
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
        IsMenuOpen(true);
        ui_Manager.PausedUI();
        Time.timeScale = 0.0f;
        Paused = true;
    }

    public void ResumeGame()
    {
        IsMenuOpen(false);
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

    private bool IsMenuOpen(bool open)
    {
        // If a menu is open and the menu camera is turned off, turn it on and turn off the player camera.
        if (!menuCamera.activeSelf && open)
        {
            ChangeCamera();
            return true;
        }
        // If a menu isn't open and the player camera is turned off, turn it on and turn off the menu camera.
        else if (!playerCamera.activeSelf && !open)
        {
            ChangeCamera();
            return false;
        }

        return false; // No change needed
    }

    // Swaps between player camera and menu camera when a menu is opened
    public static void ChangeCamera(bool isGameplayCameraOpen = false)
    {
        if (isGameplayCameraOpen)
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
            playerCamera.SetActive(false);
            menuCamera.SetActive(true);
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
        IsMenuOpen(true);
    }

    private void GamePlay1()
    {
        level_Manager.LoadGamePlay1();
        IsMenuOpen(false);
        OnGamePlay1?.Invoke();
    }
    #endregion
    private void Options()
    {
        ui_Manager.OptionsUI();
        IsMenuOpen(true);
    }

    private void GameOver()
    {
        OnGameOver?.Invoke();
        IsMenuOpen(true);
    }

    private void GameWin()
    {
        OnGameWin?.Invoke();
        IsMenuOpen(true);
    }
    #endregion
}