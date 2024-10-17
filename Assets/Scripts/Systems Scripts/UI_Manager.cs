using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public TimerController Timer;

    [SerializeField] private Level_Manager levelManager;

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

        Timer = new TimerController();
        levelManager = new Level_Manager();
    }

    private void Update()
    {
        if (Timer != null)
        {
            if (Timer.fillImage.fillAmount < 0.05f)
            {
                levelManager.Win = false;
            }
        }
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
            case "GameOver":
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
    }
    public void GamePlayUI()
    {
        HideAllUI(gamePlayUI);
    }
    public void OptionsUI()
    {
        HideAllUI(optionsUI);
    }
    protected void GameWinUI()
    {
        HideAllUI(gameWinUI);
    }
    protected void GameOverUI()
    {
        HideAllUI(gameOverUI);
    }
    public void PausedUI()
    {
        HideAllUI(pausedUI);
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