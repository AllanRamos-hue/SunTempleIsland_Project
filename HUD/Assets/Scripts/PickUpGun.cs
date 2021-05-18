using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGun : MonoBehaviour
{
    public GameObject objectPickepUp;

    public Transform gunsParent;
    
    public void PickUp()
    {
        if(gameObject.name == objectPickepUp.name)
        {
            objectPickepUp.SetActive(true);

            objectPickepUp.transform.SetAsFirstSibling(); 

            gameObject.SetActive(false);

            for (int i = 0; i < gunsParent.childCount; i++)
            {
                if (i != 0)
                {
                    gunsParent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}
