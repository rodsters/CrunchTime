using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private float currentTime = 0.0f;
    [SerializeField] private float totalTime = 600.0f;

    // Referenced https://www.youtube.com/watch?v=o0j7PdU88a4&t=215s in order to make UI element update in canvas.
    [SerializeField] Text timerText;
    void Start()
    {
        currentTime = totalTime;
    }

// Functions to allow timer to be used as a currency.

    public void setTime(float newTime)
    {
        currentTime = newTime;
    }

    public float returnTime()
    {
        return currentTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        var minuteTime = Mathf.FloorToInt(currentTime/60);
        var secondTime = Mathf.FloorToInt(currentTime % 60);
        // Referenced https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html in order to make the text appear properly
        var timeString = minuteTime.ToString("00") + ":" + secondTime.ToString("00");
        timerText.text = timeString;
        if (currentTime <=-180)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
