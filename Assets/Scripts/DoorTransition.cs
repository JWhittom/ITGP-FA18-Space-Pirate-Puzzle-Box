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
        doorUsed = true;
        Vector3 rotationDirection = CalculateRotation();
        Quaternion playerRotation = player.transform.rotation;
        player.transform.parent = level.transform;
        int degreesRotated = 0, degreesPerFrame = 3;
        while (degreesRotated < 90)
        {
            level.transform.Rotate(rotationDirection * degreesPerFrame, Space.World);
            degreesRotated += degreesPerFrame;
            yield return null;
        }
        player.transform.parent = null;
        player.transform.position = playerSetPoint.position;
        player.transform.rotation = playerRotation;
    }

    private Vector3 CalculateRotation()
    {
        Vector3 rotationDirection = transform.position - rotationPoint.position;
        if (Mathf.Abs(rotationDirection.x) > Mathf.Abs(rotationDirection.y))
        {
            rotationDirection.y = Mathf.Abs(rotationDirection.x);
            rotationDirection.x = 0;
        }
        else
        {
            rotationDirection.x = Mathf.Abs(rotationDirection.y);
            rotationDirection.y = 0;
        }
        return Vector3.Normalize(rotationDirection);
    }
}
