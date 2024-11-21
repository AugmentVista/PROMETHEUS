using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private Transform projectileTarget;

    private EnemyWaveTrigger waveTrigger;

    private EnemyProjectileManager projectileManager;

    private float elapsedTime = 0f;

    private bool isGameActive = GlobalSettings.projectileSpawnerActive;
    private float ShotDelay() { return Mathf.Round(Random.Range(4.0f, 5.0f) * 100) / 100; } // produces clean decimals

    private float FiringCooldown = 1f;



    private void Start()
    {
        waveTrigger = FindObjectOfType<EnemyWaveTrigger>();
        projectileManager = FindObjectOfType<EnemyProjectileManager>(); // Reference the manager
        projectileTarget = GameObject.Find("Miss Zone").transform;
        FiringCooldown = ShotDelay();
    }

    public void ToggleFiring(bool isActive)
    {
        GlobalSettings.projectileSpawnerActive = isActive;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        CheckPermissionToFire();
    }

    private void CheckPermissionToFire()
    {
        if (GlobalSettings.globalPauseOverride || !GlobalSettings.projectileSpawnerActive)
        {
            isGameActive = false;
        }
        else
        {
            isGameActive = true;
            elapsedTime += Time.deltaTime;
            TimedShots();
        }
    }

    private void TimedShots()
    {
        if (elapsedTime > FiringCooldown)
        {
            FiringCooldown += ShotDelay();
            Debug.LogWarning($"ShotDelay is: {ShotDelay()}");
            SpawnProjectile();
        }
    }

    public void SpawnProjectile()
    {
        if ( isGameActive )
        {
            GameObject projectileInstance = projectileManager.RequestProjectile(transform); // Get a projectile from the manager
            if (projectileInstance != null)
            {
                Transform spawnPosition = gameObject.transform;
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
            }
            else if (projectileInstance == null && projectileManager.totalProjectilesCreated >= 5)
            { 
            
            }
        }
    }
}
