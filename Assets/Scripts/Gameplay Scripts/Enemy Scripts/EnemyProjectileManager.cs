using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    private WaveUI waveUI;
    private int maxProjectiles = 5;
    public Transform InitalPosition = null;

    private int currentProjectiles = 0;
    public int totalProjectilesCreated = 0;
    public Queue<GameObject> pooledProjectiles = new Queue<GameObject>(); // Queue to hold inactive projectiles

    public GameObject RequestProjectile(Transform localTransform)
    {
        GameObject projectileInstance;
        InitalPosition = localTransform;
        WaveUI waveUI = FindObjectOfType<WaveUI>();
       

        if (totalProjectilesCreated < maxProjectiles)
        {
            Debug.Log($"waveUI is {waveUI}");
            //projectileInstance = Instantiate(ProjectilePrefab, localTransform.position, Quaternion.identity);
            //totalProjectilesCreated += 1;
            //currentProjectiles++;
            FireProjectile(localTransform);
        }
        //else if (pooledProjectiles.Count > 0)
        //{
        //    Debug.Log($"Recycling a projectile {pooledProjectiles}");
        //    // Reuse from the pool
        //    projectileInstance = pooledProjectiles.Dequeue();
        //    projectileInstance.SetActive(true);
        //    projectileInstance.GetComponent<Renderer>().enabled = true;
        //    projectileInstance.GetComponent<Collider>().enabled = true;
        //    currentProjectiles++;
        //}
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
            Debug.Log($"waveUI is {waveUI}");
            return null;
        }
        projectileInstance = FireProjectile(localTransform);
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
