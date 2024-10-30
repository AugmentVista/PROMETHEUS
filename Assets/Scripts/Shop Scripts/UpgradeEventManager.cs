using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEventManager : MonoBehaviour // this needs to be on an object within UI Manager
{
    [SerializeField] private Button_UpgradeEvent upgradeButton;

    public EventHandler UpdateUpgrades;
    private State state;

    private enum State
    { 
        Consumable,
        PlayerUpgrade,
        TemporaryBuff
    }

    void Start()
    {
        upgradeButton.UpgradeWasPurchased += Button_UpgradeEvent_UpgradeWasPurchased;
    }


    private void Button_UpgradeEvent_UpgradeWasPurchased(object sender, System.EventArgs _) // _  is for events that don’t require information beyond the event occurring.
    {




        //if (state == State.whatever)
        {
            // StartWave();
            // upgradeButton.UpgradeWasPurchased -= Button_UpgradeEvent_UpgradeWasPurchased; //unsubscribe
        }
    }





}
