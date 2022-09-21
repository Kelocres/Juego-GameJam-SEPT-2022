using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [Header("Pool")]  
    int bulletPoolSize = 10;
    [SerializeField]
    GameObject proyectile;
    [SerializeField]
    int shootNumber = 0;
    [SerializeField]
    GameObject[] bullets;
    [SerializeField]
    Transform canon;

    void Awake()
    {
        bullets = new GameObject[bulletPoolSize];

        for (int i = 0; i < bulletPoolSize; i++)
        {
            bullets[i] = Instantiate(proyectile, canon.position, Quaternion.identity);
            bullets[i].SetActive(false);
        }
    }
    private void OnDestroy()
    {
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }
    public void ShootBullet()
    {
        shootNumber++;
        if (shootNumber >= bullets.Length)
        {
            shootNumber = 0;
        }
        bullets[shootNumber].transform.position = canon.position;
        bullets[shootNumber].transform.rotation = gameObject.transform.rotation;
        bullets[shootNumber].SetActive(true);
    }
}
