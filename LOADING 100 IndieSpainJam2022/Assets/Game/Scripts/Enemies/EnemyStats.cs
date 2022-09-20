using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    private void Start()
    {
        health = maxHealth;
    }
}
