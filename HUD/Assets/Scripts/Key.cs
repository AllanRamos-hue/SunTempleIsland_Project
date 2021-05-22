using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : PickUpItemController
{
    public string keyName;
    
    public string key1 = "chave";
    public string chainSaw = "chainsaw";

    public override void PickUp()
    {
        base.PickUp();

        if (keyName == key1)
        {
            OpenObject.hasKey = true;
            Debug.Log(OpenObject.hasKey);
        }
            
        
        if (keyName == chainSaw) 
        {
            OpenObject.hasChainsaw = true;
            Debug.Log(OpenObject.hasChainsaw);
        }
    }
}
