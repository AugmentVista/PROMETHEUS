using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private Transform missZoneTransform;
    public GameObject enemyPrefab;
    private Transform initalPosition = null;

    private EnemyFire fireScript;
    private MeshRenderer meshRenderer;

    [SerializeField] private GameObject SpawnTransformParent;

    List<Transform> enemySpawnPositions = new List<Transform>();

    private GameObject enemyInstance;

    private Transform spawnPosition;

    public bool IsAlive = true;

    bool spawnPositionsAssigned =  false;

   
    private void Start()
    {
        fireScript = enemyPrefab.GetComponent<EnemyFire>();
        meshRenderer = enemyPrefab.GetComponent<MeshRenderer>();
        enemyInstance
    }
    public void SetDead()
    {
        IsAlive = false;
        spawnPositionsAssigned = false;

        if (fireScript != null)
        {
            fireScript.ToggleFiring(false);
        }

        if (meshRenderer.enabled)
        {
            meshRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("MissZone") || collider.CompareTag("Weapon"))
        {
            SetDead();
        }
    }

    public void Spawn()
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
            // Check if the prefab is set
            if (enemyPrefab == null)
            {
                Debug.LogError("Enemy prefab is not assigned in the inspector.");
                return;
            }

            if (enemySpawnPositions.Count > 0)
            {
                // Instantiate enemy
                enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                initalPosition = transform;

                // Get random spawn position and set the enemy's position
                spawnPosition = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Count)];
                enemyInstance.transform.position = spawnPosition.position;
                enemyInstance.transform.rotation = Quaternion.identity;

                // Remove the spawn position to avoid reusing it
                //enemySpawnPositions.Remove(spawnPosition);

                //Debug.Log($"Remaining spawn points: {enemySpawnPositions.Count}");
            }
        } 
    }
}