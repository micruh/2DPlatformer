using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// DetectionZone manages a trigger zone that detects and tracks colliders within it. 
/// It triggers an event when no colliders remain in the zone, making it useful for AI targeting or detection.
/// </summary>
public class DetectionZone : MonoBehaviour
{
    // Event triggered when no colliders are left in the detection zone
    public UnityEvent noCollidersRemain;

    // List of colliders currently detected in the zone
    public List<Collider2D> detectedColliders = new List<Collider2D>();

    // Reference to this object's Collider2D component
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>(); // Initialize the collider reference
    }
    
    // Called when another collider enters the detection zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision); // Add the detected collider to the list
    }

    // Called when another collider exits the detection zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision); // Remove the collider from the list

        // If no colliders remain, invoke the event
        if (detectedColliders.Count <= 0)
        {
            noCollidersRemain.Invoke();
        }
    }
}
