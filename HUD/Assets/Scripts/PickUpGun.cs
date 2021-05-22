using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGun : MonoBehaviour
{
    public GameObject objectPickepUp;

    public Transform gunsParent;

    public AudioClip SFX;

    public void PickUp()
    {
        objectPickepUp.SetActive(true);

        objectPickepUp.transform.SetParent(gunsParent);

        objectPickepUp.transform.SetPositionAndRotation(gunsParent.position, gunsParent.rotation);

        objectPickepUp.transform.SetAsFirstSibling();

        gameObject.SetActive(false);

        AudioManager.PlaySFX(SFX);

        for (int i = 0; i < gunsParent.childCount; i++)
        {
            if (i != 0)
            {
                gunsParent.GetChild(i).gameObject.SetActive(false);
            }
        }
        
    }
}
