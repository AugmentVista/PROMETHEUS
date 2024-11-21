using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public GameObject[] Projectiles;
    private WaveUI waveUI;
    private int maxProjectiles = 10;
    public Transform InitalPosition = null;

    private int currentProjectiles = 0;
    public int totalProjectilesCreated = 0;
    int localWaveCount = 0;
    public Queue<GameObject> pooledProjectiles = new Queue<GameObject>(); // Queue to hold inactive projectiles


    public GameObject RequestProjectile(Transform localTransform)
    {
        GameObject projectileInstance; // declared undefined
        WaveUI waveUI = FindObjectOfType<WaveUI>();
        InitalPosition = localTransform;

        if (totalProjectilesCreated < maxProjectiles)
        {
            FireProjectile(localTransform); // creates projectileInstance and assigns it
            projectileInstance = FireProjectile(localTransform);
        }
        else if (totalProjectilesCreated >= maxProjectiles) // has hit the max number of projectiles
        {
            Debug.Log(waveUI.waveCount);
            if (localWaveCount < waveUI.waveCount)
            {
                totalProjectilesCreated = 0;   
                waveUI.NextWave();
                maxProjectiles += waveUI.waveCount * 2;
                localWaveCount++;
            }
            return null;
        }
        else
        {
            return null; // nothing to provide to enemyFire
        }
        return projectileInstance;
    }

    public void RotateProjectilePool()

    {
        switch (waveUI.waveCount)
        {
            case 1:
                Debug.Log("Projectile Rotator online");
                break;
            case 3:
                
                break; 
            default:
                Debug.Log("Rotator has exceeded current wave limits");
                break;
        }
        Debug.Log(ProjectilePrefab);
    }

    private int GenerateNewProjectiles()
    {
        Debug.Log("can i have one debug log please?");
        int p = 0;
        int randomNumber = Random.Range(1, 101);
        switch (randomNumber)
        {
            case int i when (i >= 1 /*+ (waveUI.waveCount * 10)*/ && i <= 50):
                Debug.Log("First Result");

                ProjectilePrefab = Projectiles[0];
                p = 0;

                Debug.Log(ProjectilePrefab.name);
                break;

            case int i when (i /*+ (waveUI.waveCount*10)*/ >= 51):
                Debug.Log("Second Result");

                ProjectilePrefab = Projectiles[0];
                p = 1;

                Debug.Log(ProjectilePrefab.name);
                break;
        }
        Debug.Log(p + " is the value of p");
        return p;
    }

    GameObject FireProjectile(Transform localTransform)
    {
        GameObject projectileInstance;
        InitalPosition = localTransform;
        Debug.Log(GenerateNewProjectiles());
        projectileInstance = Instantiate(Projectiles[GenerateNewProjectiles()], localTransform.position, Quaternion.identity);
        totalProjectilesCreated += 1;
        currentProjectiles++;
        return projectileInstance;
    }

    public void ReturnProjectile(GameObject obj)
    {
        ProjectileCollisionHandler handler = obj.GetComponent<ProjectileCollisionHandler>();
        if (handler != null)
        {
            handler.struckByWeapon = false;
            handler.reusedProjectile = true;
        }

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; 
        }

        obj.transform.position = InitalPosition.position;
        pooledProjectiles.Enqueue(obj);

        currentProjectiles--;
    }
}
