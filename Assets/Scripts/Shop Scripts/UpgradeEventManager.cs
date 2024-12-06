using System;
using UnityEngine;

public class UpgradeEventManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerHealthMerchandise;
    [SerializeField] GameObject HammerMerchandise;
    [SerializeField] GameObject CityHealthMerchandise;
    [SerializeField] GameObject ToBeChanged;
    [SerializeField] GameObject MagicMerchandise;
    [SerializeField] GameObject BlockMerchandise;

    [SerializeField] private Button_UpgradeEvent upgradeButton;
    int upgradeLimit = 4;

    public bool upgradesHaveBeenReset;

    private int healthUpgradesPurchased = 0;
    private int hammerUpgradesPurchased = 0;
    private int cityHealthUpgradesPurchased = 0;
    private int attackSpeedUpgradesPurchased = 0;
    private int magicUpgradesPurchased = 0;
    private int blockUpgradesPurchased = 0;

    public EventHandler<UpgradeEventArgs> UpdateUpgradeHealth;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeHammer;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeCityHealth;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeAttackSpeed;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeMagic;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeBlock;

    [SerializeField] GameObject[] HealthStars;
    [SerializeField] GameObject[] RangeStars;
    [SerializeField] GameObject[] CityHealthStars;
    [SerializeField] GameObject[] MagicStars;
    [SerializeField] GameObject[] BlockStars;



    void Start()
    {
        upgradeButton.UpgradeWasPurchased += Button_UpgradeEvent_UpgradeWasPurchased;

        foreach (GameObject star in HealthStars)
        { 
            star.SetActive(false);
        }
        foreach (GameObject star in RangeStars)
        { 
            star.SetActive(false); 
        }
        foreach (GameObject star in CityHealthStars) 
        { 
            star.SetActive(false); 
        }
        foreach (GameObject star in MagicStars)
        {
            star.SetActive(false);
        }
        foreach (GameObject star in BlockStars)
        {
            star.SetActive(false);
        }
    }


    private void Update()
    {
        for (int i = 0; i < healthUpgradesPurchased; i++)
        {
            HealthStars[i].SetActive(true);
        }
        for (int j = 0; j < hammerUpgradesPurchased; j++)
        {
            RangeStars[j].SetActive(true);
        }
        for (int k = 0; k < cityHealthUpgradesPurchased; k++)
        {
            RangeStars[k].SetActive(true);
        }
        for (int l = 0; l < magicUpgradesPurchased; l++)
        {
            MagicStars[l].SetActive(true);
        }
        for (int m = 0; m < blockUpgradesPurchased; m++)
        {
            BlockStars[m].SetActive(true);
        }
        ResetUpgradeCount();
    }

    public void ResetUpgradeCount()
    {
        if (upgradesHaveBeenReset)
        {
            healthUpgradesPurchased = 0;
            hammerUpgradesPurchased = 0;
            cityHealthUpgradesPurchased = 0;
            attackSpeedUpgradesPurchased = 0;
            magicUpgradesPurchased = 0;
            blockUpgradesPurchased = 0;
            upgradesHaveBeenReset = false;
        }
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
                    healthUpgradesPurchased += 1;
                    if (healthUpgradesPurchased < upgradeLimit)
                    {
                        UpdateUpgradeHealth?.Invoke(this, new UpgradeEventArgs(Item));
                        Debug.Log("Player bought a Health Potion");
                    }
                    else 
                    {
                        PlayerHealthMerchandise.SetActive(false);
                        DataToBigDisplay.DisplayDefault();
                        Debug.LogError($"Player has bought the last {Item}"); 
                    }
                break;
            case "Attack Range":
                    hammerUpgradesPurchased += 1;
                    if (hammerUpgradesPurchased < upgradeLimit)
                    {
                        UpdateUpgradeHammer?.Invoke(this, new UpgradeEventArgs(Item));
                    }
                    else
                    {
                        HammerMerchandise.SetActive(false);
                        DataToBigDisplay.DisplayDefault();
                        Debug.LogError($"Player has bought the last {Item}");
                    }
                break;
                case "City Health":
                    cityHealthUpgradesPurchased += 1;
                    if (cityHealthUpgradesPurchased < upgradeLimit)
                    {
                        UpdateUpgradeCityHealth?.Invoke(this, new UpgradeEventArgs(Item));
                        Debug.Log($"Player bought A {Item}");
                    }
                    else
                    {
                        CityHealthMerchandise.SetActive(false);
                        DataToBigDisplay.DisplayDefault();
                        Debug.LogError($"Player has bought the last {Item}");
                    }
                break;
                case "Attack Speed Upgrade":
                    attackSpeedUpgradesPurchased += 1;
                    if (attackSpeedUpgradesPurchased < upgradeLimit)
                    {
                        UpdateUpgradeAttackSpeed?.Invoke(this, new UpgradeEventArgs(Item));
                    }
                    else
                    {
                        ToBeChanged.SetActive(false);
                        DataToBigDisplay.DisplayDefault();
                        Debug.LogError($"Player has bought the last {Item}");
                    }
                break;
            case "Magic":
                    magicUpgradesPurchased += 1;
                    if (magicUpgradesPurchased < upgradeLimit)
                    {
                        UpdateUpgradeMagic?.Invoke(this, new UpgradeEventArgs(Item));
                    }
                    else
                    {
                        MagicMerchandise.SetActive(false);
                        DataToBigDisplay.DisplayDefault();
                        Debug.LogError($"Player has bought the last {Item}");
                    }
                break;
            case "Block":
                    blockUpgradesPurchased += 1;
                    if (blockUpgradesPurchased < upgradeLimit)
                    { 
                        UpdateUpgradeBlock?.Invoke(this, new UpgradeEventArgs(Item));
                    }
                    else
                    {
                        BlockMerchandise.SetActive(false);
                        DataToBigDisplay.DisplayDefault();
                        Debug.LogError($"Player has bought the last {Item}");
                    }
                break;
            default:
                Debug.Log("No Matching Item Name Found, Upgrade failed");
                break;
        }
    }
}