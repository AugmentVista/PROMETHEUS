using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class UpgradeItem : ScriptableObject
{
    public string title;

    public string description;

    public string price;

    public Sprite itemImage;

    public Sprite currencyType;

    public float ImprovementModifier;
}
