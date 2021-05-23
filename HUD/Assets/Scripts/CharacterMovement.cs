using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public float speed = 5;

    public float jumpForce = 5;

    public float playerGravity = 8;

    public Image fade;

    public AudioClip fenceDamageSFX;
    
    CharacterController controller;

    PlayerLife player;

    Vector3 move;

    [HideInInspector]
    public Vector3 respawnPos;
    
    [HideInInspector]
    public Quaternion respawnRot;

    bool respawning;


    void Start()
    {
        respawnPos = transform.position;
        respawnRot = transform.rotation;
        
        move = Vector3.zero;

        controller = GetComponent<CharacterController>();

        player = FindObjectOfType<PlayerLife>();    
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
            AudioManager.PlaySFX(fenceDamageSFX);
            player.ReceiveDamage(-15);
        }    
    }

    IEnumerator Respawn()
    {
        if (respawning) yield break;

        --GameOverScreen.round;

        respawning = true;

        EnemyTurret[] turrets = FindObjectsOfType<EnemyTurret>();
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        EnemyPatrol[] patrols = FindObjectsOfType<EnemyPatrol>();

        foreach (EnemyTurret t in turrets)
        {
            t.enabled = false;
        }

        foreach (Enemy e in enemies)
        {
            e.enabled = false;
        }

        foreach (EnemyPatrol p in patrols)
        {
            p.enabled = false;
        }

        GetComponent<CharacterMovement>().enabled = false;
        GetComponent<PlayerVision>().enabled = false;

        Color color = Color.black;
        color.a = 0;

        yield return new WaitForSeconds(1);
        
        while (color.a < 1)
        {
            color.a += Time.deltaTime / 2;
            fade.color = color;
            yield return null;
        }

        transform.position = respawnPos;
        transform.rotation = respawnRot;
        player.Revive();

        GetComponent<CharacterMovement>().enabled = true;
        GetComponent<PlayerVision>().enabled = true;

        foreach (EnemyTurret t in turrets)
        {
            t.enabled = true;
        }

        foreach (Enemy e in enemies)
        {
            e.enabled = true;
        }

        foreach (EnemyPatrol p in patrols)
        {
            p.enabled = true;
        }

        while (color.a > 0)
        {
            color.a -= Time.deltaTime / 2;
            fade.color = color;
            yield return null;
        }

        respawning = false;   
    }
}
