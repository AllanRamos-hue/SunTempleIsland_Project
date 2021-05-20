using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public float life = 100;
    public float minDistance = 5;
    public Transform[] spots;

    int index;
    int i;

    NavMeshAgent agent;
    Animator anim;

    GameObject target; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        anim.SetBool("Walk", false);

        target = GameObject.Find("Player");

        index = 0;
        i = 1;
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

    public void TakeDamage(float damage)
    {
        life += damage;

        Die();

        Debug.Log("Dano");
    }

    void Die()
    {
        if (life <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
