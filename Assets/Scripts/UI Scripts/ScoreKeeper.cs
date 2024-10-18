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

    public Image[] DrachmaNumberImages;
    public Image drachmaOnes;
    public Image drachmaTens;

    public int score;
    public int drachma; // Ancient Greek name for currency

    private int lastPointValue = -1;
    private int lastDrachma = -1;    

    private void Start()
    {
    }

    void Update()
    {
        int currentPoints = score;
        if (currentPoints != lastPointValue && currentPoints >= 0)
        {
            SetPointUINumbers(currentPoints);
            lastPointValue = currentPoints;
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

        pointsOnes.sprite = PointsNumberImages[ones].sprite;
        pointsTens.sprite = PointsNumberImages[tens].sprite;
    }

    private void SetDrachmaUINumbers(int money)
    {
        int ones = drachma % 10;
        int tens = (drachma / 10) % 10;

        drachmaOnes.sprite = DrachmaNumberImages[ones].sprite;
        drachmaTens.sprite = DrachmaNumberImages[tens].sprite;
    }
}
