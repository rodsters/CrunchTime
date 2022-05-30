using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] float speed = 7;
    // Update is called once per frame

    Vector3 mousePosition;
    float angle;
    // Not sure whether or not this should be a thing, but it could be fun for upgrades (maybe a minigun one that adds
    // to inaccuracy but gives a huge fire-rate, or one that sets inaccuracy to be 0).
    float inaccuracy = PlayerController.inaccuracy;
    float damage = PlayerController.damage;

    void Start()
    {
        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        angle = Vector2.SignedAngle(Vector2.right, direction);
        angle += Random.Range(-inaccuracy, inaccuracy);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void Update()
    {
        // Go in the original aimed direction each frame.
        transform.position += transform.right * Time.deltaTime * this.speed;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    // This collision script allows enemies to behit by projectiles
    private void OnTriggerStay2D(Collider2D other)
    {
        // The Enemy tag is not intended for use anymore (its too general when different tags have to exist between enemies).
        // This exists for backwards compatibiltity.
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().ChangeEnemyHealth(-damage);
            Destroy(gameObject);
        }
        // Deal damage to enemies that are hit.
        if (other.gameObject.CompareTag("EnemyMelee"))
        {
            other.GetComponent<EnemyController>().ChangeEnemyHealth(-damage);
            Destroy(gameObject);
        }
        // Deal damage to ranged enemies that are hit.
        if (other.gameObject.CompareTag("EnemyRanged"))
        {
            other.GetComponent<RangedEnemyController>().ChangeEnemyHealth(-damage);
            Destroy(gameObject);
        }
        // If the player shoots an enemy projectile, then they canblockthe projectile from hitting.
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(other);
            Destroy(gameObject);
        }
    }
}