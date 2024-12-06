using UnityEngine;

public class HealOnDestruction : MonoBehaviour
{
    PlayerHealthSystem healthSystem;
    CityHealthSystem cityHealth;
    Blocker self;
    float lifeSpan = 0f;

    void Start()
    {
        healthSystem = FindObjectOfType<PlayerHealthSystem>();
        cityHealth = FindAnyObjectByType<CityHealthSystem>();
        self = GetComponent<Blocker>();
    }

    private void Update()
    {
        lifeSpan += Time.deltaTime;
        if (lifeSpan > 1f)
        {
            if (self.currentblockerHealth > 0)
            { 
                healthSystem.Heal(self.currentblockerHealth);
                cityHealth.Heal(self.currentblockerHealth);

            }
            self.Explode();
            Destroy(gameObject);
        }
    }

}
