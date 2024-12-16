using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public GameObject[] Projectiles;
    private WaveUI waveUI;
    [SerializeField] private int maxProjectiles = 12;

    private int currentProjectiles = 0;
    public int totalProjectilesCreated = 0;
    public int localWaveCount = 0;

    public GameObject RequestProjectile()
    {
        GameObject projectileInstance; // declared undefined

        waveUI = FindObjectOfType<WaveUI>();

        if (totalProjectilesCreated < maxProjectiles)
        {
            SpawnProjectile(); // creates projectileInstance and assigns it
            projectileInstance = SpawnProjectile();
            return projectileInstance;
        }
        else if (totalProjectilesCreated >= maxProjectiles) // has hit the max number of projectiles
        {
            Debug.Log(waveUI.waveCount + "th Wave");
            if (localWaveCount < waveUI.waveCount)
            {
                totalProjectilesCreated = 0;   
                waveUI.NextWave();
                maxProjectiles += 4;
                localWaveCount++;
            }
            return null;
        }
        else
        {
            return null; // nothing to provide to enemyFire
        }
    }

    GameObject SpawnProjectile()
    {
        GameObject projectileInstance;
        projectileInstance = Instantiate(Projectiles[DetermineProjectileType()], transform.position, Quaternion.identity);
        totalProjectilesCreated += 1;
        currentProjectiles += 1;
        return projectileInstance;
    }

    private int DetermineProjectileType()
    {
        int p = 0;
        int randomNumber = Random.Range(1 + localWaveCount, 101);
        switch (randomNumber)
        {
            case int i when (i >= 1 && i <= 50):

                ProjectilePrefab = Projectiles[0];
                p = 0;

                break;
            case int i when (i >= 51):

                ProjectilePrefab = Projectiles[0];
                p = 1;

                break;
        }
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
    }


    

}
