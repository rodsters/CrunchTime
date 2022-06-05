using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HealthBar : MonoBehaviour
{
    // Referenced https://www.youtube.com/watch?v=NE5cAlCRgzo to create HP Bar UI elements
    // and make the UI elements visually update properly.
    private Image HP;
    public float currentHealth;
    private float maxHealth;
    PlayerController playerController;

    void Start()
    {
        HP = GetComponent<Image>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // The HP bar checks how much health the player has and sets the bar based on the percentage of health remaining.
    // The player dies when hp hits 0.
    void Update()
    {
        currentHealth = playerController.GetCurrentHealth();
        maxHealth = playerController.GetMaxHealth();
        HP.fillAmount = currentHealth/maxHealth;
        if (currentHealth <= 0.0f)
        {
            Death();
        }
    }

    // If the player hits 0 health, the game ends and loads the game over scene.
    void Death()
    {
        SceneManager.LoadScene("GameOver");
    }
}
