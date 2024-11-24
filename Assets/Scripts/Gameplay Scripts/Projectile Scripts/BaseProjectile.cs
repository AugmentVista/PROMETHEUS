using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    private EnemyProjectileManager spawner; // Reference to the spawner
    private Collider localCollider;
    private Rigidbody rb;

    public float travelSpeed; // default travel speed

    public enum ProjectileState { Traveling, Recycled, HitPlayer, MissedPlayer }
    public ProjectileState currentState;
    public enum ProjectileEffect { KnockBack, TowerBuster, Slow, Cash, Bomb }
    public ProjectileEffect currentEffect;

    private float elapsedTime = 0f;

    private Vector3 previousVelocity;

    private bool isPaused = false;

    public int value;

    float TowerBusterDamage = 10f;

    public float knockBackDamage;

    float slowDamage = 2f;

    float bombDamage = 8f;

    private void Awake()
    {
        spawner = FindObjectOfType<EnemyProjectileManager>();
        localCollider = FindObjectOfType<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        UpdateProjectileStateMachine();
    }

    private void FixedUpdate()
    {
        if (GlobalSettings.globalPauseOverride)
        {
            // Pause
            if (!isPaused)
            {
                previousVelocity = rb.velocity; // Store current velocity
                rb.velocity = Vector3.zero; // Freeze the projectile
                isPaused = true;
            }
        }
        else
        {
            // Unpause
            if (isPaused)
            {
                rb.velocity = previousVelocity; // Restore velocity
                isPaused = false;
            }
        }
    }


    void Spin()
    {
        transform.Rotate(1, 0, 0, Space.Self);
    }

    void UpdateProjectileStateMachine()
    {
        switch (currentEffect)
        {
            case ProjectileEffect.KnockBack:
                Spin();
                break;
            case ProjectileEffect.Slow:
                value = 1;
                break;
            case ProjectileEffect.TowerBuster:
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
