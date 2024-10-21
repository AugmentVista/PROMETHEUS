using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    /// <summary>
    /// This is the base class for all projectiles, it defines instructs the projectile how to behave based on what
    /// kind it is and other factors
    /// </summary>

    private ProjectileSpawner spawner; // Reference to the spawner
    private Collider localCollider;
    private Rigidbody rb;

    public float travelSpeed = 1000.0f; // default travel speed

    public enum ProjectileState { Traveling, Recycled, HitPlayer, MissedPlayer }
    public ProjectileState currentState;
    public enum ProjectileEffect { KnockBack, Stun, Slow, Cash, Bomb }
    public ProjectileEffect currentEffect;
    public enum ProjectileVariant { Purple, Yellow, Pink, Blue, Black }
    public ProjectileVariant currentVariant;

    public float damage;
    public int scoreReduction;
    public int value;


    public float stunDamage = 10f;
    public float knockBackDamage = 10f;
    public float slowDamage = 12f;
    public float bombDamage = 20f;

    private void Awake()
    {
        spawner = FindObjectOfType<ProjectileSpawner>();
        localCollider = FindObjectOfType<Collider>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        
    }
    void AssignProjectileType()
    {
        switch (currentVariant)
        {
            case ProjectileVariant.Purple:
                break;
            case ProjectileVariant.Yellow:
                break;
            case ProjectileVariant.Pink:
                break;
            case ProjectileVariant.Blue:
                break;
            case ProjectileVariant.Black:
                break;
        }
    }


    private void Update()
    {
        UpdateProjectileStateMachine();
    }

    void Spin()
    {
        transform.Rotate(5, 5, 5, Space.Self);
    }

    void UpdateProjectileStateMachine()
    {
        switch (currentEffect)
        {
            case ProjectileEffect.KnockBack:
                value = 1;
                break;
            case ProjectileEffect.Slow:
                value = 1;
                break;
            case ProjectileEffect.Stun:
                value = 2;
                break;
            case ProjectileEffect.Cash:
                value = 10;
                break;
            case ProjectileEffect.Bomb:
                value = 0;
                break;
        }
    }



}
