using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public enum Combat {Meele, Distance, bomb }
    public Combat combatType = Combat.Meele;

    public EnemyMove moveScript;
    [Header("Meele")]
    public float damage;

    [Header("Shooting")]
    public EnemyShoot shootScript;
    bool isMoving;

    private void Awake()
    {
        moveScript = GetComponent<EnemyMove>();

        if(combatType == Combat.Distance)
        {
            shootScript = GetComponent<EnemyShoot>();
        }
    }

    private void Update()
    {
        if(combatType == Combat.Distance)
        {
            isMoving = moveScript.isMoving;
            shootScript.enabled = !isMoving;       
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //damage
        }
    }






}
