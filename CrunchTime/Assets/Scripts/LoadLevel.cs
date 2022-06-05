using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    private float delayTime = 6.0f;
    private float sceneTime = 0.0f;
    // Load the main gameplay scene after 8 seconds (after cutscene).
    void Update()
    {
        delayTime -= Time.deltaTime;
        if (delayTime <= 0)
        {
            loadMain();
        }
    }

    void loadMain()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
