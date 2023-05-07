using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool gameIsPaused;
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            gameIsPaused = !gameIsPaused;
            PauseTheGame();
        }
    }
    void PauseTheGame ()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else 
        {
            Time.timeScale = 1;
        }
    }
}
