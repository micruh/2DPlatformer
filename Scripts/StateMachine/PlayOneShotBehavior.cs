using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayOneShotBehavior handles playing a specified sound clip at various points
/// (on enter, on exit, or after a delay) within an animator state.
/// </summary>
public class PlayOneShotBehavior : StateMachineBehaviour
{
    // Sound clip to play
    public AudioClip soundToPlay;

    // Volume level of the sound clip
    public float volume = 1f;

    // Options to play sound on entering, exiting, or after a delay within the state
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;

    // Delay time before playing sound (if playAfterDelay is true)
    public float playDelay = 0.25f;

    // Tracks time since the state was entered and whether delayed sound has played
    private float timeSinceEntered = 0;
    private bool hasDelayedSoundPlayed = false;

    // Called when a transition starts and the animator starts evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }

        // Reset tracking variables for delay-based playback
        timeSinceEntered = 0f;
        hasDelayedSoundPlayed = false;
    }

    // Called every frame while this state is being evaluated
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check if sound should play after a delay
        if (playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;

            if (timeSinceEntered > playDelay)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
                hasDelayedSoundPlayed = true;
            }
        }
    }

    // Called when a transition ends and the animator stops evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }
    }
}
