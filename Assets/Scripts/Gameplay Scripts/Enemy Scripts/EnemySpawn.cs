using System.Linq;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private EnemyWaveTrigger waveTrigger;
    private Transform MissZoneTransform;
    public GameObject enemyPrefab;
    private Transform[] enemySpawnPositions;
    private Transform InitalPosition = null;
    public bool IsAlive = true;

    private void Start()
    {
        EnemyWaveTrigger waveTrigger = GameObject.Find("Wave Start Trigger").GetComponent<EnemyWaveTrigger>();
        Transform MissZoneTransform = GameObject.Find("Miss Zone").transform;
        if (MissZoneTransform == null) { Debug.LogError($"Miss Zone cannot be found, Miss Zone is {MissZoneTransform.gameObject}"); }
        if (waveTrigger == null)
        {
            Debug.LogError("waveTrigger is null");
        }

        if (enemySpawnPositions == null || enemySpawnPositions.Length == 0)
        {
            Debug.Log("enemySpawnPositions are null");
        }
        var spawnPositionsList = waveTrigger.SpawnPositions.ToList();
        foreach (var position in waveTrigger.SpawnPositions) 
        {
            spawnPositionsList.Add(position);
        }
        enemySpawnPositions = spawnPositionsList.ToArray();
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

            //// Remove this transform from the waveTrigger's SpawnPositions
            //if (waveTrigger.SpawnPositions.Contains(transform))
            //{
            //    var spawnPositionsList = waveTrigger.SpawnPositions.ToList();
            //    spawnPositionsList.Remove(transform);
            //    waveTrigger.SpawnPositions = spawnPositionsList.ToArray();
            //}
        }
        else if (collider.CompareTag("Weapon"))
        {
            IsAlive = false;
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

        Transform spawnPosition = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Length)];
        enemyInstance.transform.position = spawnPosition.position;
        enemyInstance.transform.rotation = Quaternion.identity;

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
    }
}

