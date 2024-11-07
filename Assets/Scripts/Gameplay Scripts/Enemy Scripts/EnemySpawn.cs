using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private EnemyWaveTrigger waveTrigger;
    private Transform MissZoneTransform;
    public GameObject enemyPrefab;
    private Transform[] enemySpawnPositions;
    private Transform InitalPosition = null;

    List<Transform> spawnPositionsList = new List<Transform>();

    public bool IsAlive = true;

    bool spawnPositionsAssigned;

    private void Start()
    {
        waveTrigger = GameObject.Find("Wave Start Trigger").GetComponent<EnemyWaveTrigger>();
        MissZoneTransform = GameObject.Find("Miss Zone").transform;
        if (MissZoneTransform == null) 
        { 
            Debug.LogError($"Miss Zone cannot be found, Miss Zone is {MissZoneTransform.gameObject}");
        }
        enemySpawnPositions = new Transform[0];
        spawnPositionsAssigned = false;
    }

    private void Update()
    {
        if (!IsAlive)
        {
            EnemyFire fireScript = GetComponent<EnemyFire>();
            if (fireScript != null)
            {
                fireScript.ToggleFiring(false);
                MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
                if (meshRenderer.enabled)
                {
                    meshRenderer.enabled = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        EnemyFire fireScript = GetComponent<EnemyFire>();
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
        if (/*!spawnPositionsAssigned && */enemySpawnPositions != null && spawnPositionsList != null)
        {
            spawnPositionsList = enemySpawnPositions.ToList();
            foreach (var position in waveTrigger.SpawnPositions)
            {
                spawnPositionsList.Add(position);
            }
            enemySpawnPositions = spawnPositionsList.ToArray();
            spawnPositionsAssigned = true;
            Debug.Log($"What the hecks value is {enemySpawnPositions.Length}");

            if (enemySpawnPositions == null || enemySpawnPositions.Length == 0)
            {
                Debug.LogError($"enemySpawnPositions is {enemySpawnPositions.Length}");
                Debug.LogError($"enemySpawnPositions is {enemySpawnPositions}");
                Debug.LogError("No valid spawn positions");
            }
            else
            {

            }
            IsAlive = true;

            GameObject enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            InitalPosition = transform;

            Transform spawnPosition = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Length)];
            enemyInstance.transform.position = spawnPosition.position;
            enemyInstance.transform.rotation = Quaternion.identity;

            if (spawnPositionsList.Count > 0)
            {
                spawnPositionsList.Remove(transform);
                enemySpawnPositions = spawnPositionsList.ToArray();
            }
            else
            {
                Debug.Log($"Remaining spawn points {spawnPositionsList.Count}");
                Debug.LogError($"No more spawn points for enemies to spawn at, there are {waveTrigger.SpawnPositions.Length} left");
            }
        }
    }
}