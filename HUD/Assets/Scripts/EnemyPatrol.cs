using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
   
    public Transform[] spots;
    public float minDistance = 5;
    public float attackDamage = -30;

    int index;

    NavMeshAgent agent;
    Animator anim;

    GameObject target;

    bool attacking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        anim.SetBool("Walk", false);

        target = GameObject.Find("Player");

        index = 0;
        agent.SetDestination(spots[index].transform.position);

    }

    void Update()
    {
        Patrol();

        if(SpotPlayer())
        {
            Chasing();
        }   
    }

    void Chasing()
    {
        Vector3 targetPos = target.transform.position;

        anim.SetBool("Walk", true);
        
        agent.SetDestination(targetPos);

        if (Vector3.Distance(transform.position, targetPos) < 2)
        {
            StartCoroutine(Attack());
        }
    }

    void Patrol()
    {
        anim.SetBool("Walk", true);

        if (agent.remainingDistance < 0.5f)
        {
            index++;

            if (index >= spots.Length)
            {
                index = 0;

            }

            agent.SetDestination(spots[index].transform.position);
        }
    }

    IEnumerator Attack()
    {
        if (attacking) yield break;

        attacking = true;

        PlayerLife player = target.GetComponent<PlayerLife>();

        player.ReceiveDamage(attackDamage);

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

            if(Physics.Raycast(ray, out hit, 5))
                if(hit.transform == target.transform)
                {
                    return true;
                } 
        }

        return false;
    }
}
