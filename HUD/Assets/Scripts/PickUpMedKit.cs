using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMedKit : PickUpItemController
{
    public float lifeEarned;

    PlayerLife player;

    void Start()
    {
        player = FindObjectOfType<PlayerLife>();
    }

    public override void PickUp()
    {
        base.PickUp();
    
        player.ReceiveDamage(lifeEarned);
    }
}
