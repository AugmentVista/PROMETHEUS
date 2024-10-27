using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



/// <summary>
/// Beginning of https://www.youtube.com/watch?v=HuXy4XX0hzg&list=WL&index=4 Tutorial
/// At 4:15 where he references this video https://www.youtube.com/watch?v=7GcEW6uwO8E
/// That video does not cover how the armour and other item details are made, just how to make something like them
/// I will need to follow another tutorial or cover the tutorial above and rewrite a large portion of code to integrate
/// this into my own work.
/// Try integrating first, then work out if it is needed to change guidance.
/// </summary>
public class UIShop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    //private void Start()
    //{
    //    CreateItemButton(Item.GetSprite(Item.Item.ItemType.Armour_1), "Armour 1", Item.GetCost(Item.ItemType.Armour_1), 0);
    //    CreateItemButton(Item.GetSprite(Item.Item.ItemType.Armour_2), "Armour 2", Item.GetCost(Item.ItemType.Armour_2), 1);
    //}

    private void CreateItemButton(Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();


        float shopItemHeight = 30f;
        shopItemRectTransform.anchoredPosition = new Vector2 (0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText (itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;


    }


}
