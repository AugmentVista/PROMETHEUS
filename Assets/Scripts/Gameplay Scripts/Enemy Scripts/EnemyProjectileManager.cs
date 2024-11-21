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
    public Queue<GameObject> pooledProjectiles = new Queue<GameObject>(); // Queue to hold inactive projectiles


    public GameObject RequestProjectile(Transform localTransform)
    {
        GameObject projectileInstance; // declared undefined
        InitalPosition = localTransform;
        WaveUI waveUI = FindObjectOfType<WaveUI>();

        if (totalProjectilesCreated < maxProjectiles)
        {
            Debug.Log($"waveUI is {waveUI}");
            FireProjectile(localTransform); // creates projectileInstance and assigns it
            projectileInstance = FireProjectile(localTransform);
        }
        else if (waveUI.waveCount > 0)
        {
            Debug.Log($"waveUI is {waveUI}"); 
            if (totalProjectilesCreated >= maxProjectiles && currentProjectiles == 0)
            {
                waveUI.NextWave();
                maxProjectiles += waveUI.waveCount * 2;
            }
            FireProjectile(localTransform);
            projectileInstance = FireProjectile(localTransform);
            return projectileInstance;
        }
        else
        {
            projectileInstance = FireProjectile(localTransform);
            Debug.Log($"waveUI is {waveUI}");
            return null;
        }
        return projectileInstance;
    }


    public void ZeroProjectilesRemaining()
    { 
    
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
