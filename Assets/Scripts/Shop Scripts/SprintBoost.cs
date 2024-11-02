using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SprintBoost : MonoBehaviour
{
    public void IncreaseSprint(float boost)
    {
        Debug.Log("Increase Sprint was called.");
        FirstPersonController FPScontroller = FindObjectOfType<FirstPersonController>(true);
        Debug.Log(FPScontroller);
        if (FPScontroller != null)
        {
            FPScontroller.UpgradeSpeed(boost);
            Debug.Log($"Sprint speed is: {FPScontroller.sprintSpeed}");
        }
    }

    public void IncreaseStamina(float boost)
    {
        Debug.Log("Increase Stamina was called.");
        FirstPersonController FPScontroller = FindObjectOfType<FirstPersonController>(true);
        Debug.Log(FPScontroller);
        if (FPScontroller != null)
        {
            FPScontroller.UpgradeDuration(boost);
            Debug.Log($"Stamina is:  {FPScontroller.sprintDuration}");
        }
    }
}
