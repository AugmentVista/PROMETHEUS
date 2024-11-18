using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;

    public GameObject lv2EnemyPrefab;

    public GameObject lv3EnemyPrefab;

    private Transform initalPosition = null;

    [SerializeField] private GameObject SpawnTransformParent;

    List<Transform> enemySpawnPositions = new List<Transform>();

    public List<GameObject> totalEnemyInstances = new List<GameObject>();

    public GameObject enemyInstance;

    private Transform spawnPosition;

    bool spawnPositionsAssigned =  false;

    public void Spawn(int amountToSpawn)
    {
        SpawnTransformParent = GameObject.Find("Spawn Positions"); // this works

        // Only assign spawn positions once
        if (!spawnPositionsAssigned)
        {
            foreach (Transform child in SpawnTransformParent.transform)
            {
                enemySpawnPositions.Add(child.transform);
                spawnPositionsAssigned = true;
            }

            if (enemySpawnPositions.Count == 0)
            {
                Debug.LogError("No valid spawn positions available.");
                return;
            }
        }
        else
        {
            if (enemyPrefab == null)
            {
                Debug.LogError("Enemy prefab is not assigned in the inspector.");
                return;
            }

            for (int i = 0; i < amountToSpawn; i++)
            {
                if (enemySpawnPositions.Count > 0)
                {
                    enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                    initalPosition = transform;

                    spawnPosition = enemySpawnPositions[UnityEngine.Random.Range(0, enemySpawnPositions.Count)];
                    enemyInstance.transform.position = spawnPosition.position;
                    enemyInstance.transform.rotation = Quaternion.identity;
                }
                totalEnemyInstances.Add(enemyInstance);
            }
        } 
    }

    public void ClearEnemyInstanceList()
    {
        totalEnemyInstances.Clear();
    }
    public GameObject GetEnemyInstance()
    { 
        return enemyInstance;
    }
}