using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectile manages the behavior of a projectile in the game, including its movement, damage application,
/// and knockback effects on collision with damageable objects.
/// </summary>
public class Projectile : MonoBehaviour
{
    // Damage dealt by the projectile
    public int damage = 30;

    // Speed of the projectile's movement
    public Vector2 moveSpeed = new Vector2(3f, 0);

    // Knockback force applied when the projectile hits a target
    public Vector2 knockback = new Vector2(0, 0);

    // Reference to the Rigidbody2D component for movement
    private Rigidbody2D rb;

    private void Awake()
    {
        // Initialize the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial velocity based on the projectile's facing direction
        float direction = transform.localScale.x > 0 ? 1 : -1;
        rb.velocity = new Vector2(direction * moveSpeed.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is damageable
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // Determine the knockback direction based on projectile's facing direction
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // Attempt to apply damage and knockback to the target
            bool gotHit = damageable.Hit(damage, deliveredKnockback);

            // Log the hit and destroy the projectile if it successfully hit a target
            if (gotHit)
            {
                Debug.Log(collision + " hit for " + damage);
                Destroy(gameObject);
            }
        }
    }
}
