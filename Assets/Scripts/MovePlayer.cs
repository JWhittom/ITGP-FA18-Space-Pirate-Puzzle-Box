using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private string horizontalAxisName;

    [SerializeField]
    private string verticalAxisName;

    [SerializeField]
    private float speed;

    private float horizontalAxisValue;
    private float verticalAxisValue;

    RaycastHit rayHit;
    private bool canMove;
    private Vector3 MoveVector;

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
       // if (canMove)
       // {
            Move();        
        //}
    }

    void GetInput()
    {
        horizontalAxisValue = Input.GetAxis(horizontalAxisName);//stores my input for each update
        verticalAxisValue = Input.GetAxis(verticalAxisName);
    }

    private void Move()
    {
        MoveVector = new Vector3(horizontalAxisValue * speed * Time.deltaTime, 0, verticalAxisValue * speed * Time.deltaTime);
        transform.Translate(MoveVector);
    }
}
