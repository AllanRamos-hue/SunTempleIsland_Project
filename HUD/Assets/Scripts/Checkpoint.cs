using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    CharacterMovement player;
    public Text checkpointText;

    bool hasAlreadyHit;

    bool check;

    private void Start()
    {
        player = FindObjectOfType<CharacterMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.respawnPos = transform.position;
            player.respawnRot = transform.rotation;

            if(!hasAlreadyHit)
            StartCoroutine(CheckpointFeedback());
        }
    }

    IEnumerator CheckpointFeedback()
    {
        if (check) yield break;

        check = true;

        Color color = Color.yellow;
        color.a = 0; 

        while (color.a < 1)
        {
            color.a += Time.deltaTime;
            checkpointText.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(2);
        
        while (color.a > 0)
        {
            color.a -= Time.deltaTime;
            checkpointText.color = color;
            yield return null;
        }

        hasAlreadyHit = true;
        
        check = false;
    }
}
