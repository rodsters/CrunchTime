using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by Jasper Fadden
// This currently adds semi-basic movement for rainbow man, via simple WASD key presses adding velocity in the respecitve direction.
// Future plans are to add the 4 stage movement system the teacher earlier described, and eventually something very comprehensive.

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    SpriteRenderer sprite;
    [SerializeField] float speed = 3.5f;
    float horizontal;
    float vertical;

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
    }

    // Fixed update is used for better compatibility and stuff
    void FixedUpdate()
    {
        // Take the player's position, increment it slightly based off current input, and then have the rigidbody move there.
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);

        // Basic sprite animation via flipping horizontally.
        if (horizontal < 0)
        {
            sprite.flipX = true;
        }
        else if (horizontal > 0)
        {
            sprite.flipX = false;
        }
    }


}
