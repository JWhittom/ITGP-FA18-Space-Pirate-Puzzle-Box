using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTransition : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The point to direct rotation when transitioning rooms.\nShould be aligned with the center of the door on two axes.")]
    private Transform rotationPoint;

    [SerializeField]
    [Tooltip("The point to set the player to after transitioning.")]
    private Transform playerSetPoint;

    private bool doorUsed;
    private GameObject level;

	// Use this for initialization
	void Start ()
    {
        level = GameObject.FindGameObjectWithTag("Level");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !doorUsed)
            StartCoroutine(RotateRoom(collision.gameObject));
    }

    private IEnumerator RotateRoom(GameObject player)
    {
        // Prevent the coroutine from starting more than once
        doorUsed = true;
        Vector3 rotationDirection = CalculateRotationAxis();
        // Save player rotation to reset it after the cube moves
        Quaternion playerRotation = player.transform.rotation;
        // Set the level as the player's parent to make them move together
        player.transform.parent = level.transform;
        // Variables to track rotation per frame and stop the loop when necessary
        // degreesPerFrame must be a factor of 90 if it's modified
        int degreesRotated = 0, degreesPerFrame = 3;
        while (degreesRotated < 90)
        {
            // Rotate in world space on the axis determined above by so many degrees per frame
            // Using world space makes rotation consistent regardless of the orientation of the cube
            level.transform.Rotate(rotationDirection * degreesPerFrame, Space.World);
            // Add to the degrees rotated so the loop stops at 90 degrees
            degreesRotated += degreesPerFrame;
            yield return null;
        }
        // Remove the player from the level
        player.transform.parent = null;
        // Move the player to the given point (kind of a hack for now)
        player.transform.position = playerSetPoint.position;
        // Set the player rotation back to what it was before
        player.transform.rotation = playerRotation;
    }

    /// <summary>
    /// Calculate the axis on which to rotate based on the direction point on the object
    /// </summary>
    /// <returns>A normalized Vector3 of either the X or Y axis to rotate on.</returns>
    private Vector3 CalculateRotationAxis()
    {
        // Calculate a vector between the door's position and that of the direction point
        Vector3 rotationDirection = transform.position - rotationPoint.position;
        // If the X value is farther from 0 than the Y value, w'ere rotating horizontally around the Y axis
        if (Mathf.Abs(rotationDirection.x) > Mathf.Abs(rotationDirection.y))
        {
            // Change the magnitude to the Y axis and set the X to 0
            rotationDirection.y = Mathf.Abs(rotationDirection.x);
            rotationDirection.x = 0;
        }
        // Otherwise, we'll do the opposite, rotating vertically around the X axis
        else
        {
            // Change the magnitude to the X axis and set the Y axis to 0
            rotationDirection.x = Mathf.Abs(rotationDirection.y);
            rotationDirection.y = 0;
        }
        // Return a normalized Vector3 based on the results
        return Vector3.Normalize(rotationDirection);
    }
}
