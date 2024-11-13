using System;
using System.Collections.Generic;
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
    [SerializeField] private int WaveCount => GlobalSettings.globalWaveCount - 1;

    private State previousState = State.Idle;
    private enum State
    { 
        Idle, 
        Active,
        BattleOver,
        SecondIdle,
        ActiveSecond,
    }

    private void Awake()
    {
       state = State.Idle;
    }

    void Start()
    {
        wave = waveArray[WaveCount];
        waveTrigger.OnPlayerEnterTrigger += EnemyWaveTrigger_OnPlayerEnterTrigger;
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
            state = State.ActiveSecond;

            waveTrigger.OnPlayerEnterTrigger -= EnemyWaveTrigger_OnPlayerEnterTrigger; 

            endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults; 
        }
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

    private void Update()
    {
        //int waveIndex = GlobalSettings.globalWaveCount; // updates, any changes to state will be seen
        CheckAndRunOnStateChange(state, WaveCount); // do i need this?
        Debug.Log($"WaveCount is: {WaveCount}");
    }

    private void CheckAndRunOnStateChange(State currentState, int waveIndexReference)
    {
        if (currentState != previousState)
        {
            Debug.Log($"This should not be running every frame");
            WaveStateMachine(waveIndexReference); // logs WaveCount value on the first frame after state is changed
            previousState = currentState;
        }
        else { return; }
    }

    private void WaveStateMachine(int waveIndex) // waveIndex is a captured value from the first frame after the last state change
    {
        switch (state)
        {
            case State.ActiveSecond:
                BeginNewWave(waveIndex);
                TestBattleOver();
                break;
            case State.Active:
                StartWave();
                TestBattleOver(); // sets state to BattleOver
                break;
            case State.BattleOver:
                if (waveArray.Length > waveIndex)
                {
                    wave = waveArray[waveIndex]; // increments waveArray when the battle ends
                }
                break;
            
        }
    }

    private void StartWave()
    {
        if (!waveStarted)
        {
            wave = waveArray[WaveCount];

            if (state == State.Active)
            {
                waveStarted = true;
                Debug.Log($"StartWave Spawned Enemies");
                wave.SpawnEnemies();
            }
        }
    }

    public void BeginNewWave(int waveIndex) 
        //waveIndex is a captured value from the first frame after the last state change
        // when would this captured value be less than the true WaveCount?
        // When state becomes BattleOver waveIndex is small
    {
        wave = waveArray[waveIndex];
        if (WaveCount < waveIndex)
        {
            Debug.Log($"BeginNewWave Spawned Enemies");
            wave.SpawnEnemies();
        }
    }

    private void TestBattleOver()
    {
        if (state == State.Active || state == State.ActiveSecond)
        {
            if (wave.IsWaveOver())
            { 
                if (waveArray.Length > WaveCount)
                {
                    GlobalSettings.globalWaveCount++;
                } // Increments WaveCount, then changes state
                state = State.BattleOver;
                Debug.Log($"Battle is {state}");
            }
        }
    }


    /// <summary>
    /// Represents a single Enemy Spawn Wave
    /// </summary>

    [Serializable]
    private class Wave 
    {
        [SerializeField] private EnemySpawn enemySpawn;
        [SerializeField] private List<BaseEnemy> spawnCount = new List<BaseEnemy>();
        [SerializeField] private List <bool> livingEnemies = new List<bool>();
        private GameObject Enemy;
        private bool initialWaveStarted = false;
        [SerializeField] private int WaveCount => GlobalSettings.globalWaveCount +3;

        public void SpawnEnemies()
        {
            Debug.LogError($"WAVE COUNT IS: {WaveCount}");
            if (enemySpawn != null)
            {
                for (int i = 0; i < WaveCount; i++)
                {
                    enemySpawn.Spawn(1);
                    AddNewEnemiesToList();
                    Debug.Log($"spawnCount length is: {spawnCount.Count}");
                    Debug.Log(spawnCount.ToString());
                }
            }
            else 
            {
                Debug.Log($"EnemySpawn is {enemySpawn.isActiveAndEnabled}");
            }

            // Track spawned enemies once, after spawning them
            DetectAliveEnemies();
        }

        private void AddNewEnemiesToList()
        {
            // Retrieve all active BaseEnemy instances.
            BaseEnemy[] allEnemies = FindObjectsOfType<BaseEnemy>();

            // Loop through all found enemies and add only new ones to spawnCount.
            foreach (BaseEnemy enemy in allEnemies)
            {
                if (!spawnCount.Contains(enemy))
                {
                    spawnCount.Add(enemy);
                }
            }
        }


        public void DetectAliveEnemies()
        {
            if (spawnCount.Count > 0)
            {
                Debug.Log($"Detected {spawnCount.Count} enemies spawned in");
            }
            else
            {
                Debug.LogError("No spawnCount is broken");
                return;
            }

            foreach (BaseEnemy enemy in spawnCount)
            {
                if (enemy != null)
                {
                    if (enemy.IsAlive)
                    {
                        Debug.Log($"{enemy.gameObject.name} is alive.");
                        livingEnemies.Add(enemy);
                    }
                    else
                    {
                        Debug.Log($"{enemy.gameObject.name} is dead.");
                        if (livingEnemies.Count > 0)
                        livingEnemies.Remove(enemy);
                    }
                    Debug.LogError($"Living Enemies Remaining {livingEnemies.Count}");
                    initialWaveStarted = true;
                }
                else
                {
                    Debug.Log("Enemy is null");
                }
            }
        }

        public bool IsWaveOver()
        {
            if (initialWaveStarted)
            {
                if (livingEnemies.Count != 0)
                {
                    return false;
                }
                else 
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
