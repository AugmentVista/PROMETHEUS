using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public GameObject[] Projectiles;
    private WaveUI waveUI;
    private int maxProjectiles = 5;
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
            Debug.LogError(waveUI.waveCount);
            if (localWaveCount < waveUI.waveCount)
            {
                totalProjectilesCreated = 0;   
                waveUI.NextWave();
                maxProjectiles += waveUI.waveCount * 2;
                localWaveCount++;
            }
            //FireProjectile(localTransform);
            //projectileInstance = FireProjectile(localTransform);
            //return projectileInstance; // hand over the projectile to EnemyFire
            return null;
        }
        else
        {
            return null; // nothing to provide to enemyFire
        }
        return projectileInstance;
    }


    GameObject FireProjectile(Transform localTransform)
    {
        GameObject projectileInstance;
        InitalPosition = localTransform;

        projectileInstance = Instantiate(ProjectilePrefab, localTransform.position, Quaternion.identity);
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
