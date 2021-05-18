using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpItemController : MonoBehaviour
{
    public virtual void PickUp()
    {
        gameObject.SetActive(false);
    }
}
