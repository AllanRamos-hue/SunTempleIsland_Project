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

    PlayerLife player;

    Vector3 move;

    Vector3 respawnPos;
    Quaternion respawnRot;

    bool respawning;


    void Start()
    {
        move = Vector3.zero;

        controller = GetComponent<CharacterController>();

        player = FindObjectOfType<PlayerLife>();

        respawnPos = transform.position;
        respawnRot = transform.rotation;
    }

    void Update()
    {
        if(controller.isGrounded)
        {
            move.y = 0;
            Jump();
        }

        Move();     
        
        if(player.Die())
        {
            StartCoroutine(Respawn());
            return;
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fence"))
        {
            player.ReceiveDamage(-15);
        }    
    }

    IEnumerator Respawn()
    {
        if (respawning) yield break;

        --GameOverScreen.round;

        respawning = true;

        transform.SetPositionAndRotation(respawnPos, respawnRot);

        player.Revive();

        respawning = false;
    }
}
