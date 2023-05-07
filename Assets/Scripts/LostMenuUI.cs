using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostMenuUI : MonoBehaviour
{
    public void TryAgainButton()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
