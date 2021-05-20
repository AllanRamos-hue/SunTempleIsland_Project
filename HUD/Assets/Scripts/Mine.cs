using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mine : MonoBehaviour
{
    [SerializeField] float maxDistance;
    [SerializeField] float damage = -50;

    public ParticleSystem explosinFX;

    PlayerLife player;

    private void Start()
    {
        player = FindObjectOfType<PlayerLife>();
    }

    private void OnDisable()
    {
        if (explosinFX)
            explosinFX.Play();
    }

    void Update()
    {
        Debug.DrawRay(transform.position + Vector3.up, player.transform.position - transform.position, Color.red);

        if(Vector3.Distance(player.transform.position, transform.position + Vector3.up) < maxDistance)
        {
            player.ReceiveDamage(damage);

            gameObject.SetActive(false);
 
        }        
    }


}
