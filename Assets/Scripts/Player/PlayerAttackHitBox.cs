using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerAttackHitBox : MonoBehaviour // This script is attached to the player weapon
{
    private KeyCode hitKey = KeyCode.Mouse0;
    private KeyCode blockKey;

    private ProjectileCollisionHandler projectileHandler;

    public GameObject rockSmashVFX;
    public GameObject HitBoxVisual;
    public GameObject RulerTargetDistance;
    public GameObject blockerPrefab;

    private Transform HandLocation;
    public Transform[] Lanes;

    public float attackDuration;

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
        HandLocation = transform;
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(hitKey) && canAttack)
        {
            StartCoroutine(Attack());
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) ||
            Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3) ||
            Input.GetKeyDown(KeyCode.Alpha4))
        {
            blockKey = GetPressedAlphaKey();
            StartCoroutine(CreateBlocker(blockKey));
        }
    }

    private IEnumerator CreateBlocker(KeyCode key)
    {
        int laneIndex = -1;

        switch (key)
        {
            case KeyCode.Alpha1:
                laneIndex = 0;
                break;
            case KeyCode.Alpha2:
                laneIndex = 1;
                break;
            case KeyCode.Alpha3:
                laneIndex = 2;
                break;
            case KeyCode.Alpha4:
                laneIndex = 3;
                break;
        }

        if (laneIndex >= 0 && laneIndex < Lanes.Length)
        {
            Instantiate(blockerPrefab, Lanes[laneIndex].position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Invalid lane index or lane Transform not assigned.");
        }

        yield return null;
    }

   

    private IEnumerator Attack()
    {
        weaponVisual.material.color = Color.red;
        weaponMaterial.color = Color.red;

        weaponAnimator.SetTrigger("HammerTrigger");

        canAttack = false; 

        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        canAttack = true; // Allow attacks again

        weaponAnimator.SetTrigger("Idle");

        weaponVisual.material.color = idleColor;
        weaponMaterial.color = Color.blue;
    }

    public void UpdateAttackSpeed(float amountToReduce)
    {
        if (attackSpeedUps < 5)
        {
            float convertedValue = amountToReduce / 100;
            if (attackDuration > convertedValue && attackDuration + -convertedValue > 0)
            {
                attackDuration -= convertedValue;
            }
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

    public void CanPlayerAttackThis(Collider other)
    {
        projectileHandler = other.GetComponent<ProjectileCollisionHandler>();

        if (projectileHandler != null )
        {
            if (other.tag == "Knockback" || other.tag == "TowerBuster")
            {
                projectileHandler.struckByWeapon = true;

                GameObject explosion = Instantiate(rockSmashVFX, other.transform.position, Quaternion.identity);

                explosion.SetActive(true);

                ParticleSystem explosionVFX = explosion.GetComponent<ParticleSystem>();

                if (explosionVFX != null)
                {
                    explosionVFX.Play();
                }
                Destroy(explosion, explosionVFX.main.duration);

            }
            else
            {
                projectileHandler.struckByWeapon = false;
            }
        }
    }

    private KeyCode GetPressedAlphaKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) return KeyCode.Alpha1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) return KeyCode.Alpha2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) return KeyCode.Alpha3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) return KeyCode.Alpha4;

        return KeyCode.None;
    }
}
