using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenObject : PickUpItemController
{
    public static bool hasKey = false;
    public static bool hasChainsaw = false;

    public string objectName;
    public Text advice;
    string corrente = "corrente";
    string door = "door";

    bool check;

    private void Start()
    {
        advice.text = " ";
    }

    public override void PickUp()
    {
        if (hasKey && objectName == door)
        {
            base.PickUp();
        }

        if (hasChainsaw && objectName == corrente)
        {
            base.PickUp();
        }

        if(!hasKey || !hasChainsaw)
        {
            StartCoroutine(Feedback());
        }
    }

    IEnumerator Feedback()
    {
        if (check) yield break;

        check = true;

        advice.text = "Você não possui o item necessário";
        advice.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        advice.gameObject.SetActive(false);

        check = false;
    }
}
