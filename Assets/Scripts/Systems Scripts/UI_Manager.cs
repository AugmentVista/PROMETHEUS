using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    //[SerializeField] private Level_Manager levelManager;

    [SerializeField] private ResultsKeeper resultsKeeper;


    public GameObject emptyUI;
    public GameObject introductionUI;
    public GameObject mainMenuUI;
    public GameObject gamePlayUI;
    public GameObject optionsUI;
    public GameObject pausedUI;
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public GameObject upgradesUI;
    public GameObject resultsUI;

    void Start()
    {
        UpdateUI();
        Game_Manager.OnDoNothing += NoUI;
        Game_Manager.OnResults += ResultsUI;
        Game_Manager.OnIntroduction += IntroductionUI;
        Game_Manager.OnUpgrades += UpgradesUI;
        Game_Manager.OnMainMenu += MainMenuUI;
        Game_Manager.OnLevel1 += GamePlayUI;
        Game_Manager.OnGameOver += GameOverUI;
        Game_Manager.OnGameWin += GameWinUI;
    }

    private void OnDestroy()
    {
        Game_Manager.OnDoNothing -= NoUI;
        Game_Manager.OnResults -= ResultsUI;
        Game_Manager.OnIntroduction -= IntroductionUI;
        Game_Manager.OnUpgrades -= UpgradesUI;
        Game_Manager.OnMainMenu -= MainMenuUI;
        Game_Manager.OnLevel1 -= GamePlayUI;
        Game_Manager.OnGameOver -= GameOverUI;
        Game_Manager.OnGameWin -= GameWinUI;
    }

    public void UpdateUI()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        switch (currentScene.name)
        {
            case "MainMenu":
                MainMenuUI();
                break;
            case "Level_1":
                GamePlayUI();
                break;
            case "GameWin":
                GameWinUI();
                break;
            case "GameOver":
                GameOverUI();
                break;
            default:
                NoUI();
                break;
        }
    }

    private void NoUI()
    {
        HideAllUI(emptyUI);
    }
    private void ResultsUI()
    {
        HideAllUI(resultsUI);
        resultsKeeper.ResultsButton();
    }
    private void IntroductionUI()
    {
        HideAllUI(introductionUI);
    }
    private void UpgradesUI()
    {
        HideAllUI(upgradesUI);
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
        emptyUI.SetActive(false);
        resultsUI.SetActive(false);
        introductionUI.SetActive(false);
        upgradesUI.SetActive(false);
        mainMenuUI.SetActive(false);
        gamePlayUI.SetActive(false);
        optionsUI.SetActive(false);
        pausedUI.SetActive(false);
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
        ActiveUI.SetActive(true);
    }


    public void DisplayHitBoxUI()
    {
        PlayerAttackHitBox hitBox = FindAnyObjectByType<PlayerAttackHitBox>();

        if (hitBox != null)
        {
            GameObject hitBoxChild = hitBox.transform.Find("Hit Box Visual")?.gameObject;

            if (hitBoxChild != null)
            {
                hitBoxChild.SetActive(!hitBoxChild.activeSelf);
            }
        }
    }

}