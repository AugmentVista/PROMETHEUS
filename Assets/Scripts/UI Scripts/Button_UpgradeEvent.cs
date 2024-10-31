using System;
using UnityEngine;

public class Button_UpgradeEvent : MonoBehaviour
{
    public ShopManager Shop;
    public EventHandler<UpgradeEventArgs> UpgradeWasPurchased;
    public ItemDisplay item; // Assign this in the inspector for each button

    private void Start()
    {
        Debug.Log("Upgrade Buttons is working");
    }

    public void OnPurchase()
    {
        if (item != null)
        {
            if (Shop.CanPlayerAffordThis(item.priceText))
            {
                Debug.Log("Purchase successful");
                UpgradeWasPurchased?.Invoke(this, new UpgradeEventArgs(item));
            }
        }
        else
        {
            // COME BACK WHEN YOU'RE A LITTLE MMM.... RICHER!
        }
    }
}
