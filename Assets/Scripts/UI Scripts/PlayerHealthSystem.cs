using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Image playerHealthGauge;
    public float elapsedTime = 0f;

    public float fillSpeed = 0.5f;  // Controls how fast the health bar fills/drains

    private float targetFillAmount; 

    private void Start()
    {
        currentHealth = maxHealth;
        targetFillAmount = 1.0f;
        playerHealthGauge.fillAmount = targetFillAmount;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (Mathf.Abs(playerHealthGauge.fillAmount - targetFillAmount) > 0.001f)
        {
            playerHealthGauge.fillAmount = Mathf.Lerp(playerHealthGauge.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed);
        }
        PlayerDeath();
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
        // Find the active instance of PlayerHealthSystem
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

            if (currentScene.name == "GamePlay1")
            {
                gameManager.GameOverTrigger();
            }
        }
    }
}