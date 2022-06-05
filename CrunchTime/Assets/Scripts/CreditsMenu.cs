using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CreditsMenu : MonoBehaviour
{
    // When restart button is clicked, load the Main Menu scene.
    public void RestartGame()
    {
        // Deselects clicked button so that it is no longer selected.
        EventSystem.current.SetSelectedGameObject(null);
        
        SceneManager.LoadScene("Menu");
    }

    // When quit button is clicked, quit the application.
    public void QuitGame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        Debug.Log("QUIT");
        Application.Quit();
    }
}
