using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : PickUpItemController
{
    public int earnedAmmo;

    public Weapon weapon;

    public override void PickUp()
    {
        if(weapon.gameObject.activeSelf)
        {
            base.PickUp();
            weapon.ReceiveAmmo(earnedAmmo);
        }
    }
}