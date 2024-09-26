using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject ProjectilePrefab; // Prefab for the projectile
    public Transform playerTransform; // Reference to the player's position
    public Transform spawnPosition; // Transform reference for the spawn position

    public float spawnInterval = 2f; // Time between spawns
    public float projectileSpeed = 10f; // Speed at which projectiles move

    public int maxProjectiles = 5; // Maximum number of active projectiles
    private int currentProjectiles = 0;

    public bool isGameActive = true;

    private void Start()
    {
        // Start spawning projectiles at regular intervals
        InvokeRepeating(nameof(SpawnProjectile), 0f, spawnInterval);
    }

    private void SpawnProjectile()
    {
        // Check if the game is active and spawning is allowed
        if (!isGameActive) return;

        if (currentProjectiles < maxProjectiles)
        {
            // Use the spawnPosition object's position to determine where to spawn the projectile
            GameObject projectileInstance = Instantiate(ProjectilePrefab, spawnPosition.position, Quaternion.identity);

            // Get direction towards the player
            Vector3 directionToPlayer = (playerTransform.position - spawnPosition.position).normalized;

            // Set the projectile's velocity to move towards the player
            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            projectileRb.velocity = directionToPlayer * projectileSpeed;

            // Attach ProjectileCollisionHandler and pass reference to this spawner
            ProjectileCollisionHandler collisionHandler = projectileInstance.AddComponent<ProjectileCollisionHandler>();
            collisionHandler.SetSpawner(this); // Pass the spawner reference

            currentProjectiles++;
        }
    }

    public void OnProjectileInactive()
    {
        // Call this method when a projectile is destroyed or set inactive
        currentProjectiles--;
    }

    public void ToggleSpawning(bool isActive)
    {
        // Method to enable or disable spawning
        isGameActive = isActive;
    }
}

