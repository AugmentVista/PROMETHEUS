using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    private EnemyProjectileManager spawner; // Reference to the spawner
    private Collider localCollider;
    private Rigidbody rb;

    [HideInInspector]
    public float travelSpeed = 10.0f; // default travel speed

    public enum ProjectileState { Traveling, Recycled, HitPlayer, MissedPlayer }
    public ProjectileState currentState;
    public enum ProjectileEffect { KnockBack, Stun, Slow, Cash, Bomb }
    public ProjectileEffect currentEffect;

    public int scoreReduction;

    public int value;

    public float stunDamage = 10f;

    public float knockBackDamage = 5f;

    public float slowDamage = 2f;

    public float bombDamage = 8f;

    private void Awake()
    {
        spawner = FindObjectOfType<EnemyProjectileManager>();
        localCollider = FindObjectOfType<Collider>();
        rb = GetComponent<Rigidbody>();
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
                Spin();
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
