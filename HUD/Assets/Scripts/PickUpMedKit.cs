using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMedKit : MonoBehaviour
{
    public float lifeEarned;

    PlayerLife player;

    void Start()
    {
        player = FindObjectOfType<PlayerLife>();
    }

    public void Heal()
    {
        player.ReceiveDamage(lifeEarned);

        gameObject.SetActive(false);
    }
}
