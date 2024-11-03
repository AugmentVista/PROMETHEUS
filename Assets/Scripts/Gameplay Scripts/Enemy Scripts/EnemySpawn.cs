using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private EnemyWaveTrigger waveTrigger;


    public Transform MissZoneTransform;

    public GameObject enemyPrefab;

    public Transform[] enemySpawnPositions;

    private Transform InitalPosition = null;

    public bool IsAlive;

    private void Start()
    {   
        if(waveTrigger != null)
        enemySpawnPositions = waveTrigger.SpawnPositions;
        Debug.LogError($"Enemy spawn pos is {enemySpawnPositions}");
        Debug.LogError($"Enemy spawn count is {enemySpawnPositions.Length}");
    }
    
    /// <summary>
    /// enemy details here 
    /// public event EventHandler OnDead;
    /// EnemyMain enemyMain
    /// awake 
    /// start
    /// enemy healthsystem
    /// bool IsAlive
    /// enemy 
    /// </summary>

    public void Spawn()
    {
        IsAlive = true;

        if (waveTrigger != null)
            enemySpawnPositions = waveTrigger.SpawnPositions;
        Debug.LogError($"Enemy spawn pos is {enemySpawnPositions}");
        Debug.LogError($"Enemy spawn count is {enemySpawnPositions.Length}");

        GameObject enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        InitalPosition = transform;
        //enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        Debug.Log($"enemy is {enemyInstance}");

        Transform spawnPosition = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Length)];
        enemyInstance.transform.position = spawnPosition.position;

        Debug.Log($"Enemy spawn array length is {enemySpawnPositions.Length} and enemy itself is {enemyInstance}");
        
        Vector3 directionToPlayer = (MissZoneTransform.position - spawnPosition.position).normalized;

        Debug.Log("A enemy has been spawned");
    }
}
