using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RainbowGunAnimation : MonoBehaviour
{
    Vector3 mousePosition;
    float angle;
    private MobileManager manager;
    private bool mobile;

    void Start()
    {
        // Check if mobile.
        GameObject gameManager = GameObject.Find("GameManager");
        manager = gameManager.GetComponent<MobileManager>();
        mobile = manager.getMobile();
    }

    // Update is called once per frame
    void Update()
    {
        // These values are used so the gun aims where the player's cursor is. Read PlayerController for more info as to how it works.
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction;
        if (mobile)
        {
            direction = manager.getShoot();
            if (direction == Vector2.zero)
            {
                direction = mousePosition - transform.position;
            }
        }
        else
        {
            direction = mousePosition - transform.position;
        }
        
        // This is used to detect where the gun should be facing and if it should be flipped
        angle = Vector2.SignedAngle(Vector2.down, direction) + 270;

        transform.eulerAngles = new Vector3(0, 0, angle);



        // These values are just used to animate the gun so it turns with the player
        if ( (angle) > 90 && (angle) < 270 )
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.eulerAngles = new Vector3(0, 0, angle + 180);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
