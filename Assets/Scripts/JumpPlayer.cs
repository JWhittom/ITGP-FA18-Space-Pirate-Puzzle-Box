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
    private float normalGravityMultiplier;//gravity on the way up

    [SerializeField]
    private float fallMultiplier;//gravity on the way down


    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update ()
    {
        Debug.Log("On ground is: " + isOnGround().ToString());
        Jump();
    }

    /// <summary>
    /// If the player presses the jump button down and the ground check returns true, the rigidbody gets upward force
    /// </summary>
    void Jump()
    {
        //if (rigidbody.velocity.y < 0)
        //{
        //    rigidbody.gravityScale = fallMultiplier;//this controls how fast the player falls
        //}
        //else
        //{
        //    rigidbody.gravityScale = normalGravityMultiplier;
        //}

        if (Input.GetButtonDown(jumpButtonName) && isOnGround())//if we on ground and we can jump
        {
            Debug.Log("trying to jump");
            rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Raycasts downwards and looks for anything on the ground layer. If nothing, it returns false
    /// </summary>
    /// <returns></returns>
    private bool isOnGround()
    {
        if (Physics.Raycast(transform.position, Vector2.down, GetComponent<Renderer>().bounds.extents.y + .1f, ground))
        {
            return true;
        }

        else return false;
    }
}
