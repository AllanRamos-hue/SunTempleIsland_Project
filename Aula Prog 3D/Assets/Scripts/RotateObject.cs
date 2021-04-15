using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
   [SerializeField] Vector3 rotateAngle;
    void Update()
    {
        transform.Rotate(rotateAngle * Time.deltaTime);
    }
}
