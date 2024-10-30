using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public Animator animator;

    private EnemyProjectileManager spawner;
    void Start()
    {
        animator = GetComponent<Animator>();
        spawner = GetComponent<EnemyProjectileManager>();
    }

    void Update()
    {
        //SyncAttackSpeed();
    }

    //private void SyncAttackSpeed()
    //{
    //    // As spawn interval goes smaller attackspeed goes larger
    //    float newAttackSpeed = 1 / spawner.spawnInterval * Time.deltaTime;                   
    //    if (animator != null && spawner != null)
    //    {
    //        animator.speed = newAttackSpeed;
    //    }
    //}
}
