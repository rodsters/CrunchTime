using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    // Speed and damage are specified per prefab in case we end up making multiple types of projectiles.
    [SerializeField] float speed = 7.0f;
    [SerializeField] float damage = 7.5f;
    [SerializeField] float inaccuracy = 5.0f;
    GameObject player;

    float currentTime = 180.0f;
    private GameObject gameManager;
    private Timer timer;

    Vector3 mousePosition;
    float angle;
    // Not sure whether or not this should be a thing, but it could be fun for upgrades (maybe a minigun one that adds
    // to inaccuracy but gives a huge fire-rate, or one that sets inaccuracy to be 0).

    void Start()
    {
        // Take the player's current position vs the projectile, find the angle to that position, 
        // add inaccuracy, and turn to that direction upon initializing.
        player = GameObject.FindWithTag("Player");
        Vector2 direction = player.transform.position - transform.position;
        angle = Vector2.SignedAngle(Vector2.right, direction);
        angle += Random.Range(-inaccuracy, inaccuracy);
        transform.eulerAngles = new Vector3(0, 0, angle);

        gameManager = GameObject.Find("GameManager");
        timer = gameManager.GetComponent<Timer>();
        CheckForNegativeTime();
    }

    void Update()
    {
        // get mouse position if this should be homing.
        // Vector3 mousePos = Input.mousePosition;   
        transform.position += transform.right * Time.deltaTime * this.speed;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player is hit by one of these, then they will take damage and the projectile will be destroyed.
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().ChangeCurrentHealth(-damage);
        }
        if (collision.gameObject.CompareTag("EnemyProjectile") != true)
        {
            Destroy(gameObject);
        }
    }

    // Strengthen projectile if the player has entered crunch time. Deal +5 damage, +0-15 damage based on the negative time.
    private void CheckForNegativeTime()
    {
        currentTime = timer.returnTime();

        if (currentTime <= 0)
        {
            damage = damage + 5 + (10 * (currentTime / 180.0f));
        }
    }
}
