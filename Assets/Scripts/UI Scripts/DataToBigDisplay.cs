using UnityEngine;

public class DataToBigDisplay : MonoBehaviour
{
    public ItemDisplay display;
    public ItemDisplay defaultDisplay;
    public ItemDisplay bigDisplay;


    public static void DisplayDefault()
    {
        DataToBigDisplay instance = FindObjectOfType<DataToBigDisplay>();

        if (instance != null)
        {
            instance.bigDisplay.UpdateDisplay(instance.defaultDisplay.scriptableItem, instance.defaultDisplay.titleText, instance.defaultDisplay.descriptionText,
            instance.defaultDisplay.itemSprite, instance.defaultDisplay.currencyImage, instance.defaultDisplay.priceText, instance.defaultDisplay.Modifer);
        }
        
    }


    public void DisplayNewValues()
    {
        if (bigDisplay != null && display != null)
            bigDisplay.UpdateDisplay(display.scriptableItem, display.titleText, display.descriptionText, 
                display.itemSprite, display.currencyImage, display.priceText, display.Modifer);
    }
}