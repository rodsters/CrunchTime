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

        
    }
}