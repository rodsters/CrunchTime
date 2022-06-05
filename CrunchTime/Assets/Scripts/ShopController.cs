using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ShopController : MonoBehaviour
{
    public GameObject Panel;
    public Timer timer;
    PlayerController playerController;
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
        // Deselects clicked button so that it is no longer selected.
        EventSystem.current.SetSelectedGameObject(null);
        playerController = FindObjectOfType<PlayerController>();
        var currentValue = timer.returnTime();
        currentValue -= 300.0f;
        timer.setTime(currentValue);
        playerController.ChangeDamage(2.0f);
    }

    public void MovespeedUpgrade()
    {
        EventSystem.current.SetSelectedGameObject(null);
        playerController = FindObjectOfType<PlayerController>();
        var currentValue = timer.returnTime();
        currentValue -= 150.0f;
        timer.setTime(currentValue);
        var newspeed = playerController.GetSpeed();
        newspeed *= 2;
        playerController.SetSpeed(newspeed);
    }

    public void upgradeThree()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void upgradeFour()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}

