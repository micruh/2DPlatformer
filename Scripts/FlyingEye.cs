using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FlyingEye controls the movement and behaviors of a flying enemy that follows a set of waypoints.
/// It detects targets in its "bite" detection zone, handles death behavior, and flips its direction as needed.
/// </summary>
public class FlyingEye : MonoBehaviour
{
    // Movement settings
    public float flightSpeed = 2f;
    public float waypointReachedDistance = 1f;

    // Detection zone for identifying targets to engage
    public DetectionZone biteDetectionZone;

    // List of waypoints for patrolling
    public List<Transform> waypoints;

    // Collider that activates upon death to handle interactions with other objects
    public Collider2D deathCollider;

    // References to components
    private Animator animator;
    private Rigidbody2D rb;
    private Damageable damageable;

    // Current waypoint the enemy is moving towards
    private Transform nextWaypoint;
    private int waypointNum = 0;

    // Indicates if the enemy currently has a target
    private bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value); // Update animator based on target status
        }
    }

    // Determines if the enemy is allowed to move based on Animator parameters
    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    private void Awake()
    {
        // Initialize component references
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        // Set the initial waypoint to start patrolling
        nextWaypoint = waypoints[waypointNum];
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there are any targets in the bite detection zone
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            // If the enemy can move, proceed with movement logic
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero; // Stop movement if CanMove is false
            }
        }
        else
        {
            // Handle death behavior: enable gravity, set velocity, and activate death collider
            rb.gravityScale = 2f;
            rb.velocity = new Vector2(0, rb.velocity.y);
            deathCollider.enabled = true;
        }
    }

    /// <summary>
    /// Handles movement to the next waypoint and updates the waypoint target when reached.
    /// </summary>
    private void Flight()
    {
        // Calculate direction to the waypoint and set velocity
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
        rb.velocity = directionToWaypoint * flightSpeed;

        // Check distance to the waypoint and update direction if needed
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        UpdateDirection();

        // If close enough to the waypoint, snap position and update to the next waypoint
        if (distance <= waypointReachedDistance + 0.1f)
        {
            rb.position = nextWaypoint.position; // Snap to waypoint

            // Update waypoint index
            waypointNum++;
            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0; // Loop back to the first waypoint
            }

            // Set the next waypoint target
            nextWaypoint = waypoints[waypointNum];
        }
    }

    /// <summary>
    /// Flips the character's direction based on movement direction to face the current waypoint.
    /// </summary>
    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;

        // Flip the scale based on the velocity to face the correct direction
        if (transform.localScale.x > 0)
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }
}
