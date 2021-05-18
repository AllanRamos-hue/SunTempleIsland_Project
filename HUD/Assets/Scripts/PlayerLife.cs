using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public float life;

    float currentLife;

    public Slider lifeBar;

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
            return true;
        
        return false;
       
    }

    public void ReceiveDamage(float damage)
    {
        currentLife += damage;

        currentLife = Mathf.Clamp(currentLife, 0, life);
    }
}
