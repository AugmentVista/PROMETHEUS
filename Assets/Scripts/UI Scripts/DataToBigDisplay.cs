using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataToBigDisplay : MonoBehaviour
{
    public ItemDisplay display;
    public ItemDisplay bigDisplay;


    public void DisplayNewValues()
    {
        if (bigDisplay != null && display != null)
            bigDisplay.UpdateDisplay(display.scriptableItem, display.titleText, display.descriptionText, 
                display.itemSprite, display.currencyImage, display.priceText, display.Modifer);


        Debug.Log("huh?");
    }
}