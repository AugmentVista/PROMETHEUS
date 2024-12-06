using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private Transform projectileTarget;

    private EnemyProjectileManager projectileManager;

    private float elapsedTime = 0f;

    List <GameObject> legitimateProjectiles = new List<GameObject>();

    private bool isGameActive = GlobalSettings.projectileSpawnerActive;
    private float ShotDelay() { return Mathf.Round(Random.Range(2.0f , 5.0f) * 100) / 100; } // produces clean decimals

    private float FiringCooldown;
    float lastShotSpeed = 4.75f;



    private void Start()
    {
        projectileManager = FindObjectOfType<EnemyProjectileManager>();
        projectileTarget = GameObject.Find("Miss Zone").transform;
        FiringCooldown = ShotDelay()/2;
    }

    public void ToggleFiring(bool isActive)
    {
        GlobalSettings.projectileSpawnerActive = isActive;
    }

    private void LateUpdate()
    {
        CheckPermissionToFire();
        GameObject[] allBombs = GameObject.FindGameObjectsWithTag("Knockback");
        foreach (GameObject obj in allBombs)
        {
            if(obj && obj.GetComponent<ProjectileCollisionHandler>() == null)
            { 
                Destroy(obj);
            }
        }
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
        if (elapsedTime > FiringCooldown && isGameActive)
        {
            if (lastShotSpeed > ShotDelay()) // ShotDelay is faster
            {
                lastShotSpeed = lastShotSpeed - 0.25f;
                FiringCooldown += ShotDelay();
            }
            else if (lastShotSpeed < ShotDelay()) // ShotDelay is slower
            {
                FiringCooldown += lastShotSpeed;
            }
            Debug.LogWarning($"ShotDelay: {ShotDelay()} from {gameObject.name}");
            Debug.LogWarning($"lastShotSpeed: {lastShotSpeed} from {gameObject.name}");
            SpawnProjectile();
        }
    }

    public void SpawnProjectile()
    {
        if ( isGameActive )
        {
            GameObject projectileInstance = projectileManager.RequestProjectile(transform);
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
                if (projectileInstance != null)
                {
                    ProjectileCollisionHandler test = projectileInstance.GetComponent<ProjectileCollisionHandler>();
                    if (test == null)
                    { 
                        legitimateProjectiles.Add(projectileInstance);
                        Debug.Log($"Fake Bomb count: {legitimateProjectiles.Count}, this is a fake bomb");
                    }
                }
            }
        }
    }
}