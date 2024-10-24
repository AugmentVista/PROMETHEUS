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

    private void StartWave()
    {
        Debug.Log("Wave is starting");

        state = State.Active;
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
                break;
        }
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

    }
}
