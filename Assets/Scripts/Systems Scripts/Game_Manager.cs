using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private UI_Manager ui_Manager;
    [SerializeField] private Level_Manager level_Manager;

    public GameObject playerCamera;
    public GameObject menuCamera;
    [SerializeField] private GameObject Player;

    public bool Paused;

    public enum GameState { MainMenu, GamePlay1, GameOver, GameWin }
    public GameState gameState;

    public delegate void GameStateChange();
    public static event GameStateChange OnMainMenu;
    public static event GameStateChange OnGamePlay1;
    public static event GameStateChange OnGameOver;
    public static event GameStateChange OnGameWin;

    private void Awake() // Awake runs before start and again when scenes change.
    {
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
                ResumeGameTrigger();
            }
        }

        if (gameState == GameState.GamePlay1 && !Paused)
        {
            Cursor.visible = false;
        }
        else if (gameState != GameState.GamePlay1 || Paused) { Cursor.visible = true; }

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
        if (gameState != GameState.GamePlay1)
        {
            gameState = GameState.GamePlay1;
            ChangeGameState(gameState);
        }
        else if (gameState == GameState.GamePlay1) // if we are jumping back into the same game 
        {
            ResumeGameTrigger();
        }
    }

    public void PauseTrigger()
    {
        EnemyProjectileManager spawner = FindObjectOfType<EnemyProjectileManager>();
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
        EnemyProjectileManager spawner = FindObjectOfType<EnemyProjectileManager>();

        if (gameState == GameState.MainMenu) 
        {
            ReloadScene();
        }
        else if (gameState == GameState.GamePlay1)
        { 
            IsMenuOpen(false);
            ui_Manager.GamePlayUI();
            ChangeCamera(true);
            GlobalSettings.projectileSpawnerActive = true;
            Paused = false;
        }
    }

    public void OptionsTrigger()
    {
        ui_Manager.OptionsUI();
        IsMenuOpen(true);
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
    public void ReloadScene() 
    {
        Paused = false;
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
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();

        if (isGameplayCameraOpen)
        {
            gameManager.menuCamera.SetActive(false);
            gameManager.playerCamera.SetActive(true);
        }
        else if (gameManager.menuCamera.activeSelf)
        {
            gameManager.menuCamera.SetActive(false);
            gameManager.playerCamera.SetActive(true);
        }
        else if (gameManager.playerCamera.activeSelf)
        {
            gameManager.playerCamera.SetActive(false);
            gameManager.menuCamera.SetActive(true);
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