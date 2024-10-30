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
}
