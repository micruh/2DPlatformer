using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MusicPlayer manages background music playback with an introductory clip that transitions seamlessly into a looping track.
/// </summary>
public class MusicPlayer : MonoBehaviour
{
    // Audio sources for the intro and loop portions of the background music
    public AudioSource introSource, loopSource;

    // Start is called before the first frame update
    void Start()
    {
        // Play the intro audio clip immediately
        introSource.Play();

        // Schedule the loop audio clip to start playing as soon as the intro finishes
        loopSource.PlayScheduled(AudioSettings.dspTime + introSource.clip.length);
    }
}
