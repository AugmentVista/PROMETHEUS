using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    public int waveCount = 1;
    public float waveMessageTime = 0.5f;
    public Image progressBarFill;
    private float targetFillAmount;


    void Start()
    {
        targetFillAmount = 1.0f;
        progressBarFill.fillAmount = 0.0f;
        StartCoroutine(WaveDelay());
    }

    private void Update()
    {
        if (progressBarFill.fillAmount + -targetFillAmount < 1.1f)
        {
            progressBarFill.fillAmount = Mathf.Lerp(progressBarFill.fillAmount, targetFillAmount, Time.deltaTime * 1f);
        }
    }

    public void UpdateFillAmount()
    {
        if (progressBarFill != null)
        {
            targetFillAmount = waveCount / 10;
        }
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
        UpdateFillAmount();
    }
}
