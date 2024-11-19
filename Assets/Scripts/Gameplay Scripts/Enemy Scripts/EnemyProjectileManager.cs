using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public int maxProjectiles = 50;
    public Transform InitalPosition = null;

    private int currentProjectiles = 0;
    private int totalProjectilesCreated = 0;
    public Queue<GameObject> pooledProjectiles = new Queue<GameObject>(); // Queue to hold inactive projectiles

    public GameObject RequestProjectile(Transform localTransform)
    {
        GameObject projectileInstance;
        InitalPosition = localTransform;

        // Check if there are any available pooled projectiles or need to create new ones
        if (totalProjectilesCreated < maxProjectiles)
        {
            // Create new projectile if under max limit
            projectileInstance = Instantiate(ProjectilePrefab, localTransform.position, Quaternion.identity);
            totalProjectilesCreated += 1;
            currentProjectiles++;
        }
        else if (pooledProjectiles.Count > 0)
        {
            Debug.LogError($"Recycling a projectile {pooledProjectiles}");
            // Reuse from the pool
            projectileInstance = pooledProjectiles.Dequeue();
            projectileInstance.SetActive(true);
            projectileInstance.GetComponent<Renderer>().enabled = true;
            projectileInstance.GetComponent<Collider>().enabled = true;
            currentProjectiles++;
        }
        else
        {
            return null;
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

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; 
        }

        // Disable the projectile and add it back to the pool
        //obj.SetActive(false);
        obj.transform.position = InitalPosition.position;
        pooledProjectiles.Enqueue(obj);

        currentProjectiles--;
    }
}
