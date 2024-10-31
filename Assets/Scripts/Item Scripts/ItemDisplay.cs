using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public UpgradeItem scriptableItem;

    public TMP_Text titleText;

    public TMP_Text descriptionText;

    public Image itemSprite;

    public Image currencyImage;

    public TMP_Text priceText;

    public float Modifer;

    private void Start()
    {
        titleText.text = scriptableItem.title;

        descriptionText.text = scriptableItem.description;

        itemSprite.sprite = scriptableItem.itemImage;

        currencyImage.sprite = scriptableItem.currencyType;

        priceText.text = scriptableItem.price;

        Modifer = scriptableItem.ImprovementModifier;
    }

    public bool IsTitleMatch(string titleToCheck)
    {
        return titleText.text.Equals(titleToCheck, StringComparison.OrdinalIgnoreCase);
    }

    public void UpdateDisplay(UpgradeItem newScriptableItem, TMP_Text newTitleText, TMP_Text newDescriptionText, 
        Image newItemSprite, Image newCurrencyImage, TMP_Text newPriceText, float newModifer)
    {
        scriptableItem = newScriptableItem;

        titleText.text = newTitleText.text;

        descriptionText.text = newDescriptionText.text;

        itemSprite.sprite = newItemSprite.sprite;

        currencyImage.sprite = newCurrencyImage.sprite;

        priceText.text = newPriceText.text;

        Modifer = newModifer;
    }
}
