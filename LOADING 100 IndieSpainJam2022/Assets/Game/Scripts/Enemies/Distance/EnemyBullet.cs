using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float velocity = 5f;   
    public int damage;

    private void OnEnable()
    {
        StartCoroutine(lostBullet());
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * velocity;
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        RecycleBullet();
    //        GameManager.instance.EmptyBar(damage);
    //        GameManager.instance.UpdateLoading();
    //    }
    //    else
    //    {
    //        RecycleBullet();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RecycleBullet();
            GameManager.instance.EmptyBar(damage);
        }
        else if (other.CompareTag("firewall"))
        {
            RecycleBullet();
        }
    }

    IEnumerator lostBullet()
    {
        yield return new WaitForSeconds(5f);
        RecycleBullet();
    }
    void RecycleBullet()
    {
        gameObject.SetActive(false);
    }

}
