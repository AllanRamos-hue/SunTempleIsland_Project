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
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyLife>().TakeDamage(damage);
            gameObject.SetActive(false);
        }

        if(other.CompareTag("Shootable"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
