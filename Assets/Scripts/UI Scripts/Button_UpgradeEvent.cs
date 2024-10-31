using System;
using System.Collections;
using System.Collections.Generic;
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
                UpgradeWasPurchased?.Invoke(this, new UpgradeEventArgs(item));
                Debug.Log("Purchase successful");
            }
        }
    }

    public class UpgradeEventArgs : EventArgs
    {
        public ItemDisplay Item { get; }

        public UpgradeEventArgs(ItemDisplay item)
        {
            Item = item;
        }
    }
}