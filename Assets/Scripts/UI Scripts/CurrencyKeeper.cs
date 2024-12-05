using UnityEngine;
using UnityEngine.UI;

public class CurrencyKeeper : MonoBehaviour
{
    public Image[] DrachmaNumberImages;
    public Image drachmaOnes;
    public Image drachmaTens;
    public Image drachmaHundreds;
    public Image drachaThousands;
    public int drachma = GlobalSettings.globalDrachma;

    private int lastDrachma = -1;    

    void Update()
    {
        int currentDrachma = drachma;
        if (currentDrachma != lastDrachma && currentDrachma >= 0)
        {
            SetDrachmaUINumbers(currentDrachma);
            lastDrachma = currentDrachma;
        }
    }

    private void SetDrachmaUINumbers(int money)
    {
        int ones = drachma % 10;
        int tens = (drachma / 10) % 10;
        int hundreds = (drachma / 100) % 10;
        int thousands = (drachma / 1000) % 10;

        drachmaOnes.sprite = DrachmaNumberImages[ones].sprite;
        drachmaTens.sprite = DrachmaNumberImages[tens].sprite;
        drachmaHundreds.sprite = DrachmaNumberImages[hundreds].sprite;
        drachaThousands.sprite = DrachmaNumberImages[thousands].sprite;
    }
}
