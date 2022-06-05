using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class QuitMenu : MonoBehaviour
{
    // When the restart button is clicked, load the main menu scene.
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

    // When the credits button is clicked, load the credits scene.
    public void CreditsScene()
    {
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene("Credits");
    }
}
