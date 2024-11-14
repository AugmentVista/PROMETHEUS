using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//public class Wave : MonoBehaviour
//{
//    [SerializeField] private EnemySpawn enemySpawn;
//    [SerializeField] EndWaveTrigger nextWaveTrigger;
//    [SerializeField] private List<BaseEnemy> spawnCount = new List<BaseEnemy>();
//    [SerializeField] private List<GameObject> livingEnemies = new List<GameObject>();
//    private bool initialWaveStarted = false;
//    [SerializeField] private int WaveCount => GlobalSettings.globalWaveCount + 3;

//    public void SpawnEnemies()
//    {
//        Debug.LogError($"WAVE COUNT IS: {WaveCount}");
//        if (enemySpawn != null)
//        {
//            for (int i = 0; i < WaveCount; i++)
//            {
//                enemySpawn.Spawn(1);
//                AddNewEnemiesToList();
//                Debug.Log($"spawnCount length is: {spawnCount.Count}");
//                Debug.Log(spawnCount.ToString());
//            }
//            DetectAliveEnemies();
//        }
//        else
//        {
//            Debug.Log($"EnemySpawn is {enemySpawn.isActiveAndEnabled}");
//        }
//    }

//    private void AddNewEnemiesToList()
//    {
//        // Retrieve all active BaseEnemy instances.
//        BaseEnemy[] allEnemies = FindObjectsOfType<BaseEnemy>();

//        // Loop through all found enemies and add only new ones to spawnCount.
//        foreach (BaseEnemy enemy in allEnemies)
//        {
//            if (!spawnCount.Contains(enemy))
//            {
//                spawnCount.Add(enemy);
//            }
//        }
//    }

//    public void DetectAliveEnemies()
//    {
//        if (spawnCount.Count > 0)
//        {
//            Debug.Log($"Detected {spawnCount.Count} enemies spawned in");
//        }
//        else
//        {
//            Debug.LogError("No spawnCount is broken");
//            return;
//        }

//        foreach (BaseEnemy enemy in spawnCount)
//        {
//            if (enemy != null)
//            {
//                Debug.Log($"{enemy.gameObject.name} is {enemy.GetComponent<BaseEnemy>().IsAlive}.");

//                if (!livingEnemies.Contains(enemy.gameObject))
//                {
//                    livingEnemies.Add(enemy.gameObject);
//                }

//                foreach (GameObject obj in livingEnemies)
//                {
//                    if (!enemy.GetComponent<BaseEnemy>().IsAlive)
//                    {
//                        Debug.Log($"{enemy.gameObject.name} is {enemy.GetComponent<BaseEnemy>().IsAlive}.");
//                        if (!livingEnemies.Contains(enemy.gameObject))
//                        {
//                            livingEnemies.Remove(enemy.gameObject);
//                        }
//                    }
//                    else if (enemy.GetComponent<BaseEnemy>().IsAlive)
//                    {
//                        Debug.LogError($"Living Enemies Remaining {livingEnemies.Count}");
//                    }
//                    IsWaveOver();
//                }
//            }
//            else
//            {
//                Debug.Log("Enemy is null");
//            }
//        }
//        initialWaveStarted = true;
//    }

//    public bool IsWaveOver()
//    {
//        if (initialWaveStarted)
//        {
//            if (livingEnemies.Count < 1 && livingEnemies != null)
//            {
//                Debug.Log("No enemies remain, ending wave");
//                nextWaveTrigger.Alt_WaveEnd_ShowResults();
//                //spawnCount.Clear();
//                //livingEnemies.Clear();
//                return true;
//            }
//            else
//            {
//                Debug.LogError(livingEnemies.Count + " Alive enemies list count");
//                return false;
//            }
//        }
//        else
//        {
//            return false;
//        }
//    }
//}