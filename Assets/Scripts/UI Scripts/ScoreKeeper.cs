using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private TimerController timer;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI drachmaText;
    public int score;
    public int drachma; // Ancient Greek name for currency


    private void Start()
    {
        Cursor.visible = false;
        CheckForTimer();
    }

    private void CheckForTimer()
    {
        if (TryGetComponent(out TimerController timerComponent))
        {
            timer = timerComponent;
        }
    }
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
