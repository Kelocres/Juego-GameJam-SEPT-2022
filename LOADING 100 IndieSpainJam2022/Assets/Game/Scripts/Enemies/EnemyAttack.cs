using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Combat {Meele, Distance, Area }

public class EnemyAttack : MonoBehaviour
{
    public Combat combatType = Combat.Meele;

    public EnemyMove moveScript;
    [Header("Meele")]
    public int damage;

    [Header("Shooting")]
    public EnemyShoot shootScript;
    public bool isMoving;

    [Header("Area")]
    public AreaAttack areaScript;

    private void Awake()
    {
        moveScript = GetComponent<EnemyMove>();

        if(combatType == Combat.Distance)
        {
            shootScript = GetComponent<EnemyShoot>();
        }
        else if(combatType == Combat.Area)
        {
            areaScript = GetComponent<AreaAttack>();
        }
    }

    private void Update()
    {
        if(combatType == Combat.Distance)
        {
            isMoving = moveScript.isMoving;
            shootScript.enabled = !isMoving;       
        }
        else if(combatType == Combat.Area)
        {
            isMoving = moveScript.isMoving;
            areaScript.enabled = !isMoving;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            GameManager.instance.EmptyBar(damage);
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<EnemyStats>().colision();
            //if(combatType == Combat.Meele)
            //{
            //    Destroy(gameObject);
            //}
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
            
    //        GameManager.instance.FillBar(damage);
    //        GameManager.instance.UpdateLoading();
    //        Destroy(gameObject);
    //        //if(combatType == Combat.Meele)
    //        //{
    //        //    Destroy(gameObject);
    //        //}
    //    }
    //}








}
