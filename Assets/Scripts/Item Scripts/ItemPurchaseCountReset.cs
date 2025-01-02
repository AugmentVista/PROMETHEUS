using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPurchaseCountReset : MonoBehaviour
{
    public ItemDisplay[] items;

    public void ResetItemPurchaseCount()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "Level_1") 
        {
            foreach (ItemDisplay item in items)
            {
                item.timesPurchased = 0;
            }
        }
    }
}
