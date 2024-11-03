using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitBox : MonoBehaviour // This script is attached to the player weapon
{
    public KeyCode hitKey = KeyCode.Space;

    ProjectileCollisionHandler projectileHandler;

    public GameObject rockSmashVFX;

    public float attackDuration = 0.25f;

    public float attackCooldown = 1.0f;

    private int attackSpeedUps = 0;

    public Renderer weaponVisual;

    public Material weaponMaterial;
    public Material idleWeapon;

    private Color idleColor;

    public Collider weaponCollider; 

    private bool canAttack = true; 
    private bool isAttacking = false;

    public List<string> ableToHit;

    private void Start()
    {
        ableToHit = new List<string>();

        weaponCollider = GetComponent<Collider>();
        weaponVisual = GetComponent<Renderer>();
        idleColor = weaponVisual.material.color;
        idleWeapon = weaponMaterial;
        idleWeapon.color = weaponMaterial.color;

        PlayerWeaponCheck();
    }

    public void PlayerWeaponCheck()
    {
        ableToHit.Add("Knockback");
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
       
        canAttack = false; // Prevent further attacks until cooldown expires

        // Allow hit detection for a short duration
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        canAttack = true; // Allow attacks again
        weaponVisual.material.color = idleColor;
        weaponMaterial.color = Color.blue;
    }

    public void CanPlayerAttackThis(Collider other)
    {
        projectileHandler = other.GetComponent<ProjectileCollisionHandler>();

        Debug.Log("Checking for ProjectileCollisionHandler on: " + other.gameObject.name);
        if (projectileHandler != null && isAttacking)
        {
            Debug.Log("Collider tag: " + other.tag);
            Debug.Log("ableToHit contains: " + string.Join(", ", ableToHit));

            if (ableToHit.Contains(other.tag))
            {
                Debug.Log("Tag match found: " + other.tag);
                if (isAttacking)
                {
                    projectileHandler.struckByWeapon = true;
                }
                else
                {
                    projectileHandler.struckByWeapon = false;
                }

                Debug.Log($"CanPlayerAttackThis: struckByWeapon is set to {projectileHandler.struckByWeapon}");

                Attack();
                // Instantiate the VFX at the position of the projectile
                GameObject explosion = Instantiate(rockSmashVFX, other.transform.position, Quaternion.identity);
                explosion.SetActive(true);
                ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Play();
                }

                Destroy(explosion, ps.main.duration);
                Debug.Log("Explosion instantiated at projectile position.");
            }
            else
            {
                Debug.Log("Tag not found in ableToHit list: " + other.tag);
                projectileHandler.struckByWeapon = false;
            }
        }
        else
        {
            Debug.Log("No ProjectileCollisionHandler found on: " + other.gameObject.name);
        }
    }
}
