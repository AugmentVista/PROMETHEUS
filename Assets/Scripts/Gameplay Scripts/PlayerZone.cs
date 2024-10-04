using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZone : MonoBehaviour
{
    public KeyCode hitKey = KeyCode.Space; // Key to hit

    private string targetTag = "Proj"; // Tag for projectiles

    public List<GameObject> currentProjectileInZone = new List<GameObject>(); // Track projectiles in the zone
    public List<GameObject> missedProjectiles = new List<GameObject>(); // keeps record of misses to reference

    private void Update()
    { 
        // if hitkey is pressed while there is a projectile in the zone HandleNoteHit is called
        if (Input.GetKeyDown(hitKey) && currentProjectileInZone.Count > 0)  
        {
            HandleNoteHit();
        }
        else if (Input.GetKeyDown(hitKey))
        {
            HandleNoteMiss();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Currently there is only one targetTag being "Proj", additional tag types may be added later
        if (other.CompareTag(targetTag))
        {
            currentProjectileInZone.Add(other.gameObject);

            Debug.Log(currentProjectileInZone.Count.ToString() + " Projectiles have entered zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            missedProjectiles.Add(other.gameObject); // adds object with targetTag to missedprojectiles list

            GameObject hitProjectile = missedProjectiles[missedProjectiles.Count - 1]; // most recent miss is last in the list

            // Remove the projectile from the zone list if it exists
            if (currentProjectileInZone.Contains(other.gameObject))
            {
                currentProjectileInZone.Remove(other.gameObject);
                HandleNoteMiss();
            }
            Debug.Log(currentProjectileInZone.Count.ToString() + " Projectiles remain in zone");
        }
    }

    private void HandleNoteHit() // Only runs if list isn't empty
    {
        if (currentProjectileInZone.Count > 0)
        {
            Debug.Log("HandleNoteHit called");

            GameObject hitProjectile = currentProjectileInZone[0];
            currentProjectileInZone.RemoveAt(0); // take that projectile out of list

            if (hitProjectile != null) 
            {
                // Call the collision handler to process the hit
                ProjectileCollisionHandler collisionHandler = hitProjectile.GetComponent<ProjectileCollisionHandler>();
                if (collisionHandler != null)
                {
                    collisionHandler.HandlePlayerInput(true); // provide true argument to didPlayerHitThis
                }
            }
        }
    }

    private void HandleNoteMiss()
    {
        Debug.Log("HHHHHHHHHHHHHHaaaaaaaaaaaaaaaaaaaa");
        if (missedProjectiles.Count > 0)
        {
            Debug.Log("HandleNoteMiss called");

            GameObject missedProjectile = missedProjectiles[0];
            missedProjectiles.RemoveAt(0);

            if (missedProjectile != null)
            {
                ProjectileCollisionHandler collisionHandler = missedProjectile.GetComponent<ProjectileCollisionHandler>();
                if (collisionHandler != null)
                {
                    Debug.Log("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
                    collisionHandler.HandlePlayerInput(false); // provide false argument to didPlayerHitThis
                }
            }
        }
    }

}
