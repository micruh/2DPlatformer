using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// HealthText displays floating text that shows health change values, moving upward and fading out over time.
/// This script is typically used to display damage or healing feedback to the player.
/// </summary>
public class HealthText : MonoBehaviour
{
    // Speed at which the text moves upward
    public Vector3 moveSpeed = new Vector3(0, 75, 0);

    // Duration of the fade-out effect
    public float timeToFade = 1f;

    // Tracks the time elapsed since the text appeared
    private float timeElapsed = 0f;

    // Starting color of the text for fading calculations
    private Color startColor;

    // References to the RectTransform and TextMeshPro components
    private RectTransform textTransform;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        // Initialize references to RectTransform and TextMeshPro components
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Store the initial color of the text for the fade-out effect
        startColor = textMeshPro.color;
    }

    private void Update()
    {
        // Move the text upward over time
        textTransform.position += moveSpeed * Time.deltaTime;

        // Increment the time elapsed for fade-out calculations
        timeElapsed += Time.deltaTime;

        // Calculate and apply the fade-out effect
        if (timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            // Destroy the text object after it has fully faded
            Destroy(gameObject);
        }
    }
}
