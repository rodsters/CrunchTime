using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Load the main gameplay scene on clicking Play.
    public void PlayGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    // When quit button is clicked, quit the application.
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void InstructionMenu()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}
