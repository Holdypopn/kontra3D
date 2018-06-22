using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("Quit called - Not working in unity, only in build.");
        Application.Quit();
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
