using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public Animator animator;

    private ProjectileSpawner spawner;
    void Start()
    {
        animator = GetComponent<Animator>();
        spawner = GetComponent<ProjectileSpawner>();
    }

    void Update()
    {
        SyncAttackSpeed();
    }

    private void SyncAttackSpeed()
    {
        float attackSpeed = spawner.spawnInterval * Time.deltaTime;                        
        if (animator != null && spawner != null)
        {
            animator.speed = spawner.spawnInterval;
        }
    }
}
