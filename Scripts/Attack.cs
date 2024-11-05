using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attack handles applying damage and knockback to any `Damageable` object it collides with,
/// typically when the character or object performs an attack.
/// </summary>
public class Attack : MonoBehaviour
{
    // The amount of damage dealt by this attack
    public int attackDamage = 10;

    // Knockback force applied upon hitting a target
    public Vector2 knockback = Vector2.zero;

    // Triggered when another collider enters this object's trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has a Damageable component
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // Determine knockback direction based on the parent's local scale
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // Attempt to apply damage and knockback
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);
            
            // Log the attack result if the target was hit successfully
            if (gotHit)
            {
                Debug.Log(collision + " hit for " + attackDamage);
            }
        }
    }
}
