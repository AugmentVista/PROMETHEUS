using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttackHitBox : MonoBehaviour // This script is attached to the player weapon
{
    public KeyCode hitKey = KeyCode.Space;

    ProjectileCollisionHandler projectileHandler;

    public GameObject rockSmashVFX;
    public Vector3 posOffset = new Vector3(0, 0, 200);

    public float attackDuration = 0.25f; // Time the collider remains active

    public float attackCooldown = 0.25f; // Time before another attack can occur

    public Renderer weaponVisual;

    private Color idleColor;

    [SerializeField]

    public Collider swordCollider; 
    private bool canAttack = true; 
    private bool isAttacking = false;


    public List<string> ableToHit;

    private void Start()
    {
        ableToHit = new List<string>();

        swordCollider = GetComponent<Collider>();
        weaponVisual = GetComponent<Renderer>();
        idleColor = weaponVisual.material.color;

        PlayerWeaponCheck();
    }

    public void PlayerWeaponCheck()
    {
        ableToHit.Add("Knockback");
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
       
        canAttack = false; // Prevent further attacks until cooldown expires

        // Allow hit detection for a short duration
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        canAttack = true; // Allow attacks again
        weaponVisual.material.color = idleColor;
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
