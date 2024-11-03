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
        GameObject enemyInstance;
        InitalPosition = transform;

        enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        Transform spawnPosition = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Length)];
        enemyInstance.transform.position = spawnPosition.position;

        // Calculate direction to target (e.g., player)
        Vector3 directionToPlayer = (MissZoneTransform.position - spawnPosition.position).normalized;

        Debug.Log("A enemy has been spawned");
    }
}
