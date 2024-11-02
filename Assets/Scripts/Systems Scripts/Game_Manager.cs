using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private UI_Manager ui_Manager;
    [SerializeField] private Level_Manager level_Manager;
    [SerializeField] private GameObject meteorVFX;

    public GameObject playerCamera;
    public GameObject menuCamera;

    public bool Paused;

    public enum GameState { MainMenu, Level1, GameOver, GameWin, DoNothing, Upgrades } 
    public GameState gameState;

    public delegate void GameStateChange();
    public static event GameStateChange OnMainMenu;
    public static event GameStateChange OnLevel1;
    public static event GameStateChange OnGameOver;
    public static event GameStateChange OnGameWin;
    public static event GameStateChange OnDoNothing;
    public static event GameStateChange OnUpgrades;

    private void Awake() // Awake runs before start and again when scenes change.
    {
        if (playerCamera == null || menuCamera == null)
        {
            Debug.LogError("Camera references not found!");
        }
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

    public void PauseTrigger()
    {
        IsMenuOpen(true);
        ui_Manager.PausedUI();
        GlobalSettings.projectileSpawnerActive = false;
        Time.timeScale = 0.001f;
        Paused = true;
    }

    public void ResumeGameTrigger()
    {
        Time.timeScale = 1.0f;
        Paused = false;
        ResumeGame(gameState);
    }

    private void ResumeGame(GameState state)
    {
        if (gameState == GameState.MainMenu) 
        {
            ReloadScene();
        }
        else if (gameState == GameState.Level1)
        { 
            IsMenuOpen(false);
            ui_Manager.GamePlayUI();
            EnableGameplayCamera(true);
            GlobalSettings.projectileSpawnerActive = true;
            Paused = false;
        }
    }

    public void OptionsTrigger()
    {
        ui_Manager.OptionsUI();
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

    #endregion
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
            EnableGameplayCamera();
            return true;
        }
        // If a menu isn't open and the player camera is turned off, turn it on and turn off the menu camera.
        else if (!playerCamera.activeSelf && !open)
        {
            EnableGameplayCamera();
            return false;
        }

        return false; // No change needed
    }

    // Swaps between player camera and menu camera when a menu is opened
    public void EnableGameplayCamera(bool isGameplayCameraOpen = false)
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
        meteorVFX.SetActive(!isGameplayCameraOpen);
    }

    #endregion

    #region States

    #region States that trigger scene Managers

    private void Default()
    {
        IsMenuOpen(false);
    }

    private void UpgradesMenu()
    {
        Time.timeScale = 1.0f;
        OnUpgrades?.Invoke();
        IsMenuOpen(true);
    }

    private void MainMenu()
    {
        Time.timeScale = 1.0f;
        OnMainMenu?.Invoke();
        level_Manager.LoadMainMenu();
        IsMenuOpen(true);
    }

    private void Level_1()
    {
        Time.timeScale = 1.0f;
        level_Manager.LoadLevel_1();
        IsMenuOpen(false);
        OnLevel1?.Invoke();
    }
    #endregion
    private void GameOver()
    {
        Time.timeScale = 1.0f;
        level_Manager.LoadGameOver();
        OnGameOver?.Invoke();
        IsMenuOpen(true);
    }

    private void GameWin()
    {
        Time.timeScale = 1.0f;
        level_Manager.LoadGameWin();
        OnGameWin?.Invoke();
        IsMenuOpen(true);
    }
    #endregion
}