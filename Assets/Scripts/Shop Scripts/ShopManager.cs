using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ScoreKeeper scoreKeeper;

    #region Player Balance

    private int drachma => scoreKeeper != null ? scoreKeeper.drachma : 0;
    private int buttonAmount = 5;
    public Image[] DrachmaPositive;
    public Image[] DrachmaNegative;
    public Image drachmaOnes;
    public Image drachmaTens;
    public Image drachmaHundreds;

    public int blockerCost = 4;

    #endregion
    
    private int lastDrachma = -1;

    public void AddDrachma(int amountToAdd)
    {
        scoreKeeper.drachma += amountToAdd;
        Debug.Log(drachma);
    }

    public void SubtractDrachma(int amountToReduce)
    {
        scoreKeeper.drachma -= amountToReduce;
        Debug.Log(drachma);
    }
    public void ButtonAddMoney() { AddDrachma(buttonAmount); }
    public void ButtonRemoveMoney() { SubtractDrachma(buttonAmount); }

    void Update()
    {
        int currentDrachma = drachma;
        if (currentDrachma != lastDrachma)
        {
            SetBalance(currentDrachma);
            lastDrachma = currentDrachma;
        }
    }

    private void SetBalance(int drachma)
    {
        int absDrachma = Mathf.Abs(drachma); // Get the absolute value for digit extraction

        int ones = absDrachma % 10;
        int tens = (absDrachma / 10) % 10;
        int hundreds = (absDrachma / 100) % 10;

        if (drachma >= 0)
        {
            // Display positive sprites
            drachmaOnes.sprite = DrachmaPositive[ones].sprite;
            drachmaTens.sprite = DrachmaPositive[tens].sprite;
            drachmaHundreds.sprite = DrachmaPositive[hundreds].sprite;
        }
        else
        {
            // Display negative sprites
            drachmaOnes.sprite = DrachmaNegative[ones].sprite;
            drachmaTens.sprite = DrachmaNegative[tens].sprite;
            drachmaHundreds.sprite = DrachmaNegative[hundreds].sprite;
        }
    }

    public bool CanPlayerAffordThis(TMP_Text priceText)
    {
        string priceString = priceText.text;

        if (int.TryParse(priceString, out int price))
        {
            if (drachma - price >= 0)
            {
                Debug.Log("Player can Afford this item");
                SubtractDrachma(price);
                return true;
            }
            else
            {
                Debug.Log("Player can't afford that item");
                return false;
            }
        }
        else
        {
            Debug.Log($"Price cannot be converted to an int, price is {priceString}");
            return false;
        }
    }

    public bool CanPlayerAffordBlocker()
    {
        if (GlobalSettings.globalScore >= blockerCost)
        {
            SubtractScore(blockerCost);
            Debug.Log($"Blocker spawned! Remaining score: {GlobalSettings.globalScore}");
            return true;
        }
        else
        {
            Debug.Log("Not enough score to spawn blocker.");
            Debug.Log($"Player has {GlobalSettings.globalScore} score, blocker cost is {blockerCost}");
            return false;
        }
    }

    public void SubtractScore(int amountToReduce)
    {
        GlobalSettings.globalScore -= amountToReduce;
        Debug.Log($"Score subtracted: {amountToReduce}. New score: {GlobalSettings.globalScore}");
    }


}