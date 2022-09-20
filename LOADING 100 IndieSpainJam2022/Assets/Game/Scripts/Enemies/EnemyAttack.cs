using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public enum Combat {Meele, Distance, bomb }
    public Combat combatType = Combat.Meele;

    EnemyMove moveScript;

    public float damage;

    public GameObject bullet;

    private void Start()
    {
        moveScript = GetComponent<EnemyMove>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PLayer"))
        {
            //damage
        }
    }


    void Shoot()
    {
        Instantiate(bullet,transform.position,Quaternion.identity);
    }






}
