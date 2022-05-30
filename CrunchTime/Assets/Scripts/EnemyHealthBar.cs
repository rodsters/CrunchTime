using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // Referenced https://www.youtube.com/watch?v=v1UGTTeQzbo in order to create the healthbar UI
    // and functionality.
    public Slider slider;
    public Color healthColor;
    public Vector3 Offset;

    // The healthbar will only appear after the enemy has taken damage.
    public void SetHealth(float currentHealth, float maxHealth)
    {
        slider.gameObject.SetActive(currentHealth < maxHealth);
        slider.value = currentHealth;
        slider.maxValue = maxHealth;
    }

    // Set the healthbar to the proper location.
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
