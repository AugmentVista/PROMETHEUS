using System;
using UnityEngine;

public class EndWaveTrigger : MonoBehaviour
{
    public EventHandler WaveEnd_ShowResults;

    void Start()
    {
        Debug.Log($"Wave End Trigger is {this}");
    }

    public void Alt_WaveEnd_ShowResults()
    {
        WaveEnd_ShowResults?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("PlayerBody"))
        {
            Debug.LogError("Player has finished the wave");
            WaveEnd_ShowResults?.Invoke(this, EventArgs.Empty);
        }
    }
}
