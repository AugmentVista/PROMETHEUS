using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public int maxProjectiles = GlobalSettings.spawnerProjectilesMaxAmount;

    private int currentProjectiles = 0;
    private Queue<GameObject> pooledProjectiles = new Queue<GameObject>(); // Queue to hold inactive projectiles

    public GameObject RequestProjectile()
    {
        GameObject projectileInstance;

        // Check if there are any available pooled projectiles or need to create new ones
        if (pooledProjectiles.Count + currentProjectiles < maxProjectiles)
        {
            // Create new projectile if under max limit
            projectileInstance = Instantiate(ProjectilePrefab);
            currentProjectiles++;
        }
        else if (pooledProjectiles.Count > 0)
        {
            // Reuse from the pool
            projectileInstance = pooledProjectiles.Dequeue();
            projectileInstance.SetActive(true);
            projectileInstance.GetComponent<Renderer>().enabled = true;
            projectileInstance.GetComponent<Collider>().enabled = true;
        }
        else
        {
            return null; // No available projectiles
        }

        return projectileInstance;
    }

    public void ReturnProjectile(GameObject obj)
    {
        // Reset necessary components on the projectile
        ProjectileCollisionHandler handler = obj.GetComponent<ProjectileCollisionHandler>();
        if (handler != null)
        {
            handler.struckByWeapon = false;
            handler.reusedProjectile = true;
        }

        // Reset physics (if needed)
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Reset velocity
        }

        // Disable the projectile and add it back to the pool
        obj.SetActive(false);
        pooledProjectiles.Enqueue(obj);

        // Update projectile count
        currentProjectiles--;
    }

}
