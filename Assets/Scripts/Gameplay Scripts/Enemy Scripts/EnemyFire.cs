using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private Transform projectileTarget; // The player or other target
    private EnemyWaveTrigger waveTrigger;
    public Transform[] SpawnPositions => waveTrigger?.SpawnPositions;

    private int AmmunitionLifespan = 25;
    public int AmmunitionConsumed = 0;

    private float standardSpeed;
    private Vector3 memoryOfPlayer;
    private bool firstShotHasBeenFired = false;
    private float ShotDelay() { return Mathf.Round(Random.Range(0.5f, 1.0f) * 100) / 100; } // produces clean decimals

    private EnemyProjectileManager projectileManager;
    private bool isGameActive = GlobalSettings.projectileSpawnerActive;

    private void Start()
    {
        waveTrigger = FindObjectOfType<EnemyWaveTrigger>();
        projectileManager = FindObjectOfType<EnemyProjectileManager>(); // Reference the manager
        projectileTarget = GameObject.Find("Miss Zone").transform;
        if (isGameActive)
        {
            InvokeRepeating("SpawnProjectile", 2.0f, ShotDelay());
        }
    }


    private void Update()
    {
        CheckPermissionToFire();
    }

    private void CheckPermissionToFire()
    {
        if (GlobalSettings.globalPauseOverride || !GlobalSettings.projectileSpawnerActive)
        {
            isGameActive = false;
        }

        if (isGameActive)
        {
            InvokeRepeating("SpawnProjectile", 2.0f, ShotDelay());
        }
        else
        {
            CancelInvoke("SpawnProjectile");
        }
    }

    public void SpawnProjectile()
    {
        if (isGameActive && AmmunitionConsumed < AmmunitionLifespan)
        {
            GameObject projectileInstance = projectileManager.RequestProjectile(transform); // Get a projectile from the manager
            if (projectileInstance != null)
            {
                Transform spawnPosition = SpawnPositions[Random.Range(0, SpawnPositions.Length)];
                projectileInstance.transform.position = spawnPosition.position;

                Vector3 directionToPlayer = (projectileTarget.position - spawnPosition.position).normalized;
                memoryOfPlayer = directionToPlayer;


                Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();

                BaseProjectile baseProj = projectileInstance.GetComponent<BaseProjectile>();
                standardSpeed = baseProj.travelSpeed;

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
                AmmunitionConsumed += 1;
                firstShotHasBeenFired = true;
            }
        }
    }

    public void ToggleFiring(bool isActive)
    {
        isGameActive = isActive;
    }
}
