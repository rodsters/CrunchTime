using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitMenu : MonoBehaviour
{
    // When the restart button is clicked, load the main menu scene.
    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }

    // When quit button is clicked, quit the application.
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    // When the credits button is clicked, load the credits scene.
    public void CreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
}
