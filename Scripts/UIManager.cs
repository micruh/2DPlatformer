using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

/// <summary>
/// UIManager handles the creation and display of floating damage and health text in the game UI.
/// It listens for character damage and healing events, showing text feedback to the player.
/// </summary>
public class UIManager : MonoBehaviour
{
    // Prefabs for the damage and health text
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    // Reference to the game canvas where text will be displayed
    public Canvas gameCanvas;

    private void Awake()
    {
        // Find the game canvas in the scene
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        // Subscribe to damage and healing events
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        // Unsubscribe from damage and healing events
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    /// <summary>
    /// Displays floating text to indicate damage received by a character.
    /// </summary>
    /// <param name="character">The character that took damage</param>
    /// <param name="damageReceived">The amount of damage received</param>
    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        // Calculate the screen position for the floating text based on the character's world position
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        // Instantiate damage text at the calculated position within the game canvas
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString(); // Set the damage amount text
    }

    /// <summary>
    /// Displays floating text to indicate health restored to a character.
    /// </summary>
    /// <param name="character">The character that was healed</param>
    /// <param name="healthRestored">The amount of health restored</param>
    public void CharacterHealed(GameObject character, int healthRestored)
    {
        // Calculate the screen position for the floating text based on the character's world position
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        // Instantiate health text at the calculated position within the game canvas
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = healthRestored.ToString(); // Set the health amount text
    }
}
