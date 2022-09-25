using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public GameObject deadEffect;
    public AudioClip enemyDieSound;

    private void Start()
    {
        health = maxHealth;
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if(health < 0)
        {
            SoundManager.instance.PlaySound(enemyDieSound);
            deadEffect.transform.parent = null;
            deadEffect.SetActive(true);
            GameManager.instance.FillBar(maxHealth);
            Destroy(gameObject);
        }

    }
}
