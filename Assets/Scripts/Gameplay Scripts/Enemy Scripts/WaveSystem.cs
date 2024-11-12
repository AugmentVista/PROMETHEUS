using System;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waveArray;
    [SerializeField] private Wave wave;
    [SerializeField] private EnemyWaveTrigger waveTrigger;
    [SerializeField] private EndWaveTrigger endWaveTrigger;
    [SerializeField] private State state;
    [SerializeField] private bool waveStarted = false;
    [SerializeField] private int waveIndexer = -1;

    public float waveCount;

    private State previousState;
    private enum State
    { 
        Idle, 
        Active,
        BattleOver,
        SecondIdle,
    }

    private void Awake()
    {
       state = State.Idle;
    }

    void Start()
    {
        wave = waveArray[0];
        waveTrigger.OnPlayerEnterTrigger += EnemyWaveTrigger_OnPlayerEnterTrigger;
    }

    private void EndWaveTrigger_WaveEnd_ShowResults(object sender, EventArgs _)
    {
        endWaveTrigger.WaveEnd_ShowResults -= EndWaveTrigger_WaveEnd_ShowResults;
        waveTrigger.OnPlayerEnterTrigger += EnemyWaveTrigger_OnPlayerEnterTrigger;
        if (state == State.BattleOver) 
        {
            state = State.SecondIdle;
            Debug.Log($"Current state is {state}");
        }
    }

    private void EnemyWaveTrigger_OnPlayerEnterTrigger(object sender, EventArgs _) // _  is for events that don’t require information beyond the event occurring.
    {
        if (state == State.Idle)
        {
            state = State.Active;

            waveTrigger.OnPlayerEnterTrigger -= EnemyWaveTrigger_OnPlayerEnterTrigger;

            endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults; 
        }
        else if (state == State.SecondIdle)
        {
            state = State.Active;

            waveTrigger.OnPlayerEnterTrigger -= EnemyWaveTrigger_OnPlayerEnterTrigger; 

            endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults; 
        }
    }

    private void Update()
    {
        int waveIndex = Mathf.FloorToInt(GlobalSettings.globalWaveCount); // updates, any changes to state will be seen
        CheckAndRunOnStateChange(state, waveIndex);
        if (state == State.BattleOver) { Debug.LogError("THE BATTLE HAS FINISHED"); }
    }

    private void CheckAndRunOnStateChange(State currentState, int waveIndexReference)
    {
        if (currentState != previousState)
        {
            Debug.Log($"This should not be running every frame");
            WaveStateMachine(waveIndexReference);
            previousState = currentState;
        }
        else { return; }
    }

    private void WaveStateMachine(int waveIndex)
    {
        switch (state)
        {
            case State.Active:
                StartWave();
                TestBattleOver(); // sets state to BattleOver
                break;
            case State.BattleOver:
                if (waveArray.Length > waveIndex)
                {
                    Wave wave = waveArray[waveIndex]; // increments waveArray when the battle ends
                }
                break;
            case State.SecondIdle:
                BeginNewWave(waveIndex);
                TestBattleOver();
                break;
        }
    }


    private void StartWave()
    {
        if (!waveStarted)
        {
            wave = waveArray[0];

            if (state == State.Active)
            {
                waveStarted = true;
                Debug.Log($"StartWave Spawned Enemies");
                wave.SpawnEnemies();
            }
        }
        else { return; }
    }

    public void BeginNewWave(int waveIndex)
    {
        wave = waveArray[waveIndex];
        if (waveIndexer < waveIndex)
        {
            if (state == State.Active)
            {
                Debug.Log($"BeginNewWave Spawned Enemies");
                wave.SpawnEnemies();
                waveIndexer += 1;
            }
        }
    }

    private void TestBattleOver()
    {
        if (state == State.Active)
        {
            wave.DetectAliveEnemies();
            if (AreWavesOver())
            { 
                state = State.BattleOver;
                GlobalSettings.globalWaveCount++;
                Debug.Log($"Battle is {state}");
            }
        }
    }

    private bool AreWavesOver()
    {
        Debug.Log("is this even running? - AreWavesOver"); // does run
        if (wave.IsWaveOver() == true)
        {
            // wave over
            Debug.Log("WAVE IS OVER from AreWavesOver"); // does not run
            return true;
        }
        else
        {
            // Wave not over
            return false;
        }
    }


    /// <summary>
    /// Represents a single Enemy Spawn Wave
    /// </summary>

    [System.Serializable]
    private class Wave 
    {
        [SerializeField] private EnemySpawn[] enemySpawnArray;
        [SerializeField] private EnemySpawn[] spawnCount;
        [SerializeField] private float waveCount => GlobalSettings.globalWaveCount;

        public void RespawnEnemies()
        {
            foreach (EnemySpawn enemySpawn in enemySpawnArray)
            {
                //enemySpawn.Respawn();
            }
        }
        public void SpawnEnemies()
        {
            foreach (EnemySpawn enemySpawn in enemySpawnArray)
            {
                if (enemySpawn == null)
                {
                    Debug.LogError("One of the enemy spawn positions is null!");
                    continue;
                }
                enemySpawn.Spawn();
            }

            // Track spawned enemies once, after spawning them
            DetectAliveEnemies();
        }

        public void DetectAliveEnemies()
        {
            spawnCount = FindObjectsOfType<EnemySpawn>();

            if (spawnCount.Length > 0)
            {
                Debug.Log($"Detected {spawnCount.Length} spawnCounts!");
            }
            else
            {
                Debug.LogError("No spawnCounts found!");
            }

            foreach (EnemySpawn enemySpawn in spawnCount)
            {
                if (enemySpawn != null)
                {
                    Debug.Log("Enemy is not null");
                    if (enemySpawn.IsAlive)
                    {
                        Debug.Log($"{enemySpawn.gameObject.name} is alive.");
                    }
                    else
                    {
                        Debug.Log($"{enemySpawn.gameObject.name} is dead.");
                    }
                }
                else
                {
                    Debug.Log("Enemy is null");
                }
            }
        }

        public bool IsWaveOver()
        {
            if (spawnCount != null && spawnCount.Length > 0)
            {
                foreach (EnemySpawn enemySpawn in spawnCount)
                {
                    if (enemySpawn != null)
                    {
                        Debug.Log($"{enemySpawn.gameObject.name} IsAlive: {enemySpawn.IsAlive}");
                        if (enemySpawn.IsAlive)
                        {
                            return false; // Wave is not over yet
                        }
                    }
                }
                Debug.Log("WAVE IS OVER");
                return true; // All enemies are dead, wave over
            }
            else
            {
                // Enemies have not spawned yet
                return false;
            }
        }
    }
}
