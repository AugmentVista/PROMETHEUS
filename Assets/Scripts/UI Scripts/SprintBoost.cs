using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SprintBoost : MonoBehaviour
{
    public FirstPersonController controller;
    public float boost;

    public void IncreaseSprint()
    {
        GlobalSettings.globalMaxSprintSpeed += boost;
        GlobalSettings.globalSprintSpeed = boost;
        Debug.Log($"Sprint speed is {GlobalSettings.globalSprintSpeed}");
        Debug.Log($"Real Sprint speed is {controller.sprintSpeed}");

        Debug.Log(GlobalSettings.globalMaxSprintSpeed.ToString());
    }
}
