using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private Transform projectileTarget;

    private EnemyProjectileManager projectileManager;
    private Game_Manager gameManager;

    private float elapsedTime = 0f;

    private bool isGameActive = GlobalSettings.projectileSpawnerActive;

    private float FiringCooldown;

    private float accelerationMulitplier = 1.00f;


    private float ShotDelay() 
    {
        return Mathf.Round(Random.Range(4.0f, 5f) * 100) / 100;
    }


    private void Start()
    {
        projectileManager = FindObjectOfType<EnemyProjectileManager>();
        gameManager = FindObjectOfType<Game_Manager>();
        projectileTarget = GameObject.Find("Miss Zone").transform;
        FiringCooldown = ShotDelay();
    }

    public void ToggleFiring(bool isActive)
    {
        GlobalSettings.projectileSpawnerActive = isActive;
    }

    private void Update()
    {
        HandlePausedTime();
        ResetFiringSpeed();
    }

    private void ResetFiringSpeed()
    {
        if (gameManager.gameState == Game_Manager.GameState.Upgrades)
        { 
            accelerationMulitplier = 1.00f;
            Debug.Log($"Gameplay state no longer active firing speed multiplier is {accelerationMulitplier}");
        }
    }

    private void HandlePausedTime()
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
        FiringCooldown = ShotDelay() * accelerationMulitplier;
        if (elapsedTime > FiringCooldown && isGameActive)
        {
            elapsedTime = 0.0f;
            SpawnProjectile();

            accelerationMulitplier *= 0.97f; // increase the rate of fire of next shot by 2%

            Debug.LogError($"Last shot had a speed interval of {FiringCooldown}");
        }
    }

    public void SpawnProjectile()
    {
        GameObject projectileInstance = projectileManager.RequestProjectile();
        // takes the created projectile from projectileManager and shoots it at the player
        if (projectileInstance != null)
        {
            Transform spawnPosition = gameObject.transform;
            projectileInstance.transform.position = spawnPosition.position; // sets the position of the obtained projectile to this gameObjects position

            Vector3 directionToPlayer = (projectileTarget.position - spawnPosition.position).normalized; // directs the projectile towards the player

            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();

            BaseProjectile baseProj = projectileInstance.GetComponent<BaseProjectile>();

            if (baseProj && projectileRb)
            {
                projectileRb.velocity = directionToPlayer * baseProj.travelSpeed;
            }
            else
            {
                Debug.Log("This one is broken");
            }
            
        }
    }
}