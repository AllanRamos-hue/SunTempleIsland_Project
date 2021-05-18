using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : PickUpItemController
{
    public int earnedAmmo;

    public Weapon[] weapons;

    public override void PickUp()
    {
        base.PickUp();

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].ReceiveAmmo(earnedAmmo);
        }
    }
}