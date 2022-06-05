using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    private float sceneTime;
    // Initialize the timer tracking how long the scene has been activve
    void Start()
    {
        sceneTime = 0.0f;
    }

    // Load the main gameplay scene after 8 seconds (after cutscene).
    void Update()
    {
        sceneTime += Time.deltaTime;
        if (sceneTime >= 8.0f)
        {
            SceneManager.LoadScene("Main Scene");
        }
    }
    // Skip the cutscene and load the main scene if the player clicks skip.
    public void SkipCutscene()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
