using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackHitBox : MonoBehaviour // This script is attached to the player weapon
{
    public KeyCode hitKey = KeyCode.Space;

    private List<GameObject> attackableProjectiles = new();

    private void Update()
    {
        if (Input.GetKeyDown(hitKey))
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log(attackableProjectiles.Count);
        List<GameObject> attackableProjectilesRuntime = new();
        foreach (GameObject proj in attackableProjectiles)
        {
            if (proj.GetComponent<ProjectileCollisionHandler>().inAttackHitBox == true)
            {
                Debug.Log("Bullet has entered attack hitbox");
                // migrate to spawner?
                proj.GetComponent<ProjectileCollisionHandler>().inAttackHitBox = false;
            }
            else
            {
                attackableProjectilesRuntime.Add(proj);
            }
        }
        attackableProjectiles.Clear();
        foreach (GameObject proj in attackableProjectilesRuntime)
        { 
            attackableProjectiles.Add(proj);
        }
    }



    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Proj")) // If this gameobject's collider collides with an object of tag ProJ
        {
            other.gameObject.GetComponent<ProjectileCollisionHandler>().inAttackHitBox = true;
            if (!attackableProjectiles.Contains(other.gameObject))
            { 
                attackableProjectiles.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Proj")) // If this gameobject's collider collides with an object of tag ProJ
        {
            other.gameObject.GetComponent<ProjectileCollisionHandler>().inAttackHitBox = false;
            attackableProjectiles.Remove(other.gameObject);
        }
    }
}
