using System;
using System.Linq;
using UnityEngine;

public class EnemySpawnPositionDisable : MonoBehaviour
{
    public EnemyWaveTrigger waveTrigger;

    // not used by anything - see comment below
    public Transform[] SpawnPositions;

    private void Start()
    {
        waveTrigger = FindAnyObjectByType<EnemyWaveTrigger>();

        // is the second waveTrigger.SpawnPositions means to be *this* class' SpawnPositions var?
        waveTrigger.SpawnPositions = waveTrigger.SpawnPositions.ToList().ToArray();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("MissZone"))
        {
            // Ensure SpawnPositions is a List so you can remove elements dynamically
            var spawnPositionsList = waveTrigger.SpawnPositions.ToList();

            // Remove this object's Transform from the SpawnPositions list
            if (spawnPositionsList.Contains(transform))
            {
                spawnPositionsList.Remove(transform);
                waveTrigger.SpawnPositions = spawnPositionsList.ToArray(); // Update the array
            }
        }
        else if (collider.CompareTag("Weapon"))
        {
            EnemySpawn thisEnemy = GetComponent<EnemySpawn>();
            thisEnemy.IsAlive = false;
        }
    }
}