using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectionController : MonoBehaviour
{
    Ray ray;

    Transform _selection;

    public GameObject text;

    PlayerLife life;

    private void Start()
    {
        life = GetComponent<PlayerLife>();

        text.SetActive(false);
    }

    void Update()
    {
        SelectObject();

        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUpSelection();
        }

    }

    void SelectObject()
    {
        if (_selection != null)
        {
            text.SetActive(false);
        }

        _selection = null;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5))
        {
            var selection = hit.transform;

            if (selection.CompareTag("Selectable"))
            {
                text.SetActive(true);
            }
            else
            {
                text.SetActive(false);
            }

            _selection = selection;
        }
    }

    void PickUpSelection()
    {
        if (_selection != null)
        {
            PickUpController pickUpController = _selection.GetComponent<PickUpController>();
            if (pickUpController)
            {
                pickUpController.PickUp();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shootable"))
        {
            life.ReceiveDamage(-20);

            Destroy(other);
        }
    }
}

    

