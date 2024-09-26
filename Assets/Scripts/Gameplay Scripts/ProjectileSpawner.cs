using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public Transform playerTransform;
    public Transform spawnPosition;

    public float spawnInterval = 2f; // Time between spawns
    public float projectileSpeed = 10f; 

    public int maxProjectiles = 5; 
    private int currentProjectiles = 0;

    public bool isGameActive = true;

    private void Start()
    {
        // Start spawning projectiles
        InvokeRepeating(nameof(SpawnProjectile), 0f, spawnInterval);
    }

    private void SpawnProjectile()
    {
        // Stop spawning if gameplay isn't active
        if (!isGameActive) return;

        if (currentProjectiles < maxProjectiles)
        {
            // Fire projectile from location of spawnPosition
            GameObject projectileInstance = Instantiate(ProjectilePrefab, spawnPosition.position, Quaternion.identity);

            Vector3 directionToPlayer = (playerTransform.position - spawnPosition.position).normalized;

            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            projectileRb.velocity = directionToPlayer * projectileSpeed;

            // Attach ProjectileCollisionHandler and pass reference to this spawner
            ProjectileCollisionHandler collisionHandler = projectileInstance.AddComponent<ProjectileCollisionHandler>();
            collisionHandler.SetSpawner(this); // give reference to spawner

            currentProjectiles++;
        }
    }

    public void OnProjectileInactive()
    {
        currentProjectiles--;
    }

    public void ToggleSpawning(bool isActive)
    {
        isGameActive = isActive;
    }
}

