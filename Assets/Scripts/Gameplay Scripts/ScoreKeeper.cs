using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
