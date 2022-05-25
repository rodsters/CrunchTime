using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private float currentTime = 0.0f;
    [SerializeField] private float totalTime = 10.0f;

    // Referenced https://www.youtube.com/watch?v=o0j7PdU88a4&t=215s in order to make UI element update in canvas.
    [SerializeField] Text timerText;
    void Start()
    {
        currentTime = totalTime;
    }

// Functions to allow timer to be used as a currency.
    void decreaseTime(float cost)
    {
        currentTime -= cost;
    }

    void increaseTime(float bonus)
    {
        currentTime += bonus;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("0");
        if (currentTime <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
