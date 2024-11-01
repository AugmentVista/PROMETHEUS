using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SprintBoost : MonoBehaviour
{
    public FirstPersonController FPScontroller;
    FirstPersonControllerEditor FPSeditor;

    private void Awake()
    {
        FPScontroller = FindObjectOfType<FirstPersonController>(true);
        FPSeditor = FindObjectOfType<FirstPersonControllerEditor>(true);
    }

    public void IncreaseSprint(float boost)
    {
        if (FPScontroller != null && FPSeditor != null)
        {
            GlobalSettings.globalMaxSprintSpeed += boost;
            GlobalSettings.globalSprintSpeed = boost;
            FPScontroller.UpdateUpgrades();
            FPSeditor.UpdateEditorUpgrade();
            Debug.Log($"Sprint speed is {GlobalSettings.globalSprintSpeed}");
            Debug.Log($"Real Sprint speed is {FPScontroller.sprintSpeed}");

            Debug.Log(GlobalSettings.globalMaxSprintSpeed.ToString());
        }
        
    }
}
