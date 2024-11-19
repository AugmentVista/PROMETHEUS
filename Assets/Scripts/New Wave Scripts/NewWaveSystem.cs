//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewWaveSystem : MonoBehaviour
//{
//    [SerializeField] private EnemySpawn enemySpawn; // assign reference in Inspector

//    [SerializeField] private EnemyWaveTrigger waveTrigger;

//    [SerializeField] private EndWaveTrigger endWaveTrigger;

//    [SerializeField] private BaseEnemy enemyDeath;

//    [SerializeField] private GameObject Enemy; // enemy prefab object, not the spawned enemy in game

//    [SerializeField] private List<GameObject> enemyWaveInstance = new List<GameObject>();

//    [SerializeField] private List<List<GameObject>> allEnemyWaveInstances = new List<List<GameObject>>();

//    [SerializeField] private List<BaseEnemy> aliveEnemies = new List<BaseEnemy>();

//    [SerializeField] private List<BaseEnemy> deadEnemies = new List<BaseEnemy>();

//    [SerializeField] private bool isWaveRunning = false;

//    [SerializeField] private bool allEnemiesInWaveSlain = false;

//    [SerializeField] private int spawnBatchSize = 1;

//    public int currentWaveCount = 0;




//    void Start()
//    {
//        waveTrigger.OnPlayerEnterTrigger += EnemyWaveTrigger_OnPlayerEnterTrigger;
//        Enemy = enemySpawn.enemyPrefab;
//    }


//    public void WaveStateMachine()
//    {
//        switch (currentWaveCount)
//        {
//            case 0:
//                // wave 0 is handled by the event listener
//                break;
//            case 1:
//                waveTrigger.OnPlayerEnterTrigger -= EnemyWaveTrigger_OnPlayerEnterTrigger;
//                NewWave();
//                endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults;
//                break;
//            case 2:
//            case 3:
//            case 4:
//                NewWave();
//                endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults;
//                break;
//            case 5:
//            case 6:
//            case 7:
//                NewWave();
//                endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults;
//                break;
//            case 8:
//            case 9:
//            case 10:
//                NewWave();
//                endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults;
//                break;
//        }
//    }


//    public void NewWave()
//    {
//        Debug.Log("New Wave Trigger");
//        isWaveRunning = true;
//        enemySpawn.Spawn(spawnBatchSize);
//        enemyWaveInstance = new List<GameObject>(enemySpawn.totalEnemyInstances);

//        foreach (GameObject enemyObj in enemySpawn.totalEnemyInstances)
//        {
//            enemyWaveInstance.Add(enemyObj); // Add the enemy to the current wave instance list

//            BaseEnemy baseEnemy = enemyObj.GetComponent<BaseEnemy>();
//            if (baseEnemy != null)
//            {
//                baseEnemy.EnemyHasDied += EnemyWaveTrigger_OnPlayerEnterTrigger;
//            }
//        }

//        allEnemyWaveInstances.Add(enemyWaveInstance);

//        spawnBatchSize++;
//        Debug.Log("New Wave Finished");
//    }

//    private void FindAllDeadEnemies()
//    {
//        foreach (GameObject enemyObj in enemyWaveInstance)
//        {
//            BaseEnemy baseEnemy = enemyObj.gameObject.GetComponent<BaseEnemy>(); // Get the BaseEnemy component once per loop

//            // Check if the enemy is dead
//            if (!baseEnemy.IsAlive)
//            {
//                // Ensure the enemy isn't already in the deadEnemies list
//                if (!deadEnemies.Contains(baseEnemy))
//                {
//                    deadEnemies.Add(baseEnemy);
//                }
//            }
//            // If the enemy is alive
//            else
//            {
//                // Ensure the enemy isn't already in the aliveEnemies list
//                if (!aliveEnemies.Contains(baseEnemy))
//                {
//                    aliveEnemies.Add(baseEnemy);
//                }
//            }
//        if (aliveEnemies.Count == 0 && deadEnemies.Count > 0)
//        {
//            allEnemiesInWaveSlain = true;
//        }
//        else
//        {
//            allEnemiesInWaveSlain = false;
//        }

//        Debug.Log($"There are {aliveEnemies.Count} alive enemies and {deadEnemies.Count} dead enemies active");
//        Debug.Log($"Is every enemy in this wave dead? {allEnemiesInWaveSlain}");
//        }
//    }

//    private void WaveEndCheck() // clears temporary lists of enemy info
//    {
//        if (allEnemiesInWaveSlain)
//        {
//            foreach (GameObject enemyObj in enemySpawn.totalEnemyInstances)
//            {
//                enemyWaveInstance.Add(enemyObj); // Add the enemy to the current wave instance list

//                BaseEnemy baseEnemy = enemyObj.GetComponent<BaseEnemy>();
//                if (baseEnemy != null)
//                {
//                    baseEnemy.EnemyHasDied -= EnemyWaveTrigger_OnPlayerEnterTrigger;
//                }
//            }
//            isWaveRunning = false;
//            aliveEnemies.Clear();
//            deadEnemies.Clear ();
//            enemySpawn.ClearEnemyInstanceList();
//        }
//        else if (!allEnemiesInWaveSlain)
//        { 
//            isWaveRunning= true;
//        }
//    }

//    private void BaseEnemy_EnemyHasDied(object sender, EventArgs _)
//    {
//        FindAllDeadEnemies();
//        WaveEndCheck();
//    }



//    private void EnemyWaveTrigger_OnPlayerEnterTrigger(object sender, EventArgs _) // Start of Wave
//    {
//        NewWave();
//        endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults;
//        waveTrigger.OnPlayerEnterTrigger -= EnemyWaveTrigger_OnPlayerEnterTrigger;
//    }

    

//    private void EndWaveTrigger_WaveEnd_ShowResults(object sender, EventArgs _) // End of Wave
//    {
//        endWaveTrigger.WaveEnd_ShowResults -= EndWaveTrigger_WaveEnd_ShowResults;
//        currentWaveCount++;
//    }
//}
