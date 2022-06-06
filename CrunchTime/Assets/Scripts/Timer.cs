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
    [SerializeField] private float totalTime = 120.0f;

    // Animation variables
    private Color red;
    private Color blue;

    // Referenced https://www.youtube.com/watch?v=o0j7PdU88a4&t=215s in order to make UI element update in canvas.
    [SerializeField] Text timerText;
    void Start()
    {
        currentTime = totalTime;
        StartCoroutine(Flash());
        blue = new Color(0.607f, 1.0f, 0.964f, 1.0f);
        red = new Color(0.854f, 0.305f, 0.219f, 1.0f);
    }

    // Functions to allow timer to be used as a currency (get and set functions).

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
        // Decrease time by 1 second constantly, and then calculate the minutes and seconds.
        currentTime -= Time.deltaTime;
        int minuteTime;
        int secondTime;
        string timeString;
        if (currentTime >= 0.0f)
        {
            minuteTime = Mathf.FloorToInt(currentTime/60);
            secondTime = Mathf.FloorToInt(Mathf.Abs(currentTime % 60));
            timeString = minuteTime.ToString("00") + ":" + secondTime.ToString("00");
            
            // Set color to #9BFFF6
            if (timerText.color.g != 1.0f)
            {
                var newColor = blue;
                newColor.a = timerText.color.a;
                timerText.color = newColor;
            }
        }
        else
        {
            // Floor returns -1 when between (-1,0)
            minuteTime = Mathf.CeilToInt(currentTime/60);
            secondTime = Mathf.CeilToInt(Mathf.Abs(currentTime % 60));
            timeString = "-" + minuteTime.ToString("0") + ":" + secondTime.ToString("00");
            
            // Set color to #DA4E38
            if (timerText.color.g != 0.305f)
            {
                var newColor = red;
                newColor.a = timerText.color.a;
                timerText.color = newColor;
            }
        }
        // Referenced https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html in order to make the text appear properly
        timerText.text = timeString;
        // The player can go into negative time, at negative three minutes they die.
        if (currentTime <=-180)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    
    IEnumerator Flash()
    {
        while (currentTime > -180.0f)
        {
            Color c = timerText.color;
            if (c.a == 0.8f)
            {
                c.a = 1.0f;
            } 
            else
            {
                c.a = 0.8f;
            }
            timerText.color = c;
            yield return new WaitForSeconds(0.5f);
        }
        Color b = timerText.color;
        b.a = 1.0f;
        timerText.color = b;
        yield break;
    }
}
