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
            RotateRoom(collision.gameObject);
    }

    private void RotateRoom(GameObject player)
    {
        Vector3 rotationDirection = CalculateRotation();
        Debug.Log(rotationDirection);
        player.transform.parent = level.transform;
        level.transform.Rotate(rotationDirection * 90, Space.World);
        player.transform.parent = null;
        player.transform.position = playerSetPoint.position;
        player.transform.rotation = playerSetPoint.rotation;
        doorUsed = true;
    }

    private Vector3 CalculateRotation()
    {
        Vector3 rotationDirection = Vector3.Normalize(transform.position - rotationPoint.position);
        if(rotationDirection.x != 0)
        {
            rotationDirection.y = rotationDirection.x;
            rotationDirection.x = 0;
        }
        else if(rotationDirection.y != 0)
        {
            rotationDirection.x = rotationDirection.y;
            rotationDirection.y = 0;
        }
        return rotationDirection;
    }
}
