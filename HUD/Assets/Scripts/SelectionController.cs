using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectionController : MonoBehaviour
{
    Ray ray;

    Transform _selection;

    public GameObject text;

    public Transform gunsParent;

    public AudioClip changeGunSFX;

    private void Start()
    {
        text.SetActive(false);
    }

    void Update()
    {
        SelectObject();

        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUpGun();
            PickUpMedKit();
            PickUpAmmo();
            PickUpKey();
            OpenDoor();

        }

        if (Input.GetKeyDown(KeyCode.Q))
            ChangeGun();

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

        if (Physics.Raycast(ray, out hit, 3))
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

    void ChangeGun()
    {
        if(gunsParent.childCount >= 2)
        {
            gunsParent.GetChild(0).gameObject.SetActive(false);
            gunsParent.GetChild(0).SetAsLastSibling();

            for (int i = 0; i < gunsParent.childCount; i++)
            {
                if (i == 0)
                {
                    gunsParent.GetChild(i).gameObject.SetActive(true);
                    gunsParent.GetChild(i).transform.SetAsFirstSibling();
                }
            }

            AudioManager.PlaySFX(changeGunSFX);
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
                pickUpMed.PickUp();
            }
        }
    }

    void PickUpAmmo()
    {
        if (_selection != null)
        {
            PickUpAmmo pickUpAmmo = _selection.GetComponent<PickUpAmmo>();

            if (pickUpAmmo)
            {
                pickUpAmmo.PickUp();
            }
        }
    }

    void PickUpKey()
    {
        Key key = _selection.GetComponent<Key>();

        if(key)
        {
            key.PickUp();
        }
    }

    void OpenDoor()
    {
        OpenObject door = _selection.GetComponent<OpenObject>();

        if(door)
        {
            door.PickUp();
        }
    }
}
