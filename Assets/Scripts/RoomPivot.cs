using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPivot : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Button to use to pivot room.")]
    private string pivotButtonName;

    private Animator levelAnimator;

    /// <summary>
    /// Do we have input for pivoting?
    /// </summary>
    private bool shouldPivot;

    /// <summary>
    /// Are we on a platform for pivoting?
    /// </summary>
    private bool canPivot;

    private bool spinning;

    private GameObject level;

    private float rotateMargin = 0.5f;
    

	// Use this for initialization
	void Start ()
    {
        level = GameObject.FindGameObjectWithTag("Level");
        levelAnimator = level.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetPivotInput();
	}

    private void FixedUpdate()
    {
        if (shouldPivot && canPivot && !spinning)
            StartCoroutine(PivotRoom());
    }

    private IEnumerator PivotRoom()
    {
        // Variables to track rotation per frame and stop the loop when necessary
        // degreesPerFrame must be a factor of 90 if it's modified
        int degreesRotated = 0, degreesPerFrame = 3;
        // Remove the platform from the level transform so it stays in place while the level rotates
        transform.parent = null;
        // Prevent the coroutine from running multiple times at once
        spinning = true;
        while (degreesRotated < 90)
        {
            // Rotate counterclockwise by so many degrees per frame in world space
            // Being in world space gives the rotation the same visible effect regardless of the level's local rotation
            level.transform.Rotate(Vector3.forward * degreesPerFrame, Space.World);
            // Add to the degrees rotated so the loop stops at 90 degrees
            degreesRotated += degreesPerFrame;
            yield return null;
        }
        // Allow the coroutine to happen again later
        spinning = false;
        // Set the parent again; the platform will once again move with the level
        transform.parent = level.transform;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            canPivot = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            canPivot = false;
    }

    private void GetPivotInput()
    {
        shouldPivot = Input.GetButtonDown(pivotButtonName);
    }
}
