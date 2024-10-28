using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public UpgradeItem[] itemDisplays;

    public GameObject[] itemsDisplayedInShop;

    #region Player Balance

    private int drachma = GlobalSettings.globalDrachma;
    private int amountToAdd = 5;
    public Image[] DrachmaPositive;
    public Image[] DrachmaNegative;// separate into postive and negative array of images
    public Image drachmaOnes;
    public Image drachmaTens;
    public Image drachmaHundreds;

    #endregion


    private int lastDrachma = -1;

    public void AddDrachma()
    {
        GlobalSettings.globalDrachma += amountToAdd;
        drachma = GlobalSettings.globalDrachma;
        Debug.Log(drachma);
    }

    public void SubdractDrachma()
    {
        GlobalSettings.globalDrachma -= amountToAdd;
        drachma = GlobalSettings.globalDrachma;
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

    public void PopulateShop()
    {
        for (int i = 0; i < itemDisplays.Length; i++)
        { 
        
        }
    
    
    }




}