using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private Transform projectileTarget; // The player or other target

    private EnemyWaveTrigger waveTrigger;

    private EnemyProjectileManager projectileManager;

    private float elapsedTime = 0f;

    private bool isGameActive = GlobalSettings.projectileSpawnerActive;
    private float ShotDelay() { return Mathf.Round(Random.Range(5.5f, 10.0f) * 100) / 100; } // produces clean decimals

    private float FiringCooldown;



    private void Start()
    {
        waveTrigger = FindObjectOfType<EnemyWaveTrigger>();
        projectileManager = FindObjectOfType<EnemyProjectileManager>(); // Reference the manager
        projectileTarget = GameObject.Find("Miss Zone").transform;
        if (isGameActive)
        {
            //InvokeRepeating("SpawnProjectile", 2.0f, ShotDelay());
        }
        FiringCooldown = ShotDelay();
    }


    private void TimedShots()
    {
        if (elapsedTime > FiringCooldown)
        {
            FiringCooldown += ShotDelay();
            SpawnProjectile();
        }
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

    public void SpawnProjectile()
    {
        if (isGameActive /*&& AmmunitionConsumed < AmmunitionLifespan*/)
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
        }
    }

    public void ToggleFiring(bool isActive)
    {
        GlobalSettings.projectileSpawnerActive = isActive;
    }
}
