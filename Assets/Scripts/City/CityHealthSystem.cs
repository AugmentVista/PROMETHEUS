using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CityHealthSystem : MonoBehaviour
{
    private float maxShield = 100f;
    private float currentShield;

    private float maxHealth = GlobalSettings.globalCityMaxHP;
    public float currentHealth;

    private UI_Manager UI;

    public Image cityHealthGauge;

    public Image cityShieldGauge;

    public float elapsedTime = 0f;

    private float fillSpeed = 10f;

    private float targetFillAmount;
    private float targetShieldFill;

    private bool isCityAlive = true;


    private void Awake()
    {
        UI = FindObjectOfType<UI_Manager>();
        cityHealthGauge = UI.CityHealthImage;
        cityShieldGauge = UI.CityShieldImage;

        currentHealth = maxHealth;
        currentShield = maxShield;

        targetFillAmount = 1.0f;
        targetShieldFill = 1.0f;

        cityHealthGauge.fillAmount = targetFillAmount;
        cityShieldGauge.fillAmount = targetShieldFill;
    }


    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (isCityAlive)
        {
            if (Mathf.Abs(cityShieldGauge.fillAmount - targetShieldFill) > 0.01f && currentShield > 0)
            {
                cityShieldGauge.fillAmount = Mathf.Lerp(cityShieldGauge.fillAmount, targetShieldFill, Time.deltaTime * fillSpeed);
            }
            else if (currentShield <= 0)
            {
                cityShieldGauge.fillAmount = 0;
            }

            if (currentShield <= 0)
            {
                if (Mathf.Abs(cityHealthGauge.fillAmount - targetFillAmount) > 0.01f)
                {
                    cityHealthGauge.fillAmount = Mathf.Lerp(cityHealthGauge.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed);
                }
            }
            CityDeath();
        }
    }

    public void Heal(float healthToAdd)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthToAdd, 0f, maxHealth);
        targetFillAmount = currentHealth / maxHealth;
    }

    public void HealShield(float shieldToAdd)
    {
        currentShield = Mathf.Clamp(currentShield + shieldToAdd, 0f, maxShield);
        targetShieldFill = currentShield / maxShield;
    }

    public void TakeDamage(float damageTaken)
    {
        currentShield = Mathf.Clamp(currentShield - damageTaken, -maxShield, maxShield);
        if (currentShield <= 0)
        {
            currentHealth = Mathf.Clamp(currentHealth + currentShield, 0f, maxHealth);
            if (currentHealth == 0)
            { 
            // Unalived
            // add UI messaged that the city died to inform player they need to protect everything
            }
            currentShield = 0;
        }
        UpdateFillAmount();
    }


    public void UpgradeCityHealth(float amountToAdd)
    {
        GlobalSettings.globalCityMaxHP += amountToAdd;
        maxHealth = GlobalSettings.globalCityMaxHP;
        Heal(amountToAdd);
    }

    public void ResetCityHealthUpgrade()
    {
        GlobalSettings.globalCityMaxHP = 100f;
        maxHealth = GlobalSettings.globalCityMaxHP;

        currentHealth = maxHealth;
        currentShield = maxShield;
        Heal(maxHealth);
        targetFillAmount = 1.0f;
        targetShieldFill = 1.0f;

        cityHealthGauge.fillAmount = targetFillAmount;
        cityShieldGauge.fillAmount = targetShieldFill;
        isCityAlive = true;
    }

    private void UpdateFillAmount()
    {
        if (cityShieldGauge != null)
        {
            targetShieldFill = currentShield / maxShield;

            if (cityHealthGauge != null)
            {
                if (currentShield <= 0) 
                {
                    targetFillAmount = currentHealth / maxHealth;
                }
            }
        }
    }

    public void ResetCity()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;

        targetFillAmount = 1.0f;
        targetShieldFill = 1.0f;

        cityHealthGauge.fillAmount = targetFillAmount;
        cityShieldGauge.fillAmount = targetShieldFill;
        isCityAlive = true;
    }

    public void CityDeath()
    {
        Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentHealth <= 0.01f && cityHealthGauge.fillAmount <= 0.01f)
        {
            if (currentScene.name == "Level_1")
            {
                gameManager.hasHitEndWaveTrigger = true;

                isCityAlive = false;
                GlobalSettings.globalPauseOverride = true;
            }
        }
    }
}
