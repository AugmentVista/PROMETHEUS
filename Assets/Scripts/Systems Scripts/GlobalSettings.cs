using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{

    public static int globalWaveCount = 1;

    #region PlayerHealthSystem Variables

    public static float globalPlayerHPMaximum = 100f;

    #endregion

    #region ScoreKeeper Variables

    public static int globalDrachma = 1;

    #endregion

    #region LevelManager Variables

    public static bool globalPlayerWin = false;

    public static bool globalPlayerLose = false;

    #endregion

    #region City Variables

    public static float globalCityMaxHP = 100f;

    #endregion

    #region Weapon and Item Variables

    public static float globalBlockerHealth = 0f;

    #endregion

    
    #region ProjectileSpawner Variables

    public static bool projectileSpawnerActive = true;

    #endregion

    #region Game_Manager Variables

    public static bool globalPauseOverride = false;

    #endregion
}