using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEventManager : MonoBehaviour // this needs to be on an object within UI Manager
{
    [SerializeField] private GlobalSettings settings;

    [SerializeField] private Button_UpgradeEvent upgradeButton;

    public EventHandler<UpgradeEventArgs> UpdateUpgradeHealth;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeHammer;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeStamina;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeAttackSpeed;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeSprintSpeed;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeBlock;


    void Start()
    {
        upgradeButton.UpgradeWasPurchased += Button_UpgradeEvent_UpgradeWasPurchased;
    }

    private void Button_UpgradeEvent_UpgradeWasPurchased(object sender, UpgradeEventArgs e)
    {
        ItemDisplay purchasedItemDisplay = e.Item;

        ApplyUpgrades(purchasedItemDisplay);
    }

    private void ApplyUpgrades(ItemDisplay Item)
    {
        switch (Item.scriptableItem.title)
        {
            case "Health Potion":
                if (Item.IsTitleMatch("Health Potion"))
                {
                    UpdateUpgradeHealth?.Invoke(this, new UpgradeEventArgs(Item));
                    Debug.Log("Player bought a Health Potion");
                }
                break;
            case "Hammer Upgrade":
                if (Item.IsTitleMatch("Hammer Upgrade"))
                {
                    UpdateUpgradeHammer?.Invoke(this, new UpgradeEventArgs(Item));
                }
                break;
                case "Stamina Upgrade":
                if (Item.IsTitleMatch("Stamina Upgrade"))
                {
                    UpdateUpgradeStamina?.Invoke(this, new UpgradeEventArgs(Item));
                }
                break;
                case "Attack Speed Upgrade":
                if (Item.IsTitleMatch("Attack Speed Upgrade"))
                {
                    UpdateUpgradeAttackSpeed?.Invoke(this, new UpgradeEventArgs(Item));
                }
                break;
                case "Sprint Speed Upgrade":
                if (Item.IsTitleMatch("Sprint Speed Upgrade"))
                {
                    UpdateUpgradeSprintSpeed?.Invoke(this, new UpgradeEventArgs(Item));
                }
                break;
            case "Block Upgrade":
                if (Item.IsTitleMatch("Block Upgrade"))
                {
                    UpdateUpgradeBlock?.Invoke(this, new UpgradeEventArgs(Item));
                }
                break;
            default:
                Debug.Log("No Matching Item Name Found");
                break;
        }
    }



}
