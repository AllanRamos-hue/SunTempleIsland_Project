using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public string gunName;
    public int damage;

    PlayerLife player;

    private void Start()
    {
        player = FindObjectOfType<PlayerLife>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.ReceiveDamage(damage);
        }
    }
}
