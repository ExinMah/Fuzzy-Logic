using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpUI;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI highScoreUI;
    public static int score = 0;
    public static int highScore = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseGame.gameIsPaused)
        {
            AddScore(1);
            hpUI.text = "HP: " + carController.hp.ToString();
            scoreUI.text = "Score: " + score.ToString();

            if (score > highScore)
            {
                highScore = score;
                Save();
            }

            highScoreUI.text = "High Score: " + highScore.ToString();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("highscore", highScore);
    }

    public void Load()
    {
        score = PlayerPrefs.GetInt("score");
        highScore = PlayerPrefs.GetInt("highscore");
    }
}
