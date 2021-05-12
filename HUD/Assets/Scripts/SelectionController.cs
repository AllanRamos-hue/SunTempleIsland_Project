using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{

    Ray ray;
    
    Transform _selection;

    Material defaultMaterial;

    public Color selectionColor;

    PlayerLife life;

    private void Start()
    {
        defaultMaterial = GetComponent<MeshRenderer>().material;

        life = GetComponent<PlayerLife>();
    }

    void Update()
    {
        SelectObject();

        if(Input.GetKeyDown(KeyCode.E))
        {
            PickUpSelection();
        }
       
    }

    void SelectObject()
    {
        if (_selection != null)
        {
            var _selectionRenderer = _selection.GetComponent<MeshRenderer>();
            _selectionRenderer.material = defaultMaterial;
          

            if(_selection.childCount > 0)
            {
                for (int i = 0; i < _selection.childCount; i++)
                {
                    if(_selection.GetChild(i).GetComponent<MeshRenderer>() != null)
                    _selection.GetChild(i).GetComponent<MeshRenderer>().material = defaultMaterial;
                }
                
            }

            _selection = null;
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5))
        {
            var selection = hit.transform;

            if (selection.CompareTag("Selectable"))
            {
                var selectionRenderer = selection.GetComponent<MeshRenderer>();

                if (selectionRenderer != null)
                {
                    selectionRenderer.material.color = selectionColor;

                    if (selection.childCount > 0)
                    {
                        for (int i = 0; i < selection.childCount; i++)
                        {
                            selection.GetChild(i).GetComponent<MeshRenderer>().material.color = selectionColor;
                        }
                    }
                }
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

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Shootable"))
        {
            life.ReceiveDamage(-20);

            Destroy(other);

            Debug.Log("hit");
        }

    }
}
