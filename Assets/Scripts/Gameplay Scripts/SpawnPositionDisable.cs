using System;
using System.Linq; // For .ToList()
using UnityEngine;

public class SpawnPositionDisable : MonoBehaviour
{
    public EnemyWaveTrigger waveTrigger;
    public event EventHandler OnPlayerEnterTrigger;
    public Transform[] SpawnPositions;

    private void Start()
    {
        // Find the EnemyWaveTrigger component in the scene
        waveTrigger = FindAnyObjectByType<EnemyWaveTrigger>();

        // Convert the array to a List if you need to modify it
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
    }
}