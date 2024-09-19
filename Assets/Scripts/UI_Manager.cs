using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject gamePlayUI;
    public GameObject optionsUI;
    public GameObject pausedUI;
    public GameObject gameOverUI;
    public GameObject gameWinUI;

    void Start()
    {
        UpdateUI();
        Game_Manager.OnMainMenu += MainMenuUI;
        Game_Manager.OnGamePlay1 += GamePlayUI;
        Game_Manager.OnGameOver += GameWinUI;
        Game_Manager.OnGameWin += GameWinUI;
    }

    private void OnDestroy()
    {
        Game_Manager.OnMainMenu -= MainMenuUI;
        Game_Manager.OnGamePlay1 -= GamePlayUI;
        Game_Manager.OnGameOver -= GameWinUI;
        Game_Manager.OnGameWin -= GameWinUI;
    }

    private void UpdateUI()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        switch (currentScene.name)
        {
            case "MainMenu":
                MainMenuUI();
                break;
            case "GamePlay1":
                GamePlayUI();
                break;
            case "GameWin":
                GameWinUI();
                break;
            case "GameLose":
                GameWinUI();
                break;
            default:
                MainMenuUI();
                break;
        }
    }
    private void MainMenuUI()
    {
        HideAllUI(mainMenuUI);
        //mainMenuUI.SetActive(true);
    }
    public void GamePlayUI()
    {
        HideAllUI(gamePlayUI);
        //gamePlayUI.SetActive(true);
    }
    public void OptionsUI()
    {
        HideAllUI(optionsUI);
        //optionsUI.SetActive(true);
    }
    protected void GameWinUI()
    {
        HideAllUI(gameWinUI);
        //gameWinUI.SetActive(true);
    }
    protected void GameOverUI()
    {
        HideAllUI(gameOverUI);
        //gameOverUI.SetActive(true);
    }
    public void PausedUI()
    {
        HideAllUI(pausedUI); // GameObject.SetActive(true);
        //pausedUI.SetActive(true);
    }
    public void HideAllUI(GameObject ActiveUI)
    {
        mainMenuUI.SetActive(false);
        gamePlayUI.SetActive(false);
        optionsUI.SetActive(false);
        pausedUI.SetActive(false);
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
        ActiveUI.SetActive(true);
    }
}