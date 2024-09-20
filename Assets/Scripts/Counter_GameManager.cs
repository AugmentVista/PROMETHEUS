using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerCounter : MonoBehaviour
{
    public TextMeshProUGUI gameManagerCountText;
    private int gameManagerCount;

    void Start()
    {
        UpdateGameManagerCount();
    }

    void Update()
    {
        gameManagerCountText.text = "There is: " + gameManagerCount +" Game_Manager In this scene";
    }

    void UpdateGameManagerCount()
    {
        Game_Manager[] gameManagers = FindObjectsOfType<Game_Manager>();
        gameManagerCount = gameManagers.Length; 
    }
}

