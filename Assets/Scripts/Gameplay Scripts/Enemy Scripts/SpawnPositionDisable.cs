using System;
using System.Linq;
using UnityEngine;

public class SpawnPositionDisable : MonoBehaviour
{
    public EnemyWaveTrigger waveTrigger;
    public Transform[] SpawnPositions;

    private void Start()
    {
        waveTrigger = FindAnyObjectByType<EnemyWaveTrigger>();

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