using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private Transform projectileTarget;

    private EnemyProjectileManager projectileManager;

    private float elapsedTime = 0f;

    private bool isGameActive = GlobalSettings.projectileSpawnerActive;
    private float ShotDelay() { return Mathf.Round(Random.Range(2.0f , 5.0f) * 100) / 100; }

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
        HandlePausedTime();
        GameObject[] allBombs = GameObject.FindGameObjectsWithTag("Knockback");
        foreach (GameObject obj in allBombs)
        {
            if (obj && obj.GetComponent<ProjectileCollisionHandler>() == null)
            {
                Destroy(obj);
            }
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
            SpawnProjectile();
        }
    }

    public void SpawnProjectile()
    {
        GameObject projectileInstance = projectileManager.RequestProjectile(transform);
        // takes the created projectile from projectileManager and shoots it at the player
        if (projectileInstance != null)
        {
            Transform spawnPosition = gameObject.transform;
            projectileInstance.transform.position = spawnPosition.position;

            Vector3 directionToPlayer = (projectileTarget.position - spawnPosition.position).normalized; // directs the projectile towards the player

            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();

            BaseProjectile baseProj = projectileInstance.GetComponent<BaseProjectile>();

            projectileRb.velocity = directionToPlayer * baseProj.travelSpeed;
        }
    }
}