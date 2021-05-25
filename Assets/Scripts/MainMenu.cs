using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // will load next scene sequentially according to build order
    }

    /* Quit functionality is irrelevant for WebGL (cannot quit browser-based games)
    However, it is included here in case you want to publish to other platforms

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false; // Optional, quits game preview in editor
    } */
}