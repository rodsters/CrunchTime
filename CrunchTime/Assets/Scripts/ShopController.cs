using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    public GameObject Panel;
    public Timer timer;
    // The back button toggles the panel visibility, the same way the open menu script handles it.
    public void CloseShop()
    {
        if (Panel != null)
        {
            bool shopOpen = Panel.activeSelf;
            Panel.SetActive(!shopOpen);
        }
    }
    // The upgrades access the timer's current time, decrease it by the amount the upgrade costs, then set the timer to the new value.
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

    public void upgradeThree()
    {

    }

    public void upgradeFour()
    {
        
    }
}

