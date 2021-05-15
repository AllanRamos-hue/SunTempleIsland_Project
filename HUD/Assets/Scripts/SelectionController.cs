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
            PickUpGun();
            PickUpMedKit();
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

    void PickUpGun()
    {
        if (_selection != null)
        {
            PickUpGun pickUpGun = _selection.GetComponent<PickUpGun>();
            if (pickUpGun)
            {
                pickUpGun.PickUp();
            }
        }
    }

    void PickUpMedKit()
    {
        if (_selection != null)
        {
            PickUpMedKit pickUpMed = _selection.GetComponent<PickUpMedKit>();

            if (pickUpMed)
            {
                pickUpMed.Heal();
            }
        }
        Debug.Log("Curou");
    }
}

    

