using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
/// <summary>
/// PlayerController handles player movement, animations, and interactions, such as running, jumping, attacking, and taking damage.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Movement settings
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpImpulse = 10f;
    public float airSpeed = 3f;

    // Input and state tracking
    private Vector2 moveInput;
    private TouchingDirections touchingDirections;
    private Damageable damageable;

    // Calculates the current move speed based on state
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        return IsRunning ? runSpeed : walkSpeed;
                    }
                    else
                    {
                        return airSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
    }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value); // Update animation
        }
    }

    [SerializeField]
    private bool _isRunning = false;
    private bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value); // Update animation
        }
    }

    [SerializeField]
    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            // Flip the player's orientation if changing direction
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    public bool IsAlive
    {
        get { return animator.GetBool(AnimationStrings.isAlive); }
    }

    // References to components
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        // Initialize component references
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    // FixedUpdate handles physics-based movement updates
    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
        {
            // Apply calculated velocity based on move input
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }

        // Update the Y velocity in the animator for jump animations
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Read movement input from player
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            // Update movement and facing direction
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        // Adjust facing direction based on move input
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        // Toggle running state based on input
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // Handle jump if grounded, allowed to move, and input is received
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        // Trigger attack animation
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        // Trigger ranged attack animation
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttack);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        // Apply knockback force in response to taking damage
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
