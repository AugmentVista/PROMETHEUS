using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZone : MonoBehaviour
{
    public KeyCode hitKey = KeyCode.Space; 

    private string targetTag = "Proj"; // Tag for projectiles

    public List<GameObject> currentProjectileInZone = new List<GameObject>(); 
    public List<GameObject> missedProjectiles = new List<GameObject>(); 

    private void Update()
    { 
        // if hitkey is pressed while there is a projectile in the zone HandleNoteHit is called
        if (Input.GetKeyDown(hitKey))  
        {
            if (currentProjectileInZone.Count > 0)
            {
                HandleNoteHit();
            }

            else if (currentProjectileInZone.Count >= 0)
            { 
                HandleNoteMiss(); 
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Add proj to the activeProj list when it enters
        if (other.CompareTag(targetTag))
        {
            // When object enters it
            currentProjectileInZone.Add(other.gameObject); 
        }
    }

    private void HandleNoteHit() // Only runs if list isn't empty
    {
        if (currentProjectileInZone.Count > 0)
        {
            Debug.Log("HandleNoteHit called");

            GameObject hitProjectile = currentProjectileInZone[0]; // most recent hit becomes index 0
            currentProjectileInZone.RemoveAt(0);

            ProjectileCollisionHandler collisionHandler = hitProjectile.GetComponent<ProjectileCollisionHandler>();
            if (collisionHandler != null)
            {
                collisionHandler.HandlePlayerInput(true); 
            }
        }
    }

    private void HandleNoteMiss()
    {
        if (currentProjectileInZone.Count <= 0 && missedProjectiles.Count > 0) 
        {
            Debug.Log("HandleNoteMiss called");

            GameObject missedProjectile = missedProjectiles[0];
            missedProjectiles.RemoveAt(0);


            ProjectileCollisionHandler collisionHandler = missedProjectile.GetComponent<ProjectileCollisionHandler>();
            if (collisionHandler != null)
            {
                collisionHandler.HandlePlayerInput(false); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag) && currentProjectileInZone.Count < 0)
        {
            missedProjectiles.Add(other.gameObject); // Add to list of misses
            GameObject hitProjectile = missedProjectiles[0]; // most recent miss becomes index 0

            currentProjectileInZone.Remove(other.gameObject);

            Debug.Log(currentProjectileInZone.Count.ToString() + "Projectile leaving zone");
        }
    }
}
