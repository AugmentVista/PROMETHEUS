using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public int waveCount = 0;
    public float waveMessageTime = 2f;


    void Start()
    {
        
    }

    public void NextWave()
    {
        if (waveCount < 10) 
        {
            waveCount++;
            Debug.Log($"Wave {waveCount} has begun");
        }
    }
}
