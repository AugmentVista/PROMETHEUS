using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private Transform projectileTarget; // The player or other target
    private EnemyWaveTrigger waveTrigger;
    public Transform[] SpawnPositions => waveTrigger?.SpawnPositions;

    private int AmmunitionLifespan = 25;
    private int Ammunition = 0;

    private float ShotDelay() { return Mathf.Round(Random.Range(0.5f, 1.0f) * 100) / 100; } // produces clean decimals

    private EnemyProjectileManager projectileManager;
    private bool isGameActive = GlobalSettings.projectileSpawnerActive;

    private void Start()
    {
        waveTrigger = FindObjectOfType<EnemyWaveTrigger>();
        projectileManager = FindObjectOfType<EnemyProjectileManager>(); // Reference the manager
        projectileTarget = GameObject.Find("Miss Zone").transform;
        Debug.LogWarning(projectileTarget);
        if (isGameActive)
        {
            InvokeRepeating("SpawnProjectile", 2.0f, ShotDelay());
        }
    }


    private void Update()
    {
        if (isGameActive)
        {
            InvokeRepeating("SpawnProjectile", 2.0f, ShotDelay());
        }
        else
        {
            CancelInvoke("SpawnProjectile");
            waveTrigger = FindObjectOfType<EnemyWaveTrigger>();
        }
    }
    public void SpawnProjectile()
    {
        if (isGameActive)
        {
            GameObject projectileInstance = projectileManager.RequestProjectile(transform); // Get a projectile from the manager
            if (projectileInstance != null)
            {
                Transform spawnPosition = SpawnPositions[Random.Range(0, SpawnPositions.Length)];
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
        GlobalSettings.projectileSpawnerActive = isActive;
    }
}
