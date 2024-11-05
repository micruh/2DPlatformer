using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Damageable handles the health, damage, and healing mechanics for an object.
/// It supports invincibility frames after taking damage and triggers events for damage and healing interactions.
/// </summary>
public class Damageable : MonoBehaviour
{
    // Event triggered when this object takes damage, passing the damage amount and knockback vector
    public UnityEvent<int, Vector2> damageableHit;

    // Event triggered when health changes, passing the current health and maximum health
    public UnityEvent<int, int> healthChanged;

    // Animator to control animations for the damageable object
    Animator animator;

    // Timer to track invincibility frames
    private float timeSinceHit = 0;

    // Duration of invincibility frames after taking damage
    public float invincibilityTime = 0.25f;

    // Tracks whether the object is currently invincible
    [SerializeField]
    private bool isInvincible = false;

    // Maximum health of the object
    [SerializeField]
    private float _maxHealth = 100;
    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    // Current health of the object
    [SerializeField]
    private float _health = 100;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            healthChanged?.Invoke((int)_health, (int)MaxHealth); // Trigger health change event
            if (_health <= 0)
            {
                IsAlive = false; // Set alive status to false if health is zero or below
            }
        }
    }

    // Tracks whether the object is alive
    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set to " + value);
        }
    }

    // Controls whether the object's velocity is locked (e.g., during animations)
    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
        set { animator.SetBool(AnimationStrings.lockVelocity, value); }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Manage invincibility frames after taking damage
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    /// <summary>
    /// Applies damage to the object if it is alive and not invincible, triggering hit animations and events.
    /// </summary>
    /// <param name="damage">Amount of damage to apply</param>
    /// <param name="knockback">Knockback force to apply</param>
    /// <returns>True if the object was damaged, false otherwise</returns>
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage; // Apply damage
            isInvincible = true; // Activate invincibility
            animator.SetTrigger(AnimationStrings.hitTrigger); // Trigger hit animation
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback); // Trigger damage event
            CharacterEvents.characterDamaged.Invoke(gameObject, damage); // Global character damaged event
            return true;
        }
        return false;
    }

    /// <summary>
    /// Heals the object, ensuring health does not exceed maximum health, and triggers healing events.
    /// </summary>
    /// <param name="healthRestore">Amount of health to restore</param>
    public void Heal(int healthRestore)
    {
        if (IsAlive)
        {
            int maxHeal = Mathf.Max((int)(MaxHealth - Health), 0); // Determine max possible healing
            int actualHeal = Mathf.Min(maxHeal, healthRestore); // Calculate actual heal amount
            Health += actualHeal;
            CharacterEvents.characterHealed.Invoke(gameObject, actualHeal); // Global character healed event
        }
    }
}
