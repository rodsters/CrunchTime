using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] float speed = 14;

    private SoundManager soundSystem;
    Vector3 mousePosition;
    float angle;
    // Not sure whether or not this should be a thing, but it could be fun for upgrades (maybe a minigun one that adds
    // to inaccuracy but gives a huge fire-rate, or one that sets inaccuracy to be 0).
    float inaccuracy = PlayerController.inaccuracy;
    float damage = PlayerController.damage;
    bool hasDealtDamage = false;

    void Start()
    {
        // To prevent linking to an incorrect sound system (remember that they survive scene transitions),
        // objects link to the first sound system initialized on the creation of the project.
        soundSystem = SoundManager.instance;

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
        soundSystem.PlaySoundEffect("WallHit");
    }

    // This collision script allows enemies to behit by projectiles
    private void OnTriggerStay2D(Collider2D other)
    {
        // Destroying an object takes time, so there is a boolean variable to prevent damaging multiple enemies at once.
        if (hasDealtDamage == false)
        {
            // The Enemy tag is not intended for use anymore (its too general when different tags have to exist between enemies).
            // This exists for backwards compatibiltity.
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyController>().ChangeEnemyHealth(-damage);
                hasDealtDamage = true;
                Destroy(gameObject);
            }
            // Deal damage to enemies that are hit.
            if (other.gameObject.CompareTag("EnemyMelee"))
            {
                other.GetComponent<EnemyController>().ChangeEnemyHealth(-damage);
                hasDealtDamage = true;
                Destroy(gameObject);
            }
            // Deal damage to ranged enemies that are hit.
            if (other.gameObject.CompareTag("EnemyRanged"))
            {
                other.GetComponent<RangedEnemyController>().ChangeEnemyHealth(-damage);
                hasDealtDamage = true;
                Destroy(gameObject);
            }
            // If the player shoots an enemy projectile, then they canblockthe projectile from hitting.
            if (other.gameObject.CompareTag("EnemyProjectile"))
            {
                Destroy(other);
                hasDealtDamage = true;
                Destroy(gameObject);
            }
        }
    }
}
