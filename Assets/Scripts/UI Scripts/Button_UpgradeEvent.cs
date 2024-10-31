using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_UpgradeEvent : MonoBehaviour
{
    public EventHandler<UpgradeEventArgs> UpgradeWasPurchased;
    public ItemDisplay item; // Assign this in the inspector for each button

    private void Start()
    {
        Debug.Log("Upgrade Buttons is working");
    }

    private void OnPurchase()
    {
        // Assuming purchase conditions are met
        if (item != null)
        {
            UpgradeWasPurchased?.Invoke(this, new UpgradeEventArgs(item));
        }
    }

    public class UpgradeEventArgs : EventArgs
    {
        public ItemDisplay Item { get; }  // Refers to ItemDisplay rather than UpgradeItem

        public UpgradeEventArgs(ItemDisplay item)
        {
            Item = item;
        }
    }
}