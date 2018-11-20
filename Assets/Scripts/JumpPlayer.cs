using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    [SerializeField]
    private string jumpButtonName;

    [SerializeField]
    private LayerMask ground;//This is how we know what the player can jump on

    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    [Tooltip("Turn off gravity and let this variable represent the amount of gravity that acts upon this object.")]
    private float normalGravityMultiplier;//gravity on the way up

    [SerializeField]
    [Tooltip("Turn off gravity and let this variable represent how much gravity acts on this object when it's falling")]
    private float fallMultiplier;//gravity on the way down

    [SerializeField]
    [Tooltip("Origin of ground detect sphere.")]
    private Transform groundDetectPoint;


    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update ()
    {      
        Jump();
    }

    /// <summary>
    /// If the player presses the jump button down and the ground check returns true, the rigidbody gets upward force
    /// </summary>
    void Jump()
    {
        if (Mathf.Floor(rigidbody.velocity.y) <= 0 && !isOnGround())//if we're in the air either falling or at the top of our jump
        {
            rigidbody.AddForce(Vector3.down * fallMultiplier);
        }
        else
        {
            rigidbody.AddForce(Vector3.down * normalGravityMultiplier);
        }

        if (Input.GetButtonDown(jumpButtonName) && isOnGround())//if we on ground and we can jump
        {
            rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Raycasts downwards and looks for anything on the ground layer. If nothing, it returns false
    /// </summary>
    /// <returns></returns>
    private bool isOnGround()
    {
        //Debug.DrawRay(transform.position, Vector3.down * 2);
        //if (Physics.Raycast(transform.position, Vector2.down, /*GetComponent<Renderer>().bounds.extents.y + */2f, ground))
        //{
        //    return true;
        //}

        //else return false;
        return Physics.OverlapSphere(groundDetectPoint.position, .5f, ground).Length > 0;
    }
}
