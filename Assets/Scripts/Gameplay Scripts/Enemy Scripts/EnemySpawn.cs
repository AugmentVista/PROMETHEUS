using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private EnemyWaveTrigger waveTrigger;
    public Transform MissZoneTransform;
    public GameObject enemyPrefab;
    private Transform[] enemySpawnPositions => waveTrigger?.SpawnPositions;
    private Transform InitalPosition = null;
    public bool IsAlive;

    private void Start()
    {
        if (waveTrigger == null)
        {
            return;
        }

        if (enemySpawnPositions == null || enemySpawnPositions.Length == 0)
        {
            return;
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("MissZone"))
        { 
            EnemyFire fireScript = GetComponent<EnemyFire>();
            if (fireScript != null)
            { 
                fireScript.enabled = false;
                fireScript.ToggleFiring(false);
            }
        }
    }

    public void Spawn()
    {
        if (enemySpawnPositions == null || enemySpawnPositions.Length == 0)
        {
            Debug.LogError("No valid spawn positions");
            return;
        }

        IsAlive = true;

        GameObject enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        InitalPosition = transform;
        Debug.Log($"Enemy instance created: {enemyInstance}");

        Transform spawnPosition = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Length)];
        enemyInstance.transform.position = spawnPosition.position;
        enemyInstance.transform.rotation = Quaternion.identity;

        Debug.Log($"Enemy spawned at position: {spawnPosition.position}");

        Debug.Log("An enemy has been spawned");
    }
}

