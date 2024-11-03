using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamOnDisable : MonoBehaviour
{
    public Camera playerCamera;



    private void OnEnable()
    {
        playerCamera.targetDisplay = 0;
    }

    private void OnDisable()
    {
        playerCamera.targetDisplay = 1;   
    }
}
