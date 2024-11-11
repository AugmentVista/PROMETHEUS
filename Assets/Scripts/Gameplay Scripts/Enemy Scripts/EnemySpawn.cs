using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private EnemyWaveTrigger waveTrigger;
    private Transform missZoneTransform;
    public GameObject enemyPrefab;
    private Transform initalPosition = null;

    List<Transform> enemySpawnPositions = new List<Transform>();

    public bool IsAlive = true;

    bool spawnPositionsAssigned =  false;

    private void InitStart()
    {
        IsAlive = true;
        waveTrigger = GameObject.Find("Wave Start Trigger").GetComponent<EnemyWaveTrigger>();
        if (missZoneTransform == null)
        {
            Debug.LogError($"waveTrigger cannot be found, waveTrigger is {waveTrigger.gameObject}");
            return;
        }
        missZoneTransform = GameObject.Find("Miss Zone").transform;
        if (missZoneTransform == null)
        {
            Debug.LogError($"Miss Zone cannot be found, Miss Zone is {missZoneTransform.gameObject}");
            return;
        }
        EnemyFire fireScript = enemyPrefab.GetComponent<EnemyFire>();
        MeshRenderer meshRenderer = enemyPrefab.GetComponent<MeshRenderer>();
    }


    private void Update()
    {
        if (!IsAlive)
        {
            EnemyFire fireScript = enemyPrefab.GetComponent<EnemyFire>();
            if (fireScript != null)
            {
                fireScript.ToggleFiring(false);
                MeshRenderer meshRenderer = enemyPrefab.GetComponent<MeshRenderer>();
                if (meshRenderer.enabled)
                {
                    meshRenderer.enabled = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        collider = enemyPrefab.GetComponent<Collider>();
        if (collider.CompareTag("MissZone"))
        {
            IsAlive = false;
        }
        else if (collider.CompareTag("Weapon"))
        {
            IsAlive = false;
        }
    }

    public void Spawn()
    {
        waveTrigger = GameObject.Find("Wave Start Trigger").GetComponent<EnemyWaveTrigger>(); // this works
        if (waveTrigger == null)
        {
            Debug.LogError("Cannot spawn. WaveTrigger is not initialized.");
            return;
        }

        // Only assign spawn positions once
        if (!spawnPositionsAssigned)
        {
            // Ensure spawn positions are added
            foreach (Transform position in waveTrigger.SpawnPositions)
            {
                if (!enemySpawnPositions.Contains(position))
                {
                    enemySpawnPositions.Add(position);
                }
                spawnPositionsAssigned = true;
            }
            Debug.Log($"Spawn positions count: {enemySpawnPositions.Count}");

            if (enemySpawnPositions.Count == 0)
            {
                Debug.LogError("No valid spawn positions available.");
                return;
            }
        }
        else
        {
            // Check if the prefab is set
            if (enemyPrefab == null)
            {
                Debug.LogError("Enemy prefab is not assigned in the inspector.");
                return;
            }
            // If no spawn points left, log a warning
            if (enemySpawnPositions.Count > 0)
            {
                // Instantiate enemy
                GameObject enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                initalPosition = transform;

                // Get random spawn position and set the enemy's position
                Transform spawnPosition = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Count)];
                enemyInstance.transform.position = spawnPosition.position;
                enemyInstance.transform.rotation = Quaternion.identity;

                // Remove the spawn position to avoid reusing it
                enemySpawnPositions.Remove(spawnPosition);

                Debug.Log($"Remaining spawn points: {enemySpawnPositions.Count}");
            }

        }

        
    }
}