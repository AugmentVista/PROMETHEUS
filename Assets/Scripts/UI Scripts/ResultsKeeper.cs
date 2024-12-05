using UnityEngine;
using UnityEngine.UI;

public class ResultsKeeper : MonoBehaviour
{
    [SerializeField] private ScoreKeeper scoreKeeper;

    public Image[] EarningsNumberImages;

    public Image earningsOnes;
    public Image earningsTens;
    public Image earningsHundreds;
    public Image earningsThousands;

    public Image[] WaveNumberImages;

    public Image waveOnes;
    public Image waveTens;
    private int earnings => scoreKeeper != null ? scoreKeeper.drachma : 0;

    public int GetWaveInfo()
    { 
        return GlobalSettings.globalWaveCount;
    }

    public void ShowResults() 
    {
        Debug.Log("money is " + earnings);
        Debug.LogError($"wave is: {GetWaveInfo().ToString()}");
    }

    private void DetermineResults(int points, int money, int waveCount)
    {
        int eOnes = money % 10;
        int eTens = (money / 10) % 10;
        int eHundreds = (money / 100) % 10;
        int eThousands = (money / 1000) % 10;

        int wOnes = waveCount % 10;
        int wTens = (waveCount / 10) % 10;

        earningsOnes.sprite = EarningsNumberImages[eOnes].sprite;
        earningsTens.sprite = EarningsNumberImages[eTens].sprite;
        earningsHundreds.sprite = EarningsNumberImages[eHundreds].sprite;
        earningsThousands.sprite = EarningsNumberImages[eThousands].sprite;

        waveOnes.sprite = WaveNumberImages[wOnes].sprite;
        waveTens.sprite = WaveNumberImages[wTens].sprite;
    }
}