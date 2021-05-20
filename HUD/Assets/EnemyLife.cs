using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public float life = 100;

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
