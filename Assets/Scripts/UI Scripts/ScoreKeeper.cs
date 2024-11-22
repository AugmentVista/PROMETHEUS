using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public Image[] PointsNumberImages;
    public Image pointsOnes;
    public Image pointsTens;
    public Image pointsHundreds;

    public Image[] DrachmaNumberImages;
    public Image drachmaOnes;
    public Image drachmaTens;
    public Image drachmaHundreds;

    public int score = GlobalSettings.globalScore;
    public int drachma = GlobalSettings.globalDrachma; // Ancient Greek name for currency

    private int lastPointValue = -1;
    private int lastDrachma = -1;    

    void Update()
    {
        int currentPoints = score;
        if (currentPoints != lastPointValue && currentPoints >= 0)
        {
            SetPointUINumbers(currentPoints);
            lastPointValue = currentPoints;
            GlobalSettings.globalScore = score;
        }

        int currentDrachma = drachma;
        if (currentDrachma != lastDrachma && currentDrachma >= 0)
        {
            SetDrachmaUINumbers(currentDrachma);
            lastDrachma = currentDrachma;
        }
    }

    private void SetPointUINumbers(int points)
    {
        int ones = points % 10;
        int tens = (points / 10) % 10;
        int hundreds = (points / 100) % 10;

        pointsOnes.sprite = PointsNumberImages[ones].sprite;
        pointsTens.sprite = PointsNumberImages[tens].sprite;
        pointsHundreds.sprite = PointsNumberImages[hundreds].sprite;
    }

    private void SetDrachmaUINumbers(int money)
    {
        int ones = drachma % 10;
        int tens = (drachma / 10) % 10;
        int hundreds = (drachma / 100) % 10;

        drachmaOnes.sprite = DrachmaNumberImages[ones].sprite;
        drachmaTens.sprite = DrachmaNumberImages[tens].sprite;
        drachmaHundreds.sprite = DrachmaNumberImages[hundreds].sprite;
    }
}
