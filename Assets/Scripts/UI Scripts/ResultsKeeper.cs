using UnityEngine;
using UnityEngine.UI;

public class ResultsKeeper : MonoBehaviour
{
    [SerializeField] private ScoreKeeper scoreKeeper;

    public Image[] PointsNumberImages;

    public Image pointsOnes;
    public Image pointsTens;
    public Image pointsHundreds;

    public Image[] EarningsNumberImages;

    public Image earningsOnes;
    public Image earningsTens;
    public Image earningsHundreds;  

    public Image[] WaveNumberImages;

    public Image waveOnes;
    public Image waveTens;


    private int score => scoreKeeper != null ? scoreKeeper.score : 0;
    private int earnings => scoreKeeper != null ? scoreKeeper.drachma : 0;

    public int GetWaveInfo()
    { 
        return GlobalSettings.globalWaveCount;
    }

    public void ShowResults() 
    {
        Debug.Log("Points is " + score);
        Debug.Log("money is " + earnings);
        Debug.LogError($"wave is: {GetWaveInfo().ToString()}");
        DetermineResults(score, earnings, GetWaveInfo());
    }

    private void DetermineResults(int points, int money, int waveCount)
    {
        int pOnes = points % 10;
        int pTens = (points / 10) % 10;
        int pHundreds = (points /  100) % 10;

        int eOnes = money % 10;
        int eTens = (money / 10) % 10;
        int eHundreds = (money / 100) % 10;

        int wOnes = waveCount % 10;
        int wTens = (waveCount / 10) % 10;

        pointsOnes.sprite = PointsNumberImages[pOnes].sprite;
        pointsTens.sprite = PointsNumberImages[pTens].sprite;
        pointsHundreds.sprite = PointsNumberImages[pHundreds].sprite;

        earningsOnes.sprite = EarningsNumberImages[eOnes].sprite;
        earningsTens.sprite = EarningsNumberImages[eTens].sprite;
        earningsHundreds.sprite = EarningsNumberImages[pHundreds].sprite;

        waveOnes.sprite = WaveNumberImages[wOnes].sprite;
        waveTens.sprite = WaveNumberImages[wTens].sprite;
    }
}