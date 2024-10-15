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
            Debug.Log("Player is pressing attack");
            Attack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Proj")) // If this gameobject's collider collides with an object of tag ProJ
        {
            other.gameObject.GetComponent<ProjectileCollisionHandler>().inAttackHitBox = true; // sets to true
            if (!attackableProjectiles.Contains(other.gameObject)) // if not in list, add to list
            {
                attackableProjectiles.Add(other.gameObject); // add to list
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Proj")) // If this gameobject's collider collides with an object of tag ProJ
        {
            other.gameObject.GetComponent<ProjectileCollisionHandler>().inAttackHitBox = false; // in hitbox to false
            attackableProjectiles.Remove(other.gameObject); // remove from list
        }
    }

    private void Attack()
    {
        Debug.Log(attackableProjectiles.Count);

        List<GameObject> attackableProjectilesRuntime = new(); // creates a new list called attackProjRun
        foreach (GameObject proj in attackableProjectiles) // looks for proj's in attackableProjectiles list
        {
            //for every proj in attackable proj's get a script and set the bool of in hitbox to true
            if (proj.GetComponent<ProjectileCollisionHandler>().inAttackHitBox == true) // checks if true
            {
                Debug.Log("Bullet has entered attack hitbox");
                // migrate to spawner?
                //proj.GetComponent<ProjectileCollisionHandler>().inAttackHitBox = false;
            }
            else
            {
                attackableProjectilesRuntime.Add(proj);
            }
        }
        foreach (GameObject proj in attackableProjectilesRuntime)
        { 
            attackableProjectiles.Add(proj);
        }
        attackableProjectiles.Clear();
    }
}
