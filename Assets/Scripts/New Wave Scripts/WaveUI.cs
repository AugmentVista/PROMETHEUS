using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public int waveCount = 1;
    public float waveMessageTime = 2f;


    void Start()
    {
        
    }

    public void NextWave()
    {
        Debug.Log("What is going on with waves?");
        if (waveCount < 10) 
        {
            waveCount++;
            Debug.Log($"Wave {waveCount} has begun");
        }
    }
}
