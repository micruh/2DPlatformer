using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ProjectileLauncher handles the instantiation and launching of projectiles
/// from a specified launch point, setting their direction and velocity based on
/// the facing direction of the launcher (e.g., player).
/// </summary>
public class ProjectileLauncher : MonoBehaviour
{
    // The point from which the projectile is launched
    public Transform launchPoint;

    // Prefab for the projectile to be instantiated
    public GameObject projectilePrefab;

    // Speed at which the projectile is launched
    public float projectileSpeed = 30f;

    /// <summary>
    /// Instantiates and launches a projectile in the direction the player is facing.
    /// </summary>
    public void FireProjectile()
    {
        // Instantiate the projectile at the launch point
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);

        // Adjust the projectile's orientation based on the player's facing direction
        if (transform.localScale.x < 0) // Player is facing left
        {
            Vector3 flippedScale = projectile.transform.localScale;
            flippedScale.x = -Mathf.Abs(flippedScale.x); // Ensure x is negative for leftward direction
            projectile.transform.localScale = flippedScale;
        }
        else // Player is facing right
        {
            Vector3 normalScale = projectile.transform.localScale;
            normalScale.x = Mathf.Abs(normalScale.x); // Ensure x is positive for rightward direction
            projectile.transform.localScale = normalScale;
        }

        // Apply velocity to the projectile's Rigidbody2D component if it exists
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float direction = transform.localScale.x > 0 ? 1 : -1; // Determine direction based on player's scale
            rb.velocity = new Vector2(direction * projectileSpeed, 0); // Apply velocity in the correct direction
        }
    }
}
