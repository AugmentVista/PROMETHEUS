using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private Transform projectileTarget; // The player or other target

    public Transform[] spawnPositions; // Array to hold multiple spawn positions

    [SerializeField] private int AmmunitionLifespan = 100;
    [SerializeField] private int Ammunition = 0;


    private float spawnInterval = 1.0f;

    private float ShotDelay() { return Mathf.Round(Random.Range(0.5f, 1.0f) * 100) / 100; } // produces clean decimals
    private float shootingTimeGap = 3.0f - GlobalSettings.globalWaveCount / 10f; 

    private EnemyProjectileManager projectileManager;
    private bool isGameActive = GlobalSettings.projectileSpawnerActive;

    private void Start()
    {
        projectileManager = FindObjectOfType<EnemyProjectileManager>(); // Reference the manager
        if (isGameActive)
        {
            shootingTimeGap = ShotDelay();
            InvokeRepeating(nameof(SpawnProjectile), 0f, spawnInterval + shootingTimeGap);
        }
    }

    public void SpawnProjectile()
    {
        if (isGameActive)
        {
            
            GameObject projectileInstance = projectileManager.RequestProjectile(transform); // Get a projectile from the manager
            if (projectileInstance != null)
            {

                Transform spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];
                projectileInstance.transform.position = spawnPosition.position;


                Vector3 directionToPlayer = (projectileTarget.position - spawnPosition.position).normalized;


                Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
                BaseProjectile baseProj = projectileInstance.GetComponent<BaseProjectile>();
                projectileRb.velocity = directionToPlayer * baseProj.travelSpeed;


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
                Ammunition += 1;
            }
        }
        if (Ammunition >= AmmunitionLifespan)
        { 
            ToggleFiring(false);
            EnemySpawn thisEnemy = GetComponent<EnemySpawn>();
            if (thisEnemy != null)
            { 
                thisEnemy.IsAlive = false;
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
