using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TouchingDirections detects if the object is grounded, touching a wall, or touching the ceiling by casting rays in each direction.
/// It updates Animator parameters accordingly to enable relevant animations and actions based on contact with surfaces.
/// </summary>
public class TouchingDirections : MonoBehaviour
{
    // Contact filter for detecting surfaces
    public ContactFilter2D castFilter;

    // Distances for checking ground, wall, and ceiling proximity
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.5f;

    // References to components
    private CapsuleCollider2D touchingCol;
    private Animator animator;

    // Arrays for storing raycast hit results in each direction
    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded;
    public bool IsGrounded
    {
        get { return _isGrounded; }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value); // Update animator for ground status
        }
    }

    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall
    {
        get { return _isOnWall; }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value); // Update animator for wall contact
        }
    }

    [SerializeField]
    private bool _isCeiling;
    public bool IsOnCeiling
    {
        get { return _isCeiling; }
        private set
        {
            _isCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value); // Update animator for ceiling contact
        }
    }

    // Direction for wall check based on object's facing direction
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    private void Awake()
    {
        // Initialize component references
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // FixedUpdate handles physics-based contact checks
    private void FixedUpdate()
    {
        // Cast rays downward, sideways, and upward to check for ground, wall, and ceiling contact
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
