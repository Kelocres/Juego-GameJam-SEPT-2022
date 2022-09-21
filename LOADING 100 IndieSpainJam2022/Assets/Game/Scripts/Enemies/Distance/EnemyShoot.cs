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
        yield return new WaitForSeconds(0.5f);
        GetComponent<BulletPool>().ShootBullet();
        yield return new WaitForSeconds(timeBtwShoots);
        StartCoroutine(Shooting());
    }
}
