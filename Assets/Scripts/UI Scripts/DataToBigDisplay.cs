using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataToBigDisplay : MonoBehaviour
{
    ItemDisplay display;
    GameObject BigDisplay;


    void Start()
    {
        display = GetComponent<ItemDisplay>();
    }

    public void SelectToDisplay()
    {
        ItemDisplay targetDisplay = BigDisplay.GetComponent<ItemDisplay>();
        if (targetDisplay != null)
        { 
            targetDisplay.scriptableItem = display.scriptableItem;

            targetDisplay.titleText = display.titleText;

            targetDisplay.descriptionText = display.descriptionText;

            targetDisplay.itemSprite = display.itemSprite;

            targetDisplay.currencyImage = display.currencyImage;

            targetDisplay.priceText = display.priceText;

            targetDisplay.Modifer = display.Modifer;
        }
    }
}