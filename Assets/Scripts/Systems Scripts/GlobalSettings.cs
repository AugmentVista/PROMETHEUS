using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static bool EnemiesAreAlive = true;

    public static float globalWaveCount = 0;

    #region PlayerHealthSystem Variables

    public static float globalPlayerHPMaximum = 100f;

    #endregion

    #region TimerController Variables

    public static float globalTimerDuration = 99f;

    #endregion

    #region ScoreKeeper Variables

    public static int globalScore = 0;

    public static int globalDrachma = 0;

    #endregion

    #region LevelManager Variables

    public static bool globalPlayerWin = false;

    public static bool globalPlayerLose = false;

    #endregion


    #region Player Movement Variables

        #region fpc MonoBehaviour

    public static float globalSprintSpeed = 10f;
    public static float globalSprintDuration = 5f;
    public static float globalSprintCooldown = globalSprintDuration/2;

    #endregion

            #region fpc Editor

        public static float globalMaxSprintSpeed = 40f;
        public static float globalMinWalkSpeed = 2.5f;

        #endregion

    #endregion


    #region ProjectileSpawner Variables

    public static int spawnerProjectilesMaxAmount = 20; // this is each?

    public static bool projectileSpawnerActive = true;

    #endregion

    #region Game_Manager Variables

    public static bool globalPauseOverride = false;

    #endregion
}