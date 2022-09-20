using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public enum Combat {Meele, Distance, bomb }
    public Combat combatType = Combat.Meele;

    EnemyMove moveScript;
    [Header("Meele")]
    public float damage;

    [Header("Shooting")]
    EnemyShoot shootScript;
    bool isMoving;

    private void Start()
    {
        moveScript = GetComponent<EnemyMove>();

        if(combatType == Combat.Distance)
        {
            shootScript = GetComponent<EnemyShoot>();
        }
    }

    private void Update()
    {
        isMoving = moveScript.isMoving;

        if(!isMoving)
        {
            shootScript.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PLayer"))
        {
            //damage
        }
    }






}
