using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//public class WaveSystem : MonoBehaviour
//{
//    [SerializeField] private Wave[] waveArray;
//    [SerializeField] private Wave wave;
//    [SerializeField] private EnemyWaveTrigger waveTrigger;
//    [SerializeField] private EndWaveTrigger endWaveTrigger;
//    [SerializeField] private State state;
//    [SerializeField] private bool waveStarted = false;
//    [SerializeField] private int WaveCount => GlobalSettings.globalWaveCount - 1;

//    private State previousState = State.Idle;
//    private enum State
//    {
//        Idle,
//        Active,
//        BattleOver,
//        SecondIdle,
//        ActiveSecond,
//    }

//    private void Awake()
//    {
//        state = State.Idle;
//        WaveStateMachine(state);
//    }

//    void Start()
//    {
//        wave = waveArray[WaveCount];
//        waveTrigger.OnPlayerEnterTrigger += EnemyWaveTrigger_OnPlayerEnterTrigger;
//    }

//    public void DetectDead()
//    {
//        wave.IsWaveOver();
//        Debug.LogError(wave.IsWaveOver().ToString());
//    }

//    private void EnemyWaveTrigger_OnPlayerEnterTrigger(object sender, EventArgs _) // _  is for events that don’t require information beyond the event occurring.
//    {
//        if (state == State.Idle)
//        {
//            state = State.Active;

//            waveTrigger.OnPlayerEnterTrigger -= EnemyWaveTrigger_OnPlayerEnterTrigger;

//            endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults;
//            WaveStateMachine(state);
//        }
//        else if (state == State.SecondIdle)
//        {
//            state = State.ActiveSecond;

//            waveTrigger.OnPlayerEnterTrigger -= EnemyWaveTrigger_OnPlayerEnterTrigger;

//            endWaveTrigger.WaveEnd_ShowResults += EndWaveTrigger_WaveEnd_ShowResults;
//            WaveStateMachine(state);
//        }
//    }

//    private void EndWaveTrigger_WaveEnd_ShowResults(object sender, EventArgs _)
//    {
//        endWaveTrigger.WaveEnd_ShowResults -= EndWaveTrigger_WaveEnd_ShowResults;
//        waveTrigger.OnPlayerEnterTrigger += EnemyWaveTrigger_OnPlayerEnterTrigger;
//        if (state == State.BattleOver)
//        {
//            state = State.SecondIdle;
//            WaveStateMachine(state);
//            if (waveArray.Length > WaveCount)
//            {
//                GlobalSettings.globalWaveCount++;
//            } // Increments WaveCount, then changes state
//            Debug.Log($"Current state is {state}");
//        }
//    }

//    private void Update()
//    {
//        //int waveIndex = GlobalSettings.globalWaveCount; // updates, any changes to state will be seen
//        //CheckAndRunOnStateChange(state, WaveCount); // do i need this?
//    }

//    //private void CheckAndRunOnStateChange(State currentState, int waveIndexReference)
//    //{
//    //    if (currentState != previousState)
//    //    {
//    //        Debug.Log($"This should not be running every frame");
//    //        WaveStateMachine(waveIndexReference); // logs WaveCount value on the first frame after state is changed
//    //        previousState = currentState;
//    //    }
//    //    else { return; }
//    //}

//    private void WaveStateMachine(State state) // waveIndex is a captured value from the first frame after the last state change
//    {
//        switch (state)
//        {
//            case State.ActiveSecond:
//                BeginNewWave(WaveCount);
//                TestBattleOver();
//                break;
//            case State.Active:
//                StartWave();
//                TestBattleOver(); // sets state to BattleOver
//                break;
//            case State.BattleOver:
//                if (waveArray.Length > WaveCount)
//                {
//                    wave = waveArray[WaveCount]; // increments waveArray when the battle ends
//                }
//                break;
//            case State.Idle:
//                // nothing 
//                break;

//        }
//    }

//    private void StartWave()
//    {
//        if (!waveStarted)
//        {
//            wave = waveArray[WaveCount];

//            if (state == State.Active)
//            {
//                waveStarted = true;
//                Debug.Log($"StartWave Spawned Enemies");
//                wave.SpawnEnemies();
//                WaveStateMachine(state);
//            }
//        }
//        else { return; }
//    }

//    public void BeginNewWave(int waveIndex)
//    //waveIndex is a captured value from the first frame after the last state change
//    // when would this captured value be less than the true WaveCount?
//    // When state becomes BattleOver waveIndex is small
//    {
//        wave = waveArray[waveIndex];
//        if (WaveCount < waveIndex)
//        {
//            Debug.Log($"BeginNewWave Spawned Enemies");
//            wave.SpawnEnemies();
//            WaveStateMachine(state);
//        }
//        else { return; }
//    }

//    private void TestBattleOver()
//    {
//        if (state == State.Active || state == State.ActiveSecond)
//        {
//            if (wave.IsWaveOver())
//            {
//                state = State.BattleOver;
//                WaveStateMachine(state);
//                Debug.Log($"Battle is {state}");
//            }
//        }
//    }

//}