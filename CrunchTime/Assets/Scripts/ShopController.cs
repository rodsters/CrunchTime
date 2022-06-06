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
    // The player can't purchase the speed upgrade multiple times so they dont fly through walls.
    private bool hasSpeedUpgrade = false;
    private bool hasDamageUpgrade = false;
    private bool hasMinigun = false;
    private bool hasHealthUpgrade = false;

    // The back button toggles the panel visibility, the same way the open menu script handles it.
    public void CloseShop()
    {
        if (Panel != null)
        {
            bool shopOpen = Panel.activeSelf;
            Panel.SetActive(!shopOpen);
            Time.timeScale = 1;
        }
    }
    // The upgrades access the timer's current time, decrease it by the amount the upgrade costs, then set the timer to the new value.
    public void DamageUpgrade()
    {
        // Deselects clicked button so that it is no longer selected.
        if(hasDamageUpgrade == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
            playerController = FindObjectOfType<PlayerController>();
            var currentValue = timer.returnTime();
            currentValue -= 120.0f;
            timer.setTime(currentValue);
            var newdamage = playerController.GetDamage();
            newdamage *= 2;
            playerController.ChangeDamage(newdamage);
            hasDamageUpgrade = true;
        }
    }

    public void MovespeedUpgrade()
    {
        if(hasSpeedUpgrade == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
            playerController = FindObjectOfType<PlayerController>();
            var currentValue = timer.returnTime();
            currentValue -= 50.0f;
            timer.setTime(currentValue);
            var newspeed = playerController.GetSpeed();
            newspeed *= 1.3f;
            playerController.SetSpeed(newspeed);
            hasSpeedUpgrade = true;
        }
    }

    public void MinigunUpgrade()
    {
        if(hasMinigun == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
            playerController = FindObjectOfType<PlayerController>();

            var currentValue = timer.returnTime();
            currentValue -= 180.0f;
            timer.setTime(currentValue);

            playerController.ChangeDamage(0.5f);
            playerController.ChangeFireRate(5.0f);
            playerController.ChangeInaccuracy(3.5f);
        }
        // High firerate and inaccruacy, low damage.
    }

    public void HealthUpgrade()
    {
        if (hasHealthUpgrade == false) 
        {
            // Double regen and boost max health.
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
}

