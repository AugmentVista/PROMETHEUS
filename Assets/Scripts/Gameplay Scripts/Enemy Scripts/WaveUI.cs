using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    public static bool FinalWaveConlcuded = false;
    public int waveCount = 1;
    public float waveMessageTime = 0.5f;
    private int amountOfWavesToBeat = 8;
    private void Awake()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        if (thisScene.name != "Level_1")
        {
            SetWaveText();
        }
    }


    private void Start()
    {
        StartCoroutine(WaveDelay());
    }

    private IEnumerator WaveDelay()
    {
        waveMessageTime = 1.0f;
        yield return new WaitForSeconds(waveMessageTime);
    }

    public void RestartWave()
    {
        waveMessageTime = 0f;
        waveCount = 0;
        NextWave();
    }

    private void Update()
    {
        SetWaveText();
    }

    public void SetWaveText()
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


    public void NextWave()
    {
        WaveDelay();
        if (waveCount < amountOfWavesToBeat)
        {
            waveCount++;
            GlobalSettings.globalWaveCount = waveCount;
            //Debug.LogError($"Wave {waveCount} has begun");
        }
        else 
        {
            FinalWaveConlcuded = true;
        }
    }
}
