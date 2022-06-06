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
    [SerializeField]
    private float maxHealth = 30.0f;
    private float currentHealth;

    [SerializeField]
    private float FireRate = 2.0f;
    private float FireRateTimer = 0.0f;

    Vector2 rayDirection;
    private RaycastHit2D VisibleObject;
    private bool hasLineOfSight = false;
    [SerializeField]
    private float sightRange = 30.0f;
    private int layerMask;


    // Distance from a gridpoint to transition to the next step in the path
    private float nextStepDistance = 1f;

    private Path path;
    private Vector2 destination;
    private Seeker seeker;
    private Rigidbody2D rigidbody2d;
    private int pathStep;


    void Start()
    {
        this.rigidbody2d = GetComponent<Rigidbody2D>();
        this.seeker = GetComponent<Seeker>();
        
        // Default destination is the player.
        destination = player.transform.position;
        InvokeRepeating("UpdatePath", 0f, 1f);

        currentHealth = maxHealth;
        normalSpeed = speed;

        // Creating a layer mask and add another layer to it.
        layerMask = LayerMask.GetMask("Protagonist");
        layerMask |= (1 << LayerMask.NameToLayer("Obstacle"));
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
                    Debug.Log("Hitting: " + VisibleObject.collider.tag);
                }
            }
        }

        if (hasLineOfSight)
        {
            // The enemy "charges" a shot when they see the player, so they will only fire a while after getting line of sight.
            FireRateTimer += Time.deltaTime;
            // Enemies slow down if they near the player, preventing them from just rushing the player like a melee enemy, yet
            // also not just having them go completely idle when the player is in sight range. Also helps in keeping sight range.
            speed = normalSpeed / 3.0f;
        }
        else
        {
            FireRateTimer = 0;

            speed = normalSpeed;
        }

        if (FireRateTimer >= FireRate)
        {
            Debug.Log("Pew Pew");
            FireRateTimer = 0;
        }
    }

    public void ChangeEnemyHealth(float hitPointsToAdd)
    {
        currentHealth += hitPointsToAdd;

        // Set vulnerability timer if damaged, or ensure max health is respected if healed.
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // If the enemy runs out of health, they die.
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
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
