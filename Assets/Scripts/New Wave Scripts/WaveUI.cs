using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public int waveCount = 1;
    public float waveMessageTime = 0.5f;


    void Start()
    {
        StartCoroutine(WaveDelay());
    }

    private IEnumerator WaveDelay()
    {
        Debug.Log("Delay is running");
        waveMessageTime += 0.25f;
        yield return new WaitForSeconds(waveMessageTime);
    }

    public void ResetWave()
    {
        waveMessageTime = 0f;
        waveCount = 0;
        NextWave();
    }


    public void NextWave()
    {
        WaveDelay();
        if (waveCount < 10) 
        {
            waveCount++;
            Debug.LogError($"Wave {waveCount} has begun");
        }
    }
}
