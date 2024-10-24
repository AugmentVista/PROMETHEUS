using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    //[SerializeField] private //EnemySpawn[] enemySpawnArray;
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

        //foreach (EnemySpawn enemySpawn in enemySpawnArray)
        //{
        //    enemySpawn.Spawn();
        //}


    }



}
