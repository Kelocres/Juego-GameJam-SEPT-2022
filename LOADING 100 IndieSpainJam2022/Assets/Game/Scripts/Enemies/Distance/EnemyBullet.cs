using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float velocity = 5f;

    private void OnEnable()
    {
        StartCoroutine(lostBullet());
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * velocity;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RecycleBullet();
            //damage player
        }
        else
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
