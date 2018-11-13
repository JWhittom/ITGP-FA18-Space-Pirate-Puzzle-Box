using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPivot : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Button to use to pivot room.")]
    private string pivotButtonName;

    private bool shouldPivot;

    private GameObject level;

	// Use this for initialization
	void Start ()
    {
        level = GameObject.FindGameObjectWithTag("Level");
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetPivotInput();
	}

    private void FixedUpdate()
    {
        if (shouldPivot)
            PivotRoom();
    }

    private void PivotRoom()
    {
        level.transform.Rotate(Vector3.back * 90, Space.World);
    }

    private void GetPivotInput()
    {
        shouldPivot = Input.GetButtonDown(pivotButtonName);
    }
}
