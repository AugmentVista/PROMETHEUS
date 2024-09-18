using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour // This used to inherit from UI_Manager for unknown reasons, change back if this causes issues
{
    public GameObject Player;
    public Transform PlayerTransform;
    public bool Win;
    public bool Lose;
    //public GameObject PlayerRespawnPoint;
    static bool isSubscribed;

    private void Awake()
    {
        if (isSubscribed) return;

        SceneManager.activeSceneChanged += OnSceneChanged;
        isSubscribed = true;
    }

    #region SceneCalls
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadGamePlay1()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadGameWin()
    {
        SceneManager.LoadScene("GameWin");
    }
    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameLose");
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
                    gameManager.gameState = Game_Manager.GameState.GameWin;
                else if (Lose)
                    gameManager.gameState = Game_Manager.GameState.GameOver;
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
        if (scene.name == "GamePlay1")
        {
            SetupGameplayScene();
        }
    }
    private void SetupGameplayScene()
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
        gameManager.Paused = false;
        //PlayerRespawnPoint = GameObject.Find("Player Spawn Point");
        //PlayerTransform.position = PlayerRespawnPoint.transform.position;
        Game_Manager.ChangeCamera(true);
    }
}