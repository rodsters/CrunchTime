using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedEnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float speed = 2.5f;
    private float normalSpeed = 2.5f;
    // Enemies move this times slower when they get line of sight with the player.
    [SerializeField]
    private float lineOfSightSlowdown = 3;
    [SerializeField]
    private float maxHealth = 30.0f;
    private float currentHealth;

    private float angle;
    private SpriteRenderer sprite;

    [SerializeField]
    private float FireRate = 2.0f;
    [SerializeField]
    private int numShots;
    private float FireRateTimer = 0.0f;
    [SerializeField] public EnemyProjectileController EnemyProjectilePrefab;

    Vector2 rayDirection;
    private RaycastHit2D VisibleObject;
    private bool hasLineOfSight = false;
    [SerializeField]
    private float sightRange = 30.0f;
    private int layerMask;
    public EnemyHealthBar enemyHealthBar;

    [SerializeField]
    private bool canMelee = false;
    [SerializeField]
    private float meleeDamage = 0.0f;

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
        normalSpeed = speed;

        // Creating a layer mask and add another layer to it. The notation looks really weird with |= but don't worry about it.
        // A layer mask just specified the only layers the raycast should pay attention to. In this case, it should only look for
        // the player but it should also take note if any walls are in the way. Also FYI, Layers are different from tags.
        layerMask = LayerMask.GetMask("Protagonist");
        layerMask |= (1 << LayerMask.NameToLayer("Obstacle"));

        currentHealth = maxHealth;
        enemyHealthBar.SetHealth(currentHealth, maxHealth);
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
        angle = Vector2.SignedAngle(Vector2.down, destination) + 270;
        if ((angle) > 90 && (angle) <= 270)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        // The enemy uses raycsting to see if the player is visible. It takes the player's direction, fires a "ray,"
        // and if there is nothing obstructing the player from the enemy, then the enemy has line of sight.
        Vector2 rayDirection = player.transform.position - transform.position;
        if (VisibleObject = Physics2D.Raycast(transform.position, rayDirection, sightRange, layerMask))
        {
            if (VisibleObject.transform != null)
            {
                if (VisibleObject.collider.CompareTag("Player"))
                {
                    hasLineOfSight = true;
                }
                else
                {
                    hasLineOfSight = false;
                }
            }
        }

        // While we're at it, use the coordinate we just got to find the angle the enemy should turn to.
        // Shooting enemies always turn towards the player.
        angle = Vector2.SignedAngle(Vector2.down, rayDirection) + 270;
        if ((angle) > 90 && (angle) <= 270)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        if (hasLineOfSight)
        {
            // The enemy "charges" a shot when they see the player, so they will only fire a while after getting line of sight.
            FireRateTimer += Time.deltaTime;
            // Enemies slow down if they near the player, preventing them from just rushing the player like a melee enemy, yet
            // also not just having them go completely idle when the player is in sight range. Also helps in keeping sight range.
            speed = normalSpeed / lineOfSightSlowdown;
        }
        else
        {
            FireRateTimer = 0;

            speed = normalSpeed;
        }

        if (FireRateTimer >= FireRate)
        {
            for (int i = 0; i < numShots; i++)
            {
                Instantiate(EnemyProjectilePrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), transform.rotation);
            }
            FireRateTimer = 0;
        }
    }

    // Melee and Ranged enemies can deal contact dmage to the player, but unlike other enemies do it from their own cs file.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && canMelee)
        {
            other.gameObject.GetComponent<PlayerController>().ChangeCurrentHealth(-meleeDamage);
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

    // The two functions below are simple getter functions for current and max health respectively.
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

}
