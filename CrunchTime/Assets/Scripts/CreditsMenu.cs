using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    // When restart button is clicked, load the Main Menu scene.
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
}
