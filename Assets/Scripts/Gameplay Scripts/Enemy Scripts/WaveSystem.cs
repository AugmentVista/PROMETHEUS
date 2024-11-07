using System;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waveArray;
    [SerializeField] private EnemyWaveTrigger waveTrigger;
    [SerializeField] private EndWaveTrigger endWaveTrigger;
    private State state;
    public float waveCount;
    private enum State
    { 
        Idle, 
        Active,
        BattleOver,
    }

    private void Awake()
    {
       state = State.Idle;
    }

    void Start()
    {
        waveTrigger.OnPlayerEnterTrigger += EnemyWaveTrigger_OnPlayerEnterTrigger;
    }

    private void EndWaveTrigger_WaveEnd_ShowResults(object sender, EventArgs _)
    {
        if (state == State.BattleOver) 
        {
            state = State.Idle; 
        }
    }

    private void EnemyWaveTrigger_OnPlayerEnterTrigger(object sender, EventArgs _) // _  is for events that don’t require information beyond the event occurring.
    {
        if (state == State.Idle)
        {
            StartWave();
            // unsub to avoid multiple triggers from the same source
            waveTrigger.OnPlayerEnterTrigger -= EnemyWaveTrigger_OnPlayerEnterTrigger; // turn off starter

            endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults; // turn on ender
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.Active:
                foreach (Wave wave in waveArray)
                {
                    wave.Update();
                }
                TestBattleOver();
                break;
            case State.BattleOver:


                break;
        }
    }

    private void StartWave()
    {
        Debug.Log("Wave is starting");

        state = State.Active;
    }

    private void TestBattleOver()
    {
        if (state == State.Active)
        {
            if (AreWavesOver())
            { 
                // Battle is over
                state = State.BattleOver;
                Debug.Log($"Battle is {state}");
            }
        }
    }

    private bool AreWavesOver()
    {
        foreach (Wave wave in waveArray)
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
        return true;
    }

    public void BeginNewWave()
    {
        Debug.Log("Has a new wave begun?");
        endWaveTrigger.WaveEnd_ShowResults -= EndWaveTrigger_WaveEnd_ShowResults;
        waveTrigger.OnPlayerEnterTrigger += EnemyWaveTrigger_OnPlayerEnterTrigger;
        foreach (Wave wave in waveArray)
        {
            //wave.DestroyAllEnemies();
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
        [SerializeField] private bool moveToNextWave = false;

        public void Update() 
        {
            float lastWaveCount = 0;
            if (lastWaveCount < waveCount)
            { 
                lastWaveCount = waveCount;
                if (!GlobalSettings.globalPauseOverride)
                {
                    SpawnEnemies();
                }
            }
        }

        public void DestroyAllEnemies()
        {
            foreach (EnemySpawn enemySpawn in enemySpawnArray)
            {
                enemySpawn.gameObject.SetActive(false);
            }
        }

        public void ReviveEnemies()
        {
            foreach (EnemySpawn enemySpawn in enemySpawnArray)
            {
                enemySpawn.IsAlive = true;
            }
        }

        private void SpawnEnemies()
        {
            foreach (EnemySpawn enemySpawn in enemySpawnArray)
            {
                enemySpawn.Spawn();
            }
            GlobalSettings.globalWaveCount++;
            
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
