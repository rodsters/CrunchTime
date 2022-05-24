using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Command;

// Made by Jasper Fadden
// This currently adds semi-basic movement for rainbow man, via simple WASD key presses adding velocity in the respecitve direction.
// Future plans are to add the 4 stage movement system the teacher earlier described, and eventually something very comprehensive.

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 3.5f;
    [SerializeField]
    public ProjectileController ProjectilePrefab;
    Rigidbody2D rigidbody2d;
    SpriteRenderer sprite;
    float horizontal;
    float vertical;

    float angle;
    Vector3 mousePosition;




    // Start is called before the first frame update.
    void Start()
    {
        // Easily access components with variables.
        rigidbody2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

       
    }

    // Update is called once per frame.
    void Update()
    {
        // GetAxis just takes the movement controls in the project settings and maps them to -1 (left/down) to 1 (right/above).
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        if(Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            
            Instantiate(ProjectilePrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), transform.rotation);
        }
    }

    // Fixed update is used for better compatibility and stuff
    void FixedUpdate()
    {
        // Take the player's position, increment it slightly based off current input, and then have the rigidbody move there.
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);

        // Basic sprite animation via flipping horizontally. It's based on where the player's mouse is, so we have to calculate things.
        // Get the player's mouse position and RainbowMan's current direction.
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        // This is used to detect where the player should be facing
        angle = Vector2.SignedAngle(Vector2.down, direction) + 270;

        if ((angle) > 90 && (angle) < 270 )
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }




    }


}