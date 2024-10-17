using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour // This used to inherit from UI_Manager for unknown reasons, change back if this causes issues
{
    public GameObject Player;
    public Transform projectileTarget;

    public bool Win;
    public bool Lose;
    static bool isSubscribed;

    private void Awake()
    {
        if (isSubscribed) return;

        SceneManager.activeSceneChanged += OnSceneChanged;
        isSubscribed = true;
    }

    private void Update()
    {
        CheckWinClause();
    }
    #region SceneCalls
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadGamePlay1()
    {
        SceneManager.LoadScene("GamePlay1");
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

    public void CheckWinClause()
    {
        if (Singleton.instance != null)
        {
            Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
            if (gameManager != null)
            {
                if (Win)
                    gameManager.GameWinTrigger();

                else if (Lose)
                    gameManager.GameOverTrigger();
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

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        PrepareScene(newScene);
    }

    private void PrepareScene(Scene scene)
    {
        switch (scene.name)
        {
            case "GamePlay1":
                SetupGameplayScene();
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
    private void SetupGameplayScene()
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
        gameManager.Paused = false;
        Game_Manager.ChangeCamera(true);
    }

    private void SetupMainMenu()
    {
        Game_Manager.ChangeCamera(false);
    }

    private void SetupGameWin()
    {
    }

    private void SetupGameOver()
    {
        // Logic for handling what happens in the Game Lose scene
        // example, activate lose UI, display scores, play defeat animations, etc.
        Game_Manager.ChangeCamera(false); // Ensure menu camera is active
    }
}