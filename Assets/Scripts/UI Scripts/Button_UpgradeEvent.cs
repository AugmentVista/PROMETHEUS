using System;
using UnityEngine;

public class Button_UpgradeEvent : MonoBehaviour
{
    public ShopManager Shop;
    public EventHandler<UpgradeEventArgs> UpgradeWasPurchased;
    public ItemDisplay item;
    public void OnPurchase()
    {
        if (item != null)
        {
            if (Shop.CanPlayerAffordThis(item.priceText))
            {
                UpgradeWasPurchased?.Invoke(this, new UpgradeEventArgs(item));
            }
            if (item.timesPurchased <= 4)
            {
                Shop.ApplyCost(item.priceText);
            }
        }
        else
        {
            // COME BACK WHEN YOU'RE A LITTLE MMM.... RICHER!
        }
    }
}
