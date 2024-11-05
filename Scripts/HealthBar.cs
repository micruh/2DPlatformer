using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// HealthBar manages the display of the player's health on the UI, including a slider and text.
/// It updates in response to health changes, allowing the player to monitor health in real-time.
/// </summary>
public class HealthBar : MonoBehaviour
{
    // UI elements for displaying health: a slider and a text component
    public Slider healthSlider;
    public TMP_Text healthBarText;

    // Reference to the player's Damageable component to access health data
    private Damageable playerDamageable;

    private void Awake()
    {
        // Find the player by tag and get its Damageable component
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("No player found in the scene. Make sure it has tag 'Player'");
        }
        playerDamageable = player.GetComponent<Damageable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the health bar with the player's current health
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    /// <summary>
    /// Calculates the percentage of health for the slider based on current and max health.
    /// </summary>
    /// <param name="currentHealth">Current health of the player</param>
    /// <param name="maxHealth">Maximum health of the player</param>
    /// <returns>A float representing the percentage of health (0 to 1)</returns>
    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    // Called when the component is enabled, subscribes to health change events
    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    // Called when the component is disabled, unsubscribes from health change events
    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }
    
    /// <summary>
    /// Updates the health bar UI when the player's health changes.
    /// </summary>
    /// <param name="newHealth">The new health value</param>
    /// <param name="maxHealth">The maximum health value</param>
    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth); // Update slider value
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;           // Update health text
    }
}
