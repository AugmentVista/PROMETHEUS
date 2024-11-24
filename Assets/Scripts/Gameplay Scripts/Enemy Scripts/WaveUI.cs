using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    public static bool FinalWaveConlcuded = false;
    public int waveCount = 1;
    private int amountOfWavesToBeat = 20;
    private void Awake()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        if (thisScene.name != "Level_1")
        {
            SetWaveText();
        }
    }

    public void RestartWave()
    {
        waveCount = 0;
        NextWave();
        SetWaveText();
    }

    private void Update()
    {
        SetWaveText();
    }

    public void SetWaveText()
    {
        GameObject waveTextObject;
        if (waveTextObject = null)
        { 
            waveTextObject = GameObject.FindGameObjectWithTag("WaveText");
        }
        else if (waveTextObject != null) 
        { 
            TextMeshProUGUI waveTextUI = waveTextObject.GetComponent<TextMeshProUGUI>();
            if (waveTextUI != null)
            {
                waveTextUI.text = $"Wave {waveCount} / {amountOfWavesToBeat}";
            }
        }
    }


    public void NextWave()
    {
        if (waveCount < amountOfWavesToBeat)
        {
            waveCount++;
            GlobalSettings.globalWaveCount = waveCount;
            Debug.Log($"Wave {waveCount} has begun");
        }
        else 
        {
            FinalWaveConlcuded = true;
        }
    }
}
