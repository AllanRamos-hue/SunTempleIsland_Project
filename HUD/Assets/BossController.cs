using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public List<GameObject> turrets;
    
    public GameObject winPanel;

    void Start()
    {
        winPanel.SetActive(false);
    }

    void Update()
    {
        bool tempSolved = true;

        for (int i = 0; i < turrets.Count; i++)
        {
            if (turrets[i].activeSelf) tempSolved = false;
        }

        if(tempSolved)
        {
            winPanel.SetActive(true);
        }
    }
}
