using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObject : PickUpItemController
{
    public static bool hasKey = false;
    public static bool hasChainsaw = false;

    public string objectName;
    string corrente = "corrente";
    string door = "door";

    public override void PickUp()
    {
        if (hasKey && objectName == door)
        {
            base.PickUp();
        }

        if (hasChainsaw && objectName == corrente)
        {
            base.PickUp();
        }
    }
}
