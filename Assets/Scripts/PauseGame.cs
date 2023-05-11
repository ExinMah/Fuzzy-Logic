using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public TextMeshProUGUI text;
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
            text.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            text.gameObject.SetActive(false);
        }
    }
}
