using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float life = 100;
    public float minDistance = 5;

    NavMeshAgent agent;
    Animator anim;

    GameObject target;

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

        anim.SetBool("Walk", true);

        agent.SetDestination(targetPos);
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

    public void TakeDamage(float damage)
    {
        life += damage;

        Die();

        Debug.Log(life);
    }

    void Die()
    {
        if (life <= 0)
        {
            gameObject.SetActive(false);
        }

    }
}
