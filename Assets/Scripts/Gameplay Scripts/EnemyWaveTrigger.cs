using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTriggerNewWave;
    private void Start()
    {
        Debug.Log("Wave script test");
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("PlayerBody"))
        {
            Debug.Log("Player has triggered a new wave");
            OnPlayerEnterTriggerNewWave?.Invoke(this, EventArgs.Empty);
        }
    }
}
