using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldown : MonoBehaviour
{
    public Image dashImage;
    bool onCooldown = false;
    private float dashCooldown;
    private bool isDashing = false;
    PlayerController playerController;
    // Referenced https://www.youtube.com/watch?v=wtrkrsJfz_4&list=LL&index=1&t=157s for the cooldown icon

    // The dashImage is the darkened image that will appear and fill when the dash is on cooldown. 
    // It is hidden initially as the ability is not on cooldown yet.
    void Start()
    {
        dashImage.fillAmount = 0;
        playerController = FindObjectOfType<PlayerController>();
    }

    // Call the function to determine whether the dash is on cooldown.
    void Update()
    {
        DashedCheck();
    }

    void DashedCheck()
    {
        // access the dashCooldown and whether the player is dashing from player controller.
        dashCooldown = playerController.GetDashCooldown();
        isDashing = playerController.GetIsDashing();
        // Check if player has dashed.
        if (isDashing == true)
        {
            // If they have, make the on cd icon appear.
            onCooldown = true;
            dashImage.fillAmount = 1;
        }
        // While the dash is on cooldown, take away the darkened icon's fill, leaving only the normal icon in its place.
        if (onCooldown)
        {
            dashImage.fillAmount -= 1/(3*dashCooldown)  * Time.deltaTime;
            if(dashImage.fillAmount <= 0)
            {
                dashImage.fillAmount = 0;
                onCooldown = false;
            }
        }
    }
}
