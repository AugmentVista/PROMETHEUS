using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyProjectileManager : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public GameObject[] Projectiles;
    private WaveUI waveUI;
    public int maxProjectiles = 10;
    public int initalProjectiles;
    public Transform InitalPosition = null;

    private int currentProjectiles = 0;
    public int totalProjectilesCreated = 0;
    int localWaveCount = 0;
    public Queue<GameObject> pooledProjectiles = new Queue<GameObject>(); // Queue to hold inactive projectiles

    private void Start()
    {
        initalProjectiles = maxProjectiles;
    }
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
            Debug.Log(waveUI.waveCount + "th Wave");
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

    
    private int GenerateNewProjectiles()
    {
        int p = 0;
        int randomNumber = Random.Range(1, 101);
        switch (randomNumber)
        {
            case int i when (i >= 1 /*+ (waveUI.waveCount * 10)*/ && i <= 50):
                //Debug.Log("First Result");

                ProjectilePrefab = Projectiles[0];
                p = 0;

                //Debug.Log(ProjectilePrefab.name);
                break;

            case int i when (i /*+ (waveUI.waveCount*10)*/ >= 51):
                //Debug.Log("Second Result");

                ProjectilePrefab = Projectiles[0];
                p = 1;

                //Debug.Log(ProjectilePrefab.name);
                break;
        }
        //Debug.Log(p + " is the value of p");
        return p;
    }

    public void DestroyAllProjectiles()
    {
        GameObject[] allBombs = GameObject.FindGameObjectsWithTag("Knockback");

        foreach (GameObject obj in allBombs)
        { 
            Destroy(obj);
        }
        currentProjectiles = 0;
        totalProjectilesCreated = 0;
        localWaveCount = 0;
        maxProjectiles =  initalProjectiles;
        //Debug.LogError("All on screen bombs destroyed");
    }


    GameObject FireProjectile(Transform localTransform)
    {
        GameObject projectileInstance;
        InitalPosition = localTransform;
        //Debug.Log(GenerateNewProjectiles());
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
