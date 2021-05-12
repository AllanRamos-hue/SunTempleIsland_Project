using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    public float sensibility = 1;

    Transform head;

    Vector3 rotationHead;

    private void Start()
    {
        head = transform.GetChild(0);

        rotationHead = Vector3.zero;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Rotacação do corpo
        Vector3 rotationBody = transform.localEulerAngles;
        rotationBody.y += Input.GetAxis("Mouse X") * sensibility;
        transform.localEulerAngles = rotationBody;

        //Rotatação da cabeça
        rotationHead.x -= Input.GetAxis("Mouse Y") * sensibility;
        rotationHead.x = Mathf.Clamp(rotationHead.x,-60, 60);
        head.localEulerAngles = rotationHead;
    }
}
