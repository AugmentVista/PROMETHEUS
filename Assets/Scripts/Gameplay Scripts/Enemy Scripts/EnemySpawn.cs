using System.Linq;
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
        EnemyFire fireScript = GetComponent<EnemyFire>();
        if (collider.CompareTag("MissZone"))
        {
            if (fireScript != null)
            {
                fireScript.ToggleFiring(false);
            }
            IsAlive = false;
            // Remove this transform from the waveTrigger's SpawnPositions
            if (waveTrigger.SpawnPositions.Contains(transform))
            {
                var spawnPositionsList = waveTrigger.SpawnPositions.ToList();
                spawnPositionsList.Remove(transform);
                waveTrigger.SpawnPositions = spawnPositionsList.ToArray();
            }
        }
        else if (collider.CompareTag("Weapon"))
        {
            IsAlive = false;
            if (fireScript != null)
            {
                fireScript.ToggleFiring(false);
            }
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer.enabled)
            {
                meshRenderer.enabled = false;
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
        if (waveTrigger.SpawnPositions.Length > 0)
        {
            var spawnPositionsList = waveTrigger.SpawnPositions.ToList();
            spawnPositionsList.Remove(transform);
            waveTrigger.SpawnPositions = spawnPositionsList.ToArray();
        }
        else
        {
            Debug.LogError($"No more spawn points for enemies to spawn at, there are {waveTrigger.SpawnPositions.Length} left");
        }
        Debug.Log("An enemy has been spawned");
    }
}

