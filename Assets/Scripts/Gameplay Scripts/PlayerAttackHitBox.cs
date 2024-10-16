using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitBox : MonoBehaviour // This script is attached to the player weapon
{
    public KeyCode hitKey = KeyCode.Space;

    public float attackDuration = 0.5f; // Time the collider remains active

    public float attackCooldown = 1f; // Time before another attack can occur

    public List<string> ableToHit; // List of valid projectile tags

    public Collider swordCollider; 
    private bool canAttack = true; // Check if player can attack

    private void Start()
    {
        swordCollider = GetComponent<Collider>(); 
        swordCollider.enabled = false; // Disable the collider initially
    }

    private void Update()
    {
        if (Input.GetKeyDown(hitKey) && canAttack)
        {
            Debug.Log("Player is pressing attack");
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false; // Prevent further attacks until cooldown expires
        swordCollider.enabled = true; 

        // Allow hit detection for a short duration
        yield return new WaitForSeconds(attackDuration);

        // Disable the collider after the attack duration
        swordCollider.enabled = false;
        
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true; // Allow attacks again
    }

    private void OnTriggerEnter(Collider other)
    {
        // Null check for ProjectileCollisionHandler
        ProjectileCollisionHandler projectileHandler = other.GetComponent<ProjectileCollisionHandler>();
        if (projectileHandler != null)
        {
            // Check if the tag of the projectile is in the ableToHit list
            if (ableToHit.Contains(other.tag))
            {
                Debug.Log("Projectile hit by weapon.");
                projectileHandler.struckByWeapon = true; // Set struckByWeapon to true
            }
            else
            {
                projectileHandler.struckByWeapon = false;
                Debug.Log("Projectile passed through the weapon.");
                // Projectile will pass through and hit the player or MissZone
            }
        }
    }
}
