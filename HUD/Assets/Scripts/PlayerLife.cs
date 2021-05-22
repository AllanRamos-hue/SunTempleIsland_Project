using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public float life;

    float currentLife;

    public Slider lifeBar;

    public AudioClip deathSFX;

    private void Start()
    {
        currentLife = life;
    }

    private void Update()
    {
        if (lifeBar)
            lifeBar.value = currentLife / life;
    }

    public bool Die()
    {
        if (currentLife <= 0)
        {
            AudioManager.PlaySFX(deathSFX);
            return true;
        }

        return false;
    }

    public void ReceiveDamage(float damage)
    {
        currentLife += damage;

        currentLife = Mathf.Clamp(currentLife, 0, life);
    }

    public void Revive()
    {
        currentLife = life;
    }
}
