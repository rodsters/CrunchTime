using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    // Load the main gameplay scene on clicking Play.
    public void PlayGame()
    {
        // Deselects clicked button so that it is no longer selected.
        EventSystem.current.SetSelectedGameObject(null);
        
        SceneManager.LoadScene("IntroCutscene");
    }

    // When quit button is clicked, quit the application.
    public void QuitGame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        Application.Quit();
    }

    public void InstructionMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene("HowToPlay");
    }
}
