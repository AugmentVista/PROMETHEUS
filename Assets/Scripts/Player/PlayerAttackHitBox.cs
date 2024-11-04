using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitBox : MonoBehaviour // This script is attached to the player weapon
{
    private KeyCode hitKey = KeyCode.Mouse0;

    private ProjectileCollisionHandler projectileHandler;

    public GameObject rockSmashVFX;

    public GameObject HitBoxVisual;

    public float attackDuration;

    public float attackCooldown;

    private int attackSpeedUps = 0;

    public Renderer weaponVisual;

    public Material weaponMaterial;

    public Material idleWeapon;

    private Color idleColor;

    public Collider weaponCollider; 

    public Animator weaponAnimator;

    private bool canAttack = true; 

    private bool isAttacking = false;


    private void Start()
    {
        weaponCollider = GetComponent<Collider>();
        weaponVisual = GetComponent<Renderer>();

        idleColor = weaponVisual.material.color;
        idleWeapon = weaponMaterial;
        idleWeapon.color = weaponMaterial.color;
    }

    public void UpdateAttackSpeed(float amountToReduce)
    {
        if (attackSpeedUps < 5)
        { 
            float convertedValue = amountToReduce / 100;
            attackCooldown -= convertedValue;
        }
        attackSpeedUps += 1;
    }

    public void UpdateHammer(float amountToEnlarge)
    {
        float convertedValue = 1f + amountToEnlarge / 100;
        weaponCollider.transform.localScale *= convertedValue;
        HitBoxVisual.transform.localScale *= convertedValue;
        Debug.Log($"Hammer has grown by {convertedValue} %");
    }

    private void Update()
    {
        if (Input.GetKeyDown(hitKey) && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        weaponVisual.material.color = Color.red;
        weaponMaterial.color = Color.red;

        weaponAnimator.SetTrigger("HammerTrigger");

        canAttack = false; 

        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        canAttack = true; // Allow attacks again

        weaponAnimator.SetTrigger("Idle");

        weaponVisual.material.color = idleColor;
        weaponMaterial.color = Color.blue;
    }

    public void CanPlayerAttackThis(Collider other)
    {
        projectileHandler = other.GetComponent<ProjectileCollisionHandler>();

        Debug.Log("Checking for ProjectileCollisionHandler on: " + other.gameObject.name);
        if (projectileHandler != null )
        {
            if (other.tag == "Knockback" /* && isAttacking*/)
            {
                if (isAttacking)
                {
                    projectileHandler.struckByWeapon = true;
                }
                else
                {
                    projectileHandler.struckByWeapon = false;
                }

                Debug.Log($"CanPlayerAttackThis: struckByWeapon is set to {projectileHandler.struckByWeapon}");

                GameObject explosion = Instantiate(rockSmashVFX, other.transform.position, Quaternion.identity);

                explosion.SetActive(true);

                ParticleSystem explosionVFX = explosion.GetComponent<ParticleSystem>();

                if (explosionVFX != null)
                {
                    explosionVFX.Play();
                }

                Destroy(explosion, explosionVFX.main.duration);
                Debug.Log("Explosion instantiated at projectile position.");
            }
            else
            {
                Debug.LogError("Tag not found in ableToHit list: " + other.tag);
                projectileHandler.struckByWeapon = false;
            }
        }
        else
        {
            Debug.LogError("No ProjectileCollisionHandler found on: " + other.gameObject.name);
        }
    }
}
