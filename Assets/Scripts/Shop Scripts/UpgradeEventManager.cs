using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEventManager : MonoBehaviour // this needs to be on an object within UI Manager
{
    [SerializeField] private GlobalSettings settings;

    [SerializeField] private Button_UpgradeEvent upgradeButton;

    public EventHandler UpdateUpgradeHealth;
    public EventHandler UpdateUpgradeWeapon;
    public EventHandler UpdateUpgradeStamina;
    public EventHandler UpdateUpgradeAttackSpeed;
    public EventHandler UpdateUpgradeSprintSpeed;

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

    private void Button_UpgradeEvent_UpgradeWasPurchased(object sender, Button_UpgradeEvent.UpgradeEventArgs e)
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

                }
                break;
            case "Hammer Upgrade":
                if (Item.IsTitleMatch("Hammer Upgrade"))
                {

                }
                break;
                case "Stamina Upgrade":
                if (Item.IsTitleMatch("Stamina Upgrade"))
                {

                }
                break;
                case "Attack Speed Upgrade":
                if (Item.IsTitleMatch("Attack Speed Upgrade"))
                {

                }
                break;
                case "Sprint Speed Upgrade":
                if (Item.IsTitleMatch("Sprint Speed Upgrade"))
                {

                }
                break;
            default:
                Debug.Log("No Matching Item Name Found");
                break;
        }
    }



}
