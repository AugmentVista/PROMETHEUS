using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public Transform projectileTarget; // The player or other target
    public Transform[] spawnPositions; // Array to hold multiple spawn positions
    public float spawnInterval = GlobalSettings.spawnerSecondsBetweenAttacks;

    private EnemyProjectileManager projectileManager;
    private bool isGameActive = GlobalSettings.projectileSpawnerActive;

    private void Start()
    {
        projectileManager = FindObjectOfType<EnemyProjectileManager>(); // Reference the manager
        if (isGameActive)
        {
            InvokeRepeating(nameof(SpawnProjectile), 0f, spawnInterval);
        }
    }

    public void SpawnProjectile()
    {
        if (isGameActive)
        {
            GameObject projectileInstance = projectileManager.RequestProjectile(); // Get a projectile from the manager
            if (projectileInstance != null)
            {
                // Select a random spawn position for the projectile
                Transform spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];
                projectileInstance.transform.position = spawnPosition.position;

                // Calculate direction to target (e.g., player)
                Vector3 directionToPlayer = (projectileTarget.position - spawnPosition.position).normalized;

                // Apply velocity to the projectile
                Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
                BaseProjectile baseProj = projectileInstance.GetComponent<BaseProjectile>();
                projectileRb.velocity = directionToPlayer * baseProj.travelSpeed;

                // Ensure a ProjectileCollisionHandler is attached and setup properly
                ProjectileCollisionHandler collisionHandler = projectileInstance.GetComponent<ProjectileCollisionHandler>();
                if (collisionHandler == null)
                {
                    collisionHandler = projectileInstance.AddComponent<ProjectileCollisionHandler>();
                }

                collisionHandler.SetSpawner(projectileManager);
                if (!collisionHandler.reusedProjectile)
                {
                    collisionHandler.reusedProjectile = true;
                }
            }
        }
    }

    public void ToggleFiring(bool isActive)
    {
        isGameActive = isActive;
        if (isGameActive)
        {
            InvokeRepeating(nameof(SpawnProjectile), 0f, spawnInterval);
        }
        else
        {
            CancelInvoke(nameof(SpawnProjectile));
        }
    }
}
