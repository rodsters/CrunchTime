using System.Collections;
using UnityEngine;

// Keeps track of whether the platform in mobile and changes scene accordingly.

public class MobileManager : MonoBehaviour
{
    [SerializeField] private bool isMobile = false;
    
    // UI Elements
    [SerializeField] private GameObject moveJoystick;
    private Joystick move;
    [SerializeField] private GameObject shootJoystick;
    private Joystick shoot;
    [SerializeField] private GameObject dashButton;
    
    public bool getMobile()
    {
        return isMobile;
    }
    
    public Vector2 getMove()
    {
        return move.Direction;
    }
    
    public Vector2 getShoot()
    {
        return shoot.Direction;
    }
    
    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            isMobile = true;
        }

        if (isMobile)
        {
            moveJoystick.SetActive(true);
            move = moveJoystick.GetComponent<FixedJoystick>();
            shootJoystick.SetActive(true);
            shoot = shootJoystick.GetComponent<FixedJoystick>();
            dashButton.SetActive(true);
        }
    }
}