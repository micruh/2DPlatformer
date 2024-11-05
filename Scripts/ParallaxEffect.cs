using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ParallaxEffect creates a parallax scrolling effect by moving the object relative to the camera's movement,
/// giving a sense of depth in 2D environments. The effect intensity depends on the distance between the object and the camera.
/// </summary>
public class ParallaxEffect : MonoBehaviour
{
    // Reference to the main camera for parallax calculation
    public Camera cam;

    // Reference to the object to follow (e.g., player)
    public Transform followTarget;

    // Stores the starting position of the object
    private Vector2 startingPosition;

    // Stores the starting Z position to maintain depth
    private float startingZ;

    // Calculates the camera's movement since the start
    private Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    // Calculates the Z distance from the object to the follow target
    private float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // Determines the appropriate clipping plane for parallax based on Z distance
    private float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // Parallax factor scales the movement effect based on Z distance
    private float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        // Record the starting position and Z depth of the object
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame to apply the parallax effect
    void Update()
    {
        // Calculate the new position based on the camera's movement and parallax factor
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        // Apply the calculated position, maintaining the original Z depth
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
