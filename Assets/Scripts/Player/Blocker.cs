using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blocker : MonoBehaviour
{
    private BaseProjectile baseProj;

    private ProjectileCollisionHandler projectileHandler;

    public GameObject rockSmashVFX;

    private Collider blockerCollider;

    public float blockerMaxHP = 30;

    private float dmgReduction = 0;

    public float currentblockerHealth;

    public Image healthBar;


    private void Start()
    {
        blockerCollider = GetComponent<Collider>();
        currentblockerHealth = blockerMaxHP;

        UpdateHealthBar();
    }

    public void BlockerTakeDamage(float damageTaken)
    {
        damageTaken -= dmgReduction;
        currentblockerHealth = Mathf.Clamp(currentblockerHealth - damageTaken, 0f, blockerMaxHP);

        Shrink();

        UpdateHealthBar();
        if (currentblockerHealth <= 0f)
        {
            Destroy(gameObject, 0.5f);
        }
    }

    public void UpgradeBlocker(float amountToAdd)
    {
        dmgReduction += amountToAdd;
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentblockerHealth / blockerMaxHP;
        }
    }

    private void Shrink()
    {
        // Scale the blocker based on health
        float healthPercentage = currentblockerHealth / blockerMaxHP;
        float newScale = Mathf.Lerp(0.005f, 0.01f, healthPercentage); // Scale between 0.005 and 0.01
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        projectileHandler = other.GetComponent<ProjectileCollisionHandler>();
        baseProj = other.GetComponent<BaseProjectile>();

        if (projectileHandler != null)
        {
            if (other.tag == "Knockback" || other.tag == "TowerBuster")
            {
                GameObject explosion = Instantiate(rockSmashVFX, other.transform.position, Quaternion.identity);

                explosion.SetActive(true);

                ParticleSystem explosionVFX = explosion.GetComponent<ParticleSystem>();

                if (explosionVFX != null)
                {
                    explosionVFX.Play();
                }
                Destroy(explosion, explosionVFX.main.duration);

            }
        }
    }
}
