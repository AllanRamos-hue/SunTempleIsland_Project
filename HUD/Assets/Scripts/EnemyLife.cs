using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public float life = 100;

    public AudioSource enemySound;

    public AudioClip[] enemyDamagesSFX;

    public void TakeDamage(float damage)
    {
        life += damage;

        for (int i = 0; i < enemyDamagesSFX.Length; i++)
        {
            AudioManager.PlaySFXSource(enemySound, enemyDamagesSFX[i], 1f);
        }

        Die();
    }

    void Die()
    {
        if (life <= 0)
        {
            gameObject.SetActive(false);
        }

    }
}
