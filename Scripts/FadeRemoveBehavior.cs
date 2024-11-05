using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FadeRemoveBehavior gradually fades out a GameObject's SpriteRenderer over a specified duration
/// and destroys the object once the fade is complete. This is typically used for fade-out effects in animations.
/// </summary>
public class FadeRemoveBehavior : StateMachineBehaviour
{
    // Duration of the fade-out effect
    public float fadeTime = 15f;

    // Tracks the elapsed time since the fade began
    private float timeElapsed = 0f;

    // References to the object's SpriteRenderer and GameObject for fading and removal
    private SpriteRenderer spriteRenderer;
    private GameObject objToRemove;

    // Stores the initial color of the sprite for fading calculations
    private Color startColor;

    // Called when a transition starts and the state machine begins evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reset the elapsed time for the fade
        timeElapsed = 0f;

        // Get the SpriteRenderer component and store the starting color
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;

        // Reference the GameObject for potential removal after fading
        objToRemove = animator.gameObject;
    }

    // Called every frame while this state is being evaluated
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Increment the elapsed time
        timeElapsed += Time.deltaTime;

        // Calculate the new alpha based on the fade time
        float newAlpha = startColor.a * (1 - (timeElapsed / fadeTime));
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

        // Destroy the object if the fade time has been exceeded
        if (timeElapsed > fadeTime)
        {
            Destroy(objToRemove);
        }
    }
}
