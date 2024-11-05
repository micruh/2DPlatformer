using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
/// <summary>
/// The Knight class controls a character that patrols, detects cliffs, attacks, and responds to hits.
/// This includes handling movement direction, attack targeting, and damage knockback.
/// </summary>
public class Knight : MonoBehaviour
{
    // Movement settings for the knight
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f;

    // Zones for detecting attack targets and cliffs
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    // Component references
    private Rigidbody2D rb;
    private TouchingDirections touchingDirections;
    private Animator animator;
    private Damageable damageable;

    // Enumeration for movement direction
    public enum WalkableDirection { Right, Left }

    // Current walking direction and corresponding movement vector
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            // Change direction if it differs from the current direction
            if (_walkDirection != value)
            {
                // Flip the knight's scale to face the new direction
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                // Update the direction vector based on the new direction
                walkDirectionVector = (value == WalkableDirection.Right) ? Vector2.right : Vector2.left;
            }
            _walkDirection = value;
        }
    }

    // Target detection properties
    private bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value); // Update the animator for targeting
        }
    }

    // Movement control based on animator state
    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    // Attack cooldown property for controlling attack rate
    public float AttackCooldown
    {
        get { return animator.GetFloat(AnimationStrings.attackCooldown); }
        private set { animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0)); }
    }

    private void Awake()
    {
        // Initialize component references
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update target detection status
        HasTarget = attackZone.detectedColliders.Count > 0;

        // Decrement the attack cooldown if applicable
        if (AttackCooldown > 0)
            AttackCooldown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // Flip direction if grounded and touching a wall
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        // Control velocity only if not locked by other animations or effects
        if (!damageable.LockVelocity)
        {
            if (CanMove)
            {
                // Apply acceleration to reach max speed, clamped within limits
                rb.velocity = new Vector2(
                    Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed),
                    rb.velocity.y
                );
            }
            else
            {
                // Gradually reduce speed to stop when movement is restricted
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
    }

    /// <summary>
    /// Flips the knight's walking direction.
    /// </summary>
    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal values of Right or Left");
        }
    }

    /// <summary>
    /// Responds to being hit by applying knockback.
    /// </summary>
    /// <param name="damage">Damage value received</param>
    /// <param name="knockback">Knockback force applied</param>
    public void OnHit(int damage, Vector2 knockback)
    {
        // Apply knockback force in response to the hit
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    /// <summary>
    /// Called when a cliff is detected, causing the knight to flip direction.
    /// </summary>
    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
