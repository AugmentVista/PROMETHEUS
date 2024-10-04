using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public Transform projectileTarget;
    public Transform[] spawnPositions; // Array to hold multiple spawn positions

    public float spawnInterval = 2f;
    public float projectileSpeed = 10f;
    public int maxProjectiles = 5;
    private int currentProjectiles = 0;

    private Queue<GameObject> pooledProjectiles = new Queue<GameObject>(); // Queue to hold inactive projectiles

    public bool isGameActive = true;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnProjectile), 0f, spawnInterval);
    }

    private void SpawnProjectile()
    {
        if (!isGameActive || currentProjectiles >= maxProjectiles) return;

        GameObject projectileInstance;

        // Check if there are any available pooled projectiles
        if (pooledProjectiles.Count > 0)
        {
            projectileInstance = pooledProjectiles.Dequeue();
            projectileInstance.SetActive(true);  // Reuse from the pool
        }
        else
        {
            // If no pooled projectiles, create a new one
            projectileInstance = Instantiate(ProjectilePrefab);
        }

        Transform spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

        projectileInstance.transform.position = spawnPosition.position;

        Vector3 directionToPlayer = (projectileTarget.position - spawnPosition.position).normalized;

        Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
        projectileRb.velocity = directionToPlayer * projectileSpeed;

        // Attach or reference the ProjectileCollisionHandler
        ProjectileCollisionHandler collisionHandler = projectileInstance.GetComponent<ProjectileCollisionHandler>();
        if (collisionHandler == null)
        {
            collisionHandler = projectileInstance.AddComponent<ProjectileCollisionHandler>();
        }

        collisionHandler.SetSpawner(this);
        collisionHandler.reusedProjectile = pooledProjectiles.Count > 0;

        currentProjectiles++;
    }

    // deactivate projectile and reset it
    public void OnProjectileInactive(GameObject obj)
    {
        obj.SetActive(false);
        pooledProjectiles.Enqueue(obj);  // put it back in the pool
        currentProjectiles--;
    }

    public void ToggleSpawning(bool isActive)
    {
        isGameActive = isActive;
    }
}
