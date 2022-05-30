using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float maxHealth = 40.0f;
    [SerializeField]
    private float damage = 10f;
    private float normalDamage;
    private float currentHealth;
    public EnemyHealthBar enemyHealthBar;

    private float angle;
    private Vector2 playerPosition;
    private SpriteRenderer sprite;

    // Distance from a gridpoint to transition to the next step in the path
    private float nextStepDistance = 1f;

    private Path path;
    private Vector2 destination;
    private Seeker seeker;
    private Rigidbody2D rigidbody2d;
    private int pathStep;

    private GameObject gameManager;
    private Timer timer;
    [SerializeField]
    private float timeAdded = 15.0f;

    [SerializeField] private float debuffTimerThrottle = 2.0f;
    private float DebuffTimer = 0;
    float currentTime = 180.0f;


    void Start()
    {
        this.rigidbody2d = GetComponent<Rigidbody2D>();
        this.seeker = GetComponent<Seeker>();
        gameManager = GameObject.Find("GameManager");
        timer = gameManager.GetComponent<Timer>();
        sprite = GetComponent<SpriteRenderer>();

        // Default destination is the player.
        destination = player.transform.position;
        InvokeRepeating("UpdatePath", 0f, 1f);

        currentHealth = maxHealth;
        enemyHealthBar.SetHealth(currentHealth, maxHealth);
        normalDamage = damage;
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(this.rigidbody2d.position, destination, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            pathStep = 0;
        }
    }

    void FixedUpdate()
    {
        // Must have a path to move.
        if (path == null || pathStep == path.vectorPath.Count)
        {
            return;
        }

        // Move toward the player for simple enemy.
        destination = player.transform.position;

        Vector2 direction = (path.vectorPath[pathStep] - this.transform.position).normalized;
        Vector2 nextPosition = (Vector2)this.transform.position + direction * speed * Time.deltaTime;
        this.rigidbody2d.MovePosition(nextPosition);

        float distanceToGridPoint = Vector2.Distance(this.rigidbody2d.position, (Vector2)path.vectorPath[pathStep]);
        
        if (distanceToGridPoint < nextStepDistance)
        {
            pathStep++;
        }

        // While we're at it, use the coordinate we just got to find the angle the enemy should turn to.
        Vector2 playerPosition = player.transform.position - transform.position;
        angle = Vector2.SignedAngle(Vector2.down, playerPosition) + 270;
        if ((angle) > 90 && (angle) <= 270)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }



        // Occasionally check for negative time to power up the enemy
        if (DebuffTimer > 0)
        {
            DebuffTimer -= Time.deltaTime;
        }
        if (DebuffTimer <= 0)
        {
            CheckForNegativeTime();
        }
    }

    public void ChangeEnemyHealth(float hitPointsToAdd)
    {
        currentHealth += hitPointsToAdd;
        enemyHealthBar.SetHealth(currentHealth, maxHealth);

        // Set vulnerability timer if damaged, or ensure max health is respected if healed.
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // If the enemy runs out of health, they die.
        if (currentHealth <= 0)
        {
            AddTime(timeAdded);
            Destroy(gameObject);
        }
    }

    private void AddTime(float timeToAdd)
    {
        timer.setTime(timer.returnTime() + timeToAdd);
    }

    // Strengthen enemy if the player has entered crunch time. The enemy deals +5 damage, +0-15 damage based on the negative time.
    private void CheckForNegativeTime()
    {
        currentTime = timer.returnTime();

        if (currentTime <= 0)
        {
            damage = normalDamage + 5 + (15 * (currentTime / 180.0f));
        }
        else
        {
            damage = normalDamage;
        }
    }

    // The three functions below are simple getter functions.
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public float GetDamage()
    {
        return damage;
    }

}
