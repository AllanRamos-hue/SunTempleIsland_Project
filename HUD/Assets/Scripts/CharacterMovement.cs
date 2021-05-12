using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public float speed = 5;

    public float jumpForce = 5;

    public float playerGravity = 8;
    
    CharacterController controller;

    Vector3 move;

    void Start()
    {
        move = Vector3.zero;

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(controller.isGrounded)
        {
            move.y = 0;
            Jump();
        }

        Move();      
    }

     void Move()
    {
        move.x = 0;
        move.z = 0;

        move += Input.GetAxis("Horizontal") * transform.right * speed;
        move += Input.GetAxis("Vertical") * transform.forward * speed;
        move.y -= playerGravity * Time.deltaTime;

        controller.Move(move * Time.deltaTime);

    }

    void Jump()
    {
        if(Input.GetButton("Jump"))
        {
            move.y = jumpForce;
        }
    }
}
