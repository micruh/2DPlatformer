using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HealthPickup is a collectible that restores a set amount of health to a player or other
/// damageable object upon collision. It also includes a spinning visual effect.
/// </summary>
public class HealthPickup : MonoBehaviour
{
    // Amount of health restored when picked up
    public int healthRestore = 20;

    // Speed at which the pickup spins in the game world
    public Vector3 spinRotateSpeed = new Vector3(0, 180, 0);

    // Audio source to play a sound when the pickup is collected
    private AudioSource pickupSource;

    private void Awake()
    {
        // Initialize the AudioSource component
        pickupSource = GetComponent<AudioSource>();
    }

    // Triggered when another collider enters this object's trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has a Damageable component
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            // Heal the damageable object
            damageable.Heal(healthRestore);

            // Play the pickup sound at the pickup's position
            AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);

            // Destroy the pickup object after use
            Destroy(gameObject);
        }
    }

    // Update is called once per frame to handle the spinning effect
    private void Update()
    {
        // Rotate the pickup over time to create a spinning effect
        transform.eulerAngles += spinRotateSpeed * Time.deltaTime;
    }
}
