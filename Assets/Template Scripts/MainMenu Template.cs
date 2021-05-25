using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Important library for scene loading

public class Main : MonoBehaviour
{
    public void StartGame()
    {
        /* How do we load the next scene in build (one-liner)? 
         
        Hints: 
        - SceneManager.LoadScene() loads a scene by buildIndex (pass the buildIndex into the parenthesis)
        - SceneManager.GetActiveScene() returns an object representing the current scene we are on
        - Each scene object has a buildIndex member variable that represents the order it will be loaded
        - Build indexes are ordered: 0, 1, 2 ... such that scene 2 comes right after scene 1
        - Check and reorder your scene build index: File -> Build Settings

        */
    }

    /* Quit functionality is irrelevant for WebGL (cannot quit browser-based games)
However, it is included here in case you want to publish to other platforms

public void QuitGame()
{
    Application.Quit();
    UnityEditor.EditorApplication.isPlaying = false; // Optional, quits game preview in editor
} */

}
