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

    float fillSpeed = 10f;

    private float targetFillAmount;

    private bool isPlayerAlive;

    public EventHandler Player_Death;
    

    private void Awake()
    {
        isPlayerAlive = true;
    }
    private void Start()
    {
        currentHealth = maxHealth;
        targetFillAmount = 1.0f;
        playerHealthGauge.fillAmount = targetFillAmount;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (Mathf.Abs(playerHealthGauge.fillAmount - targetFillAmount) > 0.01f)
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

    public void ResetPlayerHealth()
    {
        Heal(maxHealth);
        isPlayerAlive = true;
    }

    private void PlayerDeath()
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();

        if (currentHealth <= 0.01f && playerHealthGauge.fillAmount <= 0.01f)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == "Level_1")
            {
                gameManager.hasHitEndWaveTrigger = true;
                
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
            Heal(maxHealth);
            Debug.Log("Player has been healed");
        }
        else
        {
            Debug.Log("item is null");
        }
    }

    public void ResetPlayerHealthUpgrade()
    {
        GlobalSettings.globalPlayerHPMaximum = 100f;
        maxHealth = GlobalSettings.globalPlayerHPMaximum;

        currentHealth = maxHealth;
        Heal(maxHealth);
        targetFillAmount = 1.0f;

        playerHealthGauge.fillAmount = targetFillAmount;
        isPlayerAlive = true;
    }
}