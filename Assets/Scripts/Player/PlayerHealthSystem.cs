using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayerHealthSystem : MonoBehaviour
{
    public float maxHealth = GlobalSettings.globalPlayerHPMaximum;
    public float currentHealth;

    public Image playerHealthGauge;
    public float elapsedTime = 0f;

    public float fillSpeed = 0.5f;  // Controls how fast the health bar fills/drains

    private float targetFillAmount;

    private int hpUpgradeLimit = 0;

    private bool isPlayerAlive;
    // should probably add a bool for tracking if player is alive

    private void Awake()
    {
        isPlayerAlive = true;
    }
    private void Start()
    {
        currentHealth = maxHealth;
        targetFillAmount = 1.0f;
        playerHealthGauge.fillAmount = targetFillAmount;
        Debug.Log("Health Start has completed");
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (Mathf.Abs(playerHealthGauge.fillAmount - targetFillAmount) > 0.001f)
        {
            playerHealthGauge.fillAmount = Mathf.Lerp(playerHealthGauge.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed);
        }
        if (isPlayerAlive) { PlayerDeath(); }
    }

    private void UpdateFillAmount()
    {
        if (playerHealthGauge != null)
        {
            targetFillAmount = currentHealth / maxHealth;
        }
    }

    public void Heal(float healthToAdd)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthToAdd, 0f, maxHealth);
        UpdateFillAmount();
    }

    public static void TakeDamage(float damageTaken)
    {
        PlayerHealthSystem instance = FindObjectOfType<PlayerHealthSystem>();
        if (instance != null)
        {
            instance.currentHealth = Mathf.Clamp(instance.currentHealth - damageTaken, 0f, instance.maxHealth);
            instance.UpdateFillAmount();
        }
    }

    private void PlayerDeath()
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();

        if (currentHealth <= 0.01f && playerHealthGauge.fillAmount <= 0.01f)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == "Level_1")
            {
                gameManager.ResultsMenuTrigger();
                isPlayerAlive = false;
                GlobalSettings.globalPauseOverride = true;
            }
        }
    }

    public void HpEvent(ItemDisplay item)
    {
        if (item != null)
        {

            Debug.Log($"Mod is {item.Modifer}");
                GlobalSettings.globalPlayerHPMaximum += item.Modifer;
                maxHealth = GlobalSettings.globalPlayerHPMaximum;
                Heal(item.Modifer * 3f);
                Debug.Log("Player has been healed");
                hpUpgradeLimit++;
        }
        else
        {
            Debug.Log("item is null");
        }
    }
}