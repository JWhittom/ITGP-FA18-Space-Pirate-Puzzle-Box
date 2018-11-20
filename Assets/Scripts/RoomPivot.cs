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
        //float currentWorldAngle = level.transform.rotation.eulerAngles.z;
        //Debug.Log(currentWorldAngle);
        int degreesRotated = 0, degreesPerFrame = 3;
        transform.parent = null;
        spinning = true;
        while (degreesRotated < 90)
        {
            //levelAnimator.Play("PivotCounterClockwise");
            level.transform.Rotate(Vector3.forward * degreesPerFrame, Space.World);
            degreesRotated += degreesPerFrame;
            yield return null;
            //Debug.Log(level.transform.rotation.z);
        }
        spinning = false;
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
