using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGun : MonoBehaviour
{
    public GameObject objectPickepUp;

    public GameObject objectDropped;

    public Transform gunsParent;

    bool hasPickedUp = false;

    private void Update()
    {
        if (gunsParent.GetChild(0).gameObject.activeSelf)
        {
            hasPickedUp = true;
        }
    }

    public void PickUp()
    {
        if(gameObject.name == objectPickepUp.name)
        {
            objectPickepUp.SetActive(true);

            objectPickepUp.transform.SetAsFirstSibling(); 

            gameObject.SetActive(false);

            if (hasPickedUp)
                Drop();

        }
    }

    void Drop()
    {    
       
        for (int i = 0; i < gunsParent.childCount; i++)
        {
            if (i != 0)
            {
                gunsParent.GetChild(i).gameObject.SetActive(false);
            }
        }

        Transform player = gunsParent.parent.parent;

        objectDropped.transform.position = player.position + Vector3.left;
        
        objectDropped.SetActive(true);

    }
}
