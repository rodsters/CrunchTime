using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    //[SerializeField]
    //public GameObject globalStateGO; 
    //private GlobalGameState globalGameState;


    private GameObject player;
    private Transform target;

    [SerializeField]
    private float speed = 2f;
    private float maxSpeed = 0f;
    
    [SerializeField]
    private float maxHealth = 40.0f;
    [SerializeField]
    private float damage = 10f;
    private float normalDamage;
    private float currentHealth;
    public EnemyHealthBar enemyHealthBar;
    [SerializeField]
    // Radius at which to consider for avoiding an object.
    private float visionRadius = 1.75f;
    // Angle in degrees to consider for avoiding an object.
    private float visionAngle = 180f;
    // The number of vision 
    private int visionRays = 19;

    [SerializeField]
    // Minimum distance allowed between objects for steering movement.
    private float minSeparationDistance = 1f;

    private float angle;
    private SpriteRenderer sprite;

    // Movement direction should be fairly continuous in change over time.
    // This direction is used as a basis for flocking/steering. Thus, if
    // there is no bias direction found from line of sight or A*, then use 
    // the previous direction of movement.
    private Vector2 direction = Vector2.zero;

    // Distance from a gridpoint to transition to the next step in the path
    private float nextStepDistance = 1f;

    private Path path;
    private Seeker seeker;
    private Rigidbody2D rigidbody2d;
    private int pathStep;

    private GameObject gameManager;
    private Timer timer;
    private EnemyTracker  enemyTracker;

    [SerializeField]
    private float timeAdded = 15.0f;

    [SerializeField] private float debuffTimerThrottle = 2.0f;
    private float DebuffTimer = 0;
    float currentTime = 180.0f;

    private SoundManager soundSystem;

    void Start()
    {
        //GlobalGameState globalGameState = globalStateGO.GetComponent<GlobalGameState>();

        player = GameObject.Find("RainbowMan");
        target = player.transform;
        this.rigidbody2d = GetComponent<Rigidbody2D>();
        this.seeker = GetComponent<Seeker>();

        gameManager = GameObject.Find("GameManager");
        timer = gameManager.GetComponent<Timer>();
        enemyTracker = gameManager.GetComponent<EnemyTracker>();

        sprite = this.transform.Find("EnemyAnimator").gameObject.GetComponent<SpriteRenderer>();
        this.GetComponent<SpriteRenderer>().enabled = false;

        // To prevent linking to an incorrect sound system (remember that they survive scene transitions),
        // objects link to the first sound system initialized on the creation of the project.
        soundSystem = SoundManager.instance;

        InvokeRepeating("UpdatePath", 0f, 1f);

        currentHealth = maxHealth;
        enemyHealthBar.SetHealth(currentHealth, maxHealth);
        normalDamage = damage;
        maxSpeed = speed * 1;
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(this.rigidbody2d.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            pathStep = 1;
        }
    }

    void FixedUpdate()
    {
        // Prioritize moving directly to target if it is in sight.
        Vector2 targetPosition = target.position - transform.position;
        int obstacleMask = LayerMask.GetMask("Obstacle");
        bool hasTargetVision = Physics2D.Raycast(transform.position, targetPosition.normalized, targetPosition.magnitude, obstacleMask).collider == null;

        // First determine an initial direction for steering.
        // Easiest ot use line of sight position tracking for target close by.
        if (hasTargetVision)
        {
            direction = targetPosition;
        }
        // If no line of sight, must have a path to follow when using A*.
        else if (path != null && pathStep < path.vectorPath.Count)
        {
            direction = (path.vectorPath[pathStep] - transform.position);

            float distanceToGridPoint = Vector2.Distance(transform.position, (Vector2)path.vectorPath[pathStep]);
            if (distanceToGridPoint < nextStepDistance)
            {
                pathStep++;
            }
        }
        
        // Ensure a unit length direction.
        direction = direction.normalized;
        float directionAngle = Vector2.SignedAngle(Vector2.right, direction);
        // Now, bias the direction toward open space within the vision radius
        // float bestRayDistance = 0f;
        int avoidanceMask = LayerMask.GetMask(new string[] { "Enemy", "Obstacle" });
        Vector2 sumDirection = Vector2.zero;
        for (int i = 0; i < visionRays; i++)
        {
            float offsetMagnitude = ((i + 1) / 2) * (visionAngle / (visionRays - 1));
            // Debug.Log(offsetMagnitude);
            float angleOffset = (i % 2 == 0) ? -offsetMagnitude : offsetMagnitude;
            float rayAngle = (directionAngle + angleOffset) * Mathf.Deg2Rad;
            Vector2 rayDirection = (new Vector2(Mathf.Cos(rayAngle), Mathf.Sin(rayAngle))).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, visionRadius, avoidanceMask);

            float fractionalDirection = 1;
            if (hit.collider != null)
            {
                fractionalDirection = ((hit.distance - minSeparationDistance) / (visionRadius - minSeparationDistance));
            }

            if (fractionalDirection < 0 && hit.collider.gameObject.layer == 7)
            {
                // Strongly avoid if it is an obstacle.
                fractionalDirection = -2;
            }
            else if (fractionalDirection < 0)
            {
                fractionalDirection = 0;
            }
            sumDirection += rayDirection * fractionalDirection;
            // else if (hit.distance > bestRayDistance)
            // {
            //     Debug.Log(hit.collider);
            //     Debug.Log(hit.distance);
            //     bestRayDistance = hit.distance;
            //     // Steer toward the direction with the most open space within view.
            //     direction = rayDirection;
            // }
        }
        
        // This steers to a direction that is biased against areas with large amounts of enemies/obstacles.
        direction = sumDirection.normalized;
        // Then, slow down if even this direction doesn't have space to move into based linearly on separation distance.
        RaycastHit2D directHit = Physics2D.Raycast(transform.position, direction, visionRadius, avoidanceMask);
        float speed = maxSpeed;
        if (directHit.collider != null)
        {
            speed = maxSpeed * (Mathf.Max(directHit.distance - minSeparationDistance, 0) / (visionRadius - minSeparationDistance));
        }

        Vector2 nextPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;
        this.rigidbody2d.MovePosition(nextPosition);

        // While we're at it, use the coordinate we just got to find the angle the enemy should turn to.
        angle = Vector2.SignedAngle(Vector2.down, targetPosition) + 270;
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

    void OnDrawGizmos()
    {
        // direction = direction.normalized;
        // float directionAngle = Vector2.SignedAngle(Vector2.right, direction);
        // // Now, bias the direction toward open space within the vision radius
        // float bestRayDistance = 0f;
        // for (int i = 0; i < visionRays; i++)
        // {
        //     float offsetMagnitude = ((i+1)/2) * (visionAngle / (visionRays-1));
        //     // Debug.Log(offsetMagnitude);
        //     float angleOffset = (i % 2 == 0) ? -offsetMagnitude : offsetMagnitude;
        //     float rayAngle = (directionAngle + angleOffset) * Mathf.Deg2Rad;
        //     Vector2 rayDirection = (new Vector2(Mathf.Cos(rayAngle), Mathf.Sin(rayAngle))).normalized;
        //     Gizmos.DrawLine(transform.position, (Vector2)transform.position + rayDirection*visionRadius);
        // }
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction * visionRadius);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minSeparationDistance);
    }

    public void ChangeEnemyHealth(float hitPointsToAdd)
    {
        currentHealth += hitPointsToAdd;
        enemyHealthBar.SetHealth(currentHealth, maxHealth);

        if (hitPointsToAdd < 0)
        {
            soundSystem.PlaySoundEffect("EnemyHit");
        }

        // Set vulnerability timer if damaged, or ensure max health is respected if healed.
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // If the enemy runs out of health, they die.
        if (currentHealth <= 0)
        {
            AddTime(timeAdded);
            soundSystem.PlaySoundEffect("EnemyDeath");
            Destroy(gameObject);
            AddTime(timeAdded);
            
            enemyTracker.DecrementEnemies();
            Debug.Log("decrement enemies : "+ enemyTracker.getNumEnemies());

            //GlobalGameState globalGameState = globalStateGO.GetComponent<GlobalGameState>();
           // globalGameState.DecrementEnemies();

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

