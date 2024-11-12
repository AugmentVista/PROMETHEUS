using System;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waveArray;
    [SerializeField] private Wave wave;
    [SerializeField] private EnemyWaveTrigger waveTrigger;
    [SerializeField] private EndWaveTrigger endWaveTrigger;
    private State state;
    public float waveCount;
    private bool waveStarted = false;
    private int waveIndexer = 0;
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
        Wave wave = waveArray[0];
        waveTrigger.OnPlayerEnterTrigger += EnemyWaveTrigger_OnPlayerEnterTrigger;
    }

    private void EndWaveTrigger_WaveEnd_ShowResults(object sender, EventArgs _)
    {
        endWaveTrigger.WaveEnd_ShowResults -= EndWaveTrigger_WaveEnd_ShowResults;
        waveTrigger.OnPlayerEnterTrigger += EnemyWaveTrigger_OnPlayerEnterTrigger;
        if (state == State.BattleOver) 
        {
            state = State.SecondIdle; 
        }
    }

    private void EnemyWaveTrigger_OnPlayerEnterTrigger(object sender, EventArgs _) // _  is for events that don’t require information beyond the event occurring.
    {
        if (state == State.Idle)
        {
            state = State.Active;
            // unsub to avoid multiple triggers from the same source
            waveTrigger.OnPlayerEnterTrigger -= EnemyWaveTrigger_OnPlayerEnterTrigger; // turn off starter

            endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults; // turn on ender
        }
        else if (state == State.SecondIdle)
        {
            state = State.Active;
            waveTrigger.OnPlayerEnterTrigger -= EnemyWaveTrigger_OnPlayerEnterTrigger; // turn off starter

            endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults; // turn on ender
        }
    }

    private void Update()
    {
        int waveIndex = Mathf.FloorToInt(GlobalSettings.globalWaveCount); // updates, any changes to state will be seen
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
            Wave wave = waveArray[0];

            if (state == State.Active)
            {
                wave.SpawnEnemies();
                waveStarted = true;
            }
        }
    }

    public void BeginNewWave(int waveIndex)
    {
        wave = waveArray[waveIndex];
        if (waveIndexer < waveIndex)
        {
            if (state == State.Active)
            {
                wave.SpawnEnemies();
                waveIndexer += 1;
            }
        }
    }

    private void TestBattleOver()
    {
        if (state == State.Active)
        {
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
        if (wave.IsWaveOver())
        {
            // wave over
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
        [SerializeField] private float waveCount => GlobalSettings.globalWaveCount;
        private float lastWaveCount = 0;

        public void RespawnEnemies()
        {
            foreach (EnemySpawn enemySpawn in enemySpawnArray)
            {
                enemySpawn.Respawn();
            }
        }

        public void SpawnEnemies()
        {
            foreach (EnemySpawn enemySpawn in enemySpawnArray)
            {
                enemySpawn.Spawn();
            }
        }
        public bool IsWaveOver()
        {
            float lastWave = 0;
            if (waveCount < lastWave )
            {
                // Wave spawned

                foreach (EnemySpawn enemySpawn in enemySpawnArray)
                {
                    if (enemySpawn.IsAlive)
                    {
                        return false;
                    }
                }
                return true;
            }
            else 
            {
                // Enemies have not spawned yet
                return false;
            }
        }
    }
}
