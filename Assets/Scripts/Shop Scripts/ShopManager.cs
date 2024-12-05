using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private CurrencyKeeper scoreKeeper;

    #region Player Balance

    private int drachma => scoreKeeper != null ? scoreKeeper.drachma : 0;
    public Image[] DrachmaPositive;
    public Image[] DrachmaNegative;
    public Image drachmaOnes;
    public Image drachmaTens;
    public Image drachmaHundreds;
    public Image drachaThousands;

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
        int thousands = (absDrachma / 1000) % 10;

        if (drachma >= 0)
        {
            // Display positive sprites
            drachmaOnes.sprite = DrachmaPositive[ones].sprite;
            drachmaTens.sprite = DrachmaPositive[tens].sprite;
            drachmaHundreds.sprite = DrachmaPositive[hundreds].sprite;
            drachaThousands.sprite =  DrachmaPositive[thousands].sprite;
        }
        else
        {
            // Display negative sprites
            drachmaOnes.sprite = DrachmaNegative[ones].sprite;
            drachmaTens.sprite = DrachmaNegative[tens].sprite;
            drachmaHundreds.sprite = DrachmaNegative[hundreds].sprite;
            drachaThousands.sprite = DrachmaNegative[thousands].sprite;
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
}