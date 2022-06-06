using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Pause menu made referencing https://www.youtube.com/watch?v=JivuXdrIHK0&t=1s.
// Also referenced https://www.youtube.com/watch?v=tfzwyNS1LUY.
public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;
    public static bool GamePaused = false;

    // Upon pressing escape, open/close the pause menu.
    void Update()
    {
        // Escape key press - https://docs.unity3d.com/ScriptReference/KeyCode.Escape.html.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check the current state, and act based on if it is paused or resuming.
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    // Function to open pause menu.
    void Paused()
    {
        pauseMenu.SetActive(true);
        // When timeScale is set to zero your application acts as if paused - https://docs.unity3d.com/ScriptReference/Time-timeScale.html.
        Time.timeScale = 0f;
        GamePaused = true;
    }

    // Function to close pause menu.
    public void Resume()
    {
        pauseMenu.SetActive(false);
        // Reset time scale back to normal.
        Time.timeScale = 1f;
        GamePaused = false;
    }

    // Function for Main Menu button to return back to the main menu.
    public void HomeScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
