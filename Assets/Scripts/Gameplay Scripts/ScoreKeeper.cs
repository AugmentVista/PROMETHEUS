using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;


    private void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
