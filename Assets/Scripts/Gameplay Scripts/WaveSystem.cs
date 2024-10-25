using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waveArray;
    [SerializeField] private EnemyWaveTrigger waveTrigger;

    private State state;

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

    private void EnemyWaveTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        if (state == State.Idle)
        { 
            StartWave();

            waveTrigger.OnPlayerEnterTrigger -= EnemyWaveTrigger_OnPlayerEnterTrigger;
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
                // Wave is over
            }
            else
            {
                // Wave not over
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Represents a single Enemy Spawn Wave
    /// </summary>

    [System.Serializable]
    private class Wave 
    {
        [SerializeField] private EnemySpawn[] enemySpawnArray;
        [SerializeField] private float timer;

        public void Update() 
        {
            if (timer >= 0)
            { 
                timer -= Time.deltaTime;
                if (timer <= 0)
                { 
                    SpawnEnemies();
                }
            }
        } 

        private void SpawnEnemies()
        {
            foreach (EnemySpawn enemySpawn in enemySpawnArray)
            {
                enemySpawn.Spawn();
            }
        }

        public bool IsWaveOver()
        {
            if (timer < 0)
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
