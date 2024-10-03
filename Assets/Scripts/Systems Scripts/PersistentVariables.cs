using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersistentVariables : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI xp;
    public TextMeshProUGUI score;

    private int Health;
    private int Xp;
    private int Score;

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 100, 100 ,100), "Add HP"))
            Health++;

        if (GUI.Button(new Rect(100, 100, 100, 100), "Add Xp"))
            Xp++;

        if (GUI.Button(new Rect(200, 100, 100, 100), "Add Score"))
            Score++;

    }

    private void UpdateStats()
    {
        health.text = "Health is: " + Health;
        score.text = "Score is: " + Score;
        xp.text = "XP is: " + Xp;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();
    }
}
