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
    // the player can't purchase the speed upgrade multiple times so they dont fly through walls.
    private bool hasSpeedUpgrade = false;

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
    }

    public void MovespeedUpgrade()
    {
        }
    }

    public void upgradeThree()
    {
        // high firerate and inaccruacy, low damage
        EventSystem.current.SetSelectedGameObject(null);
        playerController = FindObjectOfType<PlayerController>();

        var currentValue = timer.returnTime();
        currentValue -= 180.0f;
        timer.setTime(currentValue);

        playerController.ChangeDamage(0.5f);
        playerController.ChangeFireRate(5.0f);
        playerController.ChangeInaccuracy(3.5f);

    }

    public void upgradeFour()
    {
        // Double regen and boost max health
        EventSystem.current.SetSelectedGameObject(null);
        playerController = FindObjectOfType<PlayerController>();

        var currentValue = timer.returnTime();
        currentValue -= 90.0f;
        timer.setTime(currentValue);

        playerController.ChangeRegen(0.5f);
        var currentMaxHealth = playerController.GetMaxHealth();

        playerController.SetMaxHealth(currentMaxHealth * 1.4f);
    }
}

