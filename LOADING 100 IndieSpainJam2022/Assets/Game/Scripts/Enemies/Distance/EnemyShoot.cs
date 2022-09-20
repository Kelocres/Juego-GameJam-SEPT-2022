using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]
    int timeBtwShoots;
    private void OnEnable()
    {
        StartCoroutine(Shooting());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Shooting()
    {
        GetComponent<BulletPool>().ShootBullet();
        yield return new WaitForSeconds(timeBtwShoots);
        StartCoroutine(Shooting());
    }
}
