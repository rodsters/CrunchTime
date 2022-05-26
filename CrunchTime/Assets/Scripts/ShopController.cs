using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    public GameObject Panel;
    public Timer timer;
    public void CloseShop()
    {
        if (Panel != null)
        {
            bool shopOpen = Panel.activeSelf;
            Panel.SetActive(!shopOpen);
            Time.timeScale = 1;
        }
    }

    public void DamageUpgrade()
    {
        var currentValue = timer.returnTime();
        currentValue -= 300.0f;
        timer.setTime(currentValue);
    }

    public void MovespeedUpgrade()
    {
        var currentValue = timer.returnTime();
        currentValue -= 150.0f;
        timer.setTime(currentValue);
    }
}

