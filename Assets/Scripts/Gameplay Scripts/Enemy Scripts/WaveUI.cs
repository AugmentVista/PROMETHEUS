using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    public static bool FinalWaveConlcuded = false;
    public int waveCount = 1;
    private int amountOfWavesToBeat = 10;


    private void Awake()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        if (thisScene.name == "Level_1")
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
        try
        {
            GameObject waveTextObject = GameObject.FindGameObjectWithTag("WaveText");

            if (waveTextObject != null)
            {
                TextMeshProUGUI waveTextUI = waveTextObject.GetComponent<TextMeshProUGUI>();
                if (waveTextUI != null)
                {
                    waveTextUI.text = $"Wave {waveCount} / {amountOfWavesToBeat}";
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"SetWaveText failed, WaveUI not found in this scene: {ex.Message}\n{ex.StackTrace}");
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
        if (waveCount == amountOfWavesToBeat)
        {
            Game_Manager gameManager = Singleton.instance.GetComponent<Game_Manager>();
            Scene currentScene = SceneManager.GetActiveScene();
            if (gameManager != null)
            {
                if (currentScene.name == "Level_1")
                {
                    gameManager.GameWinTrigger();
                }
            }
        }
    }
}
