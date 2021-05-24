using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : PickUpItemController
{
    public string keyName;
    
    public string key1 = "chave";
    public string chainSaw = "chainsaw";

    public GameObject keyIcon;

    public override void PickUp()
    {
        base.PickUp();

        keyIcon.SetActive(true);

        if (keyName == key1)
        {
            OpenObject.hasKey = true;
        }

        if (keyName == chainSaw) 
        {
            OpenObject.hasChainsaw = true;
        }
    }
}
