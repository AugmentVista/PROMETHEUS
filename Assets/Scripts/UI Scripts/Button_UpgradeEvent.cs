using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_UpgradeEvent : MonoBehaviour // this needs to be on an object within UI Manager
{
    public EventHandler UpgradeWasPurchased; // sends notice to UpgradeEventManager to dish out upgrades


    private void Start()
    {
        Debug.Log("Upgrade Buttons is working");
    }

    private void OnPurchase()
    {
        // if all conditions to be purchased are met
        { 
            UpgradeWasPurchased?.Invoke(this, EventArgs.Empty);
        }
    
    }

    [System.Serializable]
    private class UpgradeInfoCollector
    {
        ///<summary>
        /// This class's job is to gather up all of the information from upgradable classes
        /// This class will then have each of these classes represented by an enum or something
        /// Then when the purchase button is clicked on the button will communicate with this script

        public GlobalSettings settings;

    }
}


