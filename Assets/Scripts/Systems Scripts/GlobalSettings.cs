using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    #region PlayerHealthSystem Variables

    public static float globalPlayerHPMaximum = 100f;

    #endregion

    #region TimerController Variables

    public static float globalTimerDuration = 25f;

    #endregion

    #region ScoreKeeper Variables

    public static int globalScore = 0;

    public static int globalDrachma = 0;

    #endregion

    #region LevelManager Variables

    public static bool globalPlayerWin = false;

    public static bool globalPlayerLose = false;

    #endregion

    #region PlayerAttackHitBox Variables

    public static float globalPlayerAttackDuration = 0.25f;

    public static float globalPlayerSecondsBetweenAttacks = 0.25f;

    #endregion

    #region ProjectileCollisionHandler Variables

    public static float stoneProjectileDamage = 10f;

    public static float knockbackProjectileDamage = 10f;

    public static float stunProjectileDamage = 5f;

    public static float slowProjectileDamage = 12f;

    #endregion

    #region ProjectileSpawner Variables

    public static float spawnerProjectileSpeed = 1000f;

    public static float spawnerSecondsBetweenAttacks = 1.0f; 

    public static int spawnerProjectilesMaxAmount = 50;

    public static bool projectileSpawnerActive = true;

    #endregion

    #region Game_Manager Variables
    public enum GlobalGameState { MainMenu, GamePlay1, GameOver, GameWin }
    public static GlobalGameState globalGameState;
    public static bool globalPauseOverride = false;
    #endregion


}
