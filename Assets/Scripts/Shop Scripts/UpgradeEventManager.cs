using System;
using UnityEngine;

public class UpgradeEventManager : MonoBehaviour // this needs to be on an object within UI Manager
{
    [SerializeField] private GlobalSettings settings;

    [SerializeField] private Button_UpgradeEvent upgradeButton;

    private int healthUpgradesPurchased = 0;
    private int hammerUpgradesPurchased = 0;
    private int cityHealthUpgradesPurchased = 0;
    private int attackSpeedUpgradesPurchased = 0;
    private int sprintSpeedUpgradesPurchased = 0;
    private int blockUpgradesPurchased = 0;

    public EventHandler<UpgradeEventArgs> UpdateUpgradeHealth;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeHammer;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeCityHealth;
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
                    if (healthUpgradesPurchased < 5)
                    {
                        UpdateUpgradeHealth?.Invoke(this, new UpgradeEventArgs(Item));
                        Debug.Log("Player bought a Health Potion");
                        healthUpgradesPurchased += 1;
                    }
                    else { Debug.LogError($"Player has bought the last {Item}"); }
                }
                break;
            case "Hammer Upgrade":
                if (Item.IsTitleMatch("Hammer Upgrade"))
                {
                    if (hammerUpgradesPurchased < 5)
                    {
                        UpdateUpgradeHammer?.Invoke(this, new UpgradeEventArgs(Item));
                        hammerUpgradesPurchased += 1;
                    }
                    else { Debug.LogError($"Player has bought the last {Item}"); }
                }
                break;
                case "City Health Upgrade":
                if (Item.IsTitleMatch("City Health Upgrade"))
                {
                    if (cityHealthUpgradesPurchased < 5)
                    {
                        UpdateUpgradeCityHealth?.Invoke(this, new UpgradeEventArgs(Item));
                        cityHealthUpgradesPurchased += 1;
                        Debug.Log($"Player bought A {Item}");
                    }
                    else { Debug.LogError($"Player has bought the last {Item}"); }
                }
                break;
                case "Attack Speed Upgrade":
                if (Item.IsTitleMatch("Attack Speed Upgrade"))
                {
                    if (attackSpeedUpgradesPurchased < 5)
                    {
                        UpdateUpgradeAttackSpeed?.Invoke(this, new UpgradeEventArgs(Item));
                        attackSpeedUpgradesPurchased += 1;
                    }
                    else { Debug.LogError($"Player has bought the last {Item}"); }
                }
                break;
                //case "Sprint Speed Upgrade":
                //if (Item.IsTitleMatch("Sprint Speed Upgrade"))
                //{
                //    if (sprintSpeedUpgradesPurchased < 5)
                //    { 
                //        UpdateUpgradeSprintSpeed?.Invoke(this, new UpgradeEventArgs(Item));
                //        cityHealthUpgradesPurchased += 1;
                //    }
                //    else { Debug.LogError($"Player has bought the last {Item}"); }
                //}
                //break;
            case "Block Upgrade":
                if (Item.IsTitleMatch("Block Upgrade"))
                {
                    if (blockUpgradesPurchased < 5)
                    { 
                        UpdateUpgradeBlock?.Invoke(this, new UpgradeEventArgs(Item));
                        blockUpgradesPurchased += 1;
                    }
                    else { Debug.LogError($"Player has bought the last {Item}"); }
                }
                break;
            default:
                Debug.Log("No Matching Item Name Found");
                break;
        }
    }
}