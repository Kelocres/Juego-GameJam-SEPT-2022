using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth;
    public int health;


    private void Start()
    {
        health = maxHealth;
    }

    void GetDamage(int damage)
    {
        if(health > 0)
        {
            health -= damage;
        }
        else
        {
            GameManager.instance.FillBar(maxHealth);
        }

    }
}
