using System;
using UnityEngine;

public class UpgradeEventManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerHealthMerchandise;
    [SerializeField] GameObject RangeMerchandise;
    [SerializeField] GameObject CityHealthMerchandise;
    [SerializeField] GameObject MagicMerchandise;
    [SerializeField] GameObject BlockMerchandise;
    [SerializeField] GameObject SideToSideMerchandise;

    [SerializeField] private Button_UpgradeEvent upgradeButton;
    private int upgradeLimit = 4;

    public bool upgradesHaveBeenReset;

    private int healthUpgradesPurchased = 0;
    private int rangeUpgradesPurchased = 0;
    private int cityHealthUpgradesPurchased = 0;
    private int magicUpgradesPurchased = 0;
    private int blockUpgradesPurchased = 0;
    private int sideToSideUpgradesPurchased = 0;

    public EventHandler<UpgradeEventArgs> UpdateUpgradeHealth;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeRange;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeCityHealth;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeAttackSpeed;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeMagic;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeBlock;
    public EventHandler<UpgradeEventArgs> UpdateUpgradeSideToSide;

    [SerializeField] GameObject[] HealthStars;
    [SerializeField] GameObject[] RangeStars;
    [SerializeField] GameObject[] CityHealthStars;
    [SerializeField] GameObject[] MagicStars;
    [SerializeField] GameObject[] BlockStars;
    [SerializeField] GameObject[] SideStars;


    void Start()
    {
        upgradeButton.UpgradeWasPurchased += Button_UpgradeEvent_UpgradeWasPurchased;
        SetStarsFalse();
    }

    void SetStarsFalse()
    {
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
        foreach (GameObject star in SideStars)
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
        for (int j = 0; j < rangeUpgradesPurchased; j++)
        {
            RangeStars[j].SetActive(true);
        }
        for (int k = 0; k < cityHealthUpgradesPurchased; k++)
        {
            CityHealthStars[k].SetActive(true);
        }
        for (int l = 0; l < magicUpgradesPurchased; l++)
        {
            MagicStars[l].SetActive(true);
        }
        for (int m = 0; m < blockUpgradesPurchased; m++)
        {
            BlockStars[m].SetActive(true);
        }
        for (int n = 0; n < sideToSideUpgradesPurchased; n++)
        {
            SideStars[n].SetActive(true);
        }
        ResetUpgradeCount();
    }

    public void ResetUpgradeCount()
    {
        if (upgradesHaveBeenReset)
        {
            healthUpgradesPurchased = 0;
            rangeUpgradesPurchased = 0;
            cityHealthUpgradesPurchased = 0;
            magicUpgradesPurchased = 0;
            blockUpgradesPurchased = 0;
            sideToSideUpgradesPurchased = 0;
            SetStarsFalse();
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
                    if (healthUpgradesPurchased < upgradeLimit)
                    {
                        UpdateUpgradeHealth?.Invoke(this, new UpgradeEventArgs(Item));
                        healthUpgradesPurchased += 1;
                        Item.timesPurchased += 1;
                    }
                    else if (healthUpgradesPurchased >= upgradeLimit)
                    {
                        PlayerHealthMerchandise.SetActive(false);
                        DataToBigDisplay.DisplayDefault();
                    }
                break;
            case "Attack Range":
                    if (rangeUpgradesPurchased < upgradeLimit)
                    {
                        UpdateUpgradeRange?.Invoke(this, new UpgradeEventArgs(Item));
                        rangeUpgradesPurchased += 1;
                        Item.timesPurchased += 1;
                    }
                    else if (rangeUpgradesPurchased >= upgradeLimit)
                    {
                        RangeMerchandise.SetActive(false);
                        DataToBigDisplay.DisplayDefault();
                    }
                break;
                case "City Health":
                    if (cityHealthUpgradesPurchased < upgradeLimit)
                    {
                        UpdateUpgradeCityHealth?.Invoke(this, new UpgradeEventArgs(Item));
                        cityHealthUpgradesPurchased += 1;
                        Item.timesPurchased += 1;
                    }
                    else if (cityHealthUpgradesPurchased >= upgradeLimit)
                    {
                        CityHealthMerchandise.SetActive(false);
                        DataToBigDisplay.DisplayDefault();
                    }
                break;
            case "Magic":
                    if (magicUpgradesPurchased < upgradeLimit)
                    {
                        UpdateUpgradeMagic?.Invoke(this, new UpgradeEventArgs(Item));
                        magicUpgradesPurchased += 1;
                        Item.timesPurchased += 1;
                    }
                    else if (magicUpgradesPurchased >= upgradeLimit)
                    {
                        MagicMerchandise.SetActive(false);
                        DataToBigDisplay.DisplayDefault();
                    }
                break;
            case "Block":
                    if (blockUpgradesPurchased < upgradeLimit)
                    { 
                        UpdateUpgradeBlock?.Invoke(this, new UpgradeEventArgs(Item));
                        blockUpgradesPurchased += 1;
                        Item.timesPurchased += 1;
                    }
                    else if (blockUpgradesPurchased >= upgradeLimit)
                    {
                        BlockMerchandise.SetActive(false);
                        DataToBigDisplay.DisplayDefault();
                    }
                break;
            case "SideToSide":
                if (sideToSideUpgradesPurchased < upgradeLimit)
                {
                    UpdateUpgradeSideToSide?.Invoke(this, new UpgradeEventArgs(Item));
                    sideToSideUpgradesPurchased += 1;
                    Item.timesPurchased += 1;
                }
                else if (sideToSideUpgradesPurchased >= upgradeLimit)
                {
                    SideToSideMerchandise.SetActive(false);
                    DataToBigDisplay.DisplayDefault();
                }
                break;
            default:
                Debug.Log("No Matching Item Name Found, Upgrade failed");
                break;
        }
    }
}