using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerAttackHitBox : MonoBehaviour // This script is attached to the player weapon
{
    private KeyCode hitKey = KeyCode.Mouse0;

    private ProjectileCollisionHandler projectileHandler;

    public GameObject rockSmashVFX;

    public GameObject HitBoxVisual;

    public GameObject RulerTargetDistance;

    private Transform HandLocation;

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

    private float lerpDuration = 1f;
    private bool movingForward = false;
    private float timeElapsed;


    private void Start()
    {
        weaponCollider = GetComponent<Collider>();
        weaponVisual = GetComponent<Renderer>();

        idleColor = weaponVisual.material.color;
        idleWeapon = weaponMaterial;
        idleWeapon.color = weaponMaterial.color;
        HandLocation = transform;
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

    //private void ThrowHammer()
    //{
    //    // Update elapsed time for the Lerp
    //    timeElapsed += Time.deltaTime;
    //    float lerpProgress = timeElapsed / lerpDuration;

    //    if (movingForward)
    //    {
    //        transform.position = Vector3.Lerp(HandLocation.position, RulerTargetDistance.transform.position, lerpProgress);

    //        // Check if the movement to the target is complete
    //        if (lerpProgress >= 1f)
    //        {
    //            movingForward = false; // Toggle to returning phase
    //            timeElapsed = 0f; // Reset for the return Lerp
    //        }
    //    }
    //    else
    //    {
    //        transform.position = Vector3.Lerp(RulerTargetDistance.transform.position, HandLocation.position, lerpProgress);

    //        // Check if the return movement is complete
    //        if (lerpProgress >= 1f)
    //        {
    //            movingForward = true; // Ready to start again if needed
    //            timeElapsed = 0f; // Reset for future throws
    //            isAttacking = false; // Stop the attack movement
    //        }
    //    }
    //}

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
        //if (isAttacking)
        //{
        //    weaponAnimator.SetTrigger("HammerLerp");
        //    ThrowHammer();
        //}
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
