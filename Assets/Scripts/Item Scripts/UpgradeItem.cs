using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items", order = 0)]
public class UpgradeItem : ScriptableObject
{
    public string title;

    public string description;

    public string price;

    public Sprite itemImage;

    public Sprite currencyType;

    public float ImprovementModifier;
}
