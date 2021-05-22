using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float minDistance = 5;
    public float attackDamage = -30;

    public AudioClip attackSFX;

    NavMeshAgent agent;
    Animator anim;

    GameObject target;

    bool attacking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        target = GameObject.Find("Player");
    }

    void Update()
    {
        if (SpotPlayer())
        {
            Chasing();
        }
    }

    void Chasing()
    {
        Vector3 targetPos = target.transform.position;

        agent.SetDestination(targetPos);

        anim.SetBool("Walk", true);

        if (Vector3.Distance(transform.position, targetPos) < 2)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        if (attacking) yield break;

        attacking = true;

        PlayerLife player = target.GetComponent<PlayerLife>();
        
        player.ReceiveDamage(attackDamage);

        AudioManager.PlaySFX(attackSFX);

        yield return new WaitForSeconds(1);

        attacking = false;
    }

    bool SpotPlayer()
    {
        Vector3 targetPos = target.transform.position;

        if (Vector3.Distance(transform.position, targetPos) < minDistance)
        {
            Ray ray = new Ray(transform.position, targetPos - transform.position);

            Debug.DrawRay(transform.position + Vector3.up, targetPos - transform.position, Color.green);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5))
                if (hit.transform == target.transform)
                {
                    return true;
                }
        }

        anim.SetBool("Walk", false);

        return false;
    }

    
}
