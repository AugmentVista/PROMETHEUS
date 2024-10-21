using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    private ProjectileSpawner spawner; // Reference to the spawner
    private Collider localCollider;
    private Rigidbody rb;

    public float travelSpeed = 1000.0f; // default travel speed

    public enum ProjectileState { Damage, KnockBack, Stun, Slow }
    public ProjectileState currentState;
    public enum ProjectileEffect { Damage, KnockBack, Stun, Slow }
    public ProjectileEffect currentEffect;
    public enum ProjectileVariant { Stone, KnockBack, Stun, Slow, Cash, Bomb }
    public ProjectileVariant currentVariant;

    public float damage;
    public int scoreReduction;
    public int value;


    private void Awake()
    {
        spawner = FindObjectOfType<ProjectileSpawner>();
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
        switch (currentVariant)
        {
            case ProjectileVariant.Stone:
                //PatrolState();
                break;
            case ProjectileVariant.KnockBack:
                //ChaseState();
                break;
            case ProjectileVariant.Slow:
                //SearchState();
                break;
            case ProjectileVariant.Stun:
                //AttackState();
                break;
        }
    }



}
