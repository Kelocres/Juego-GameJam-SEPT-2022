using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    public GameObject bomb;
    public int damage;
    public float delay = 3;
    EnemyMove moveScript;
    public ParticleSystem explosion;

    private void OnEnable()
    {
        moveScript = GetComponent<EnemyMove>();
        StartCoroutine(Countdown());       
    }

    void Explode()
    {
        bomb.SetActive(true);
        explosion.Play();
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.5f);
        moveScript.enabled = false;
        yield return new WaitForSeconds(delay);
        Explode();    
        yield return new WaitForSeconds(0.5f);
        bomb.SetActive(false);
        yield return new WaitForSeconds(1f);
        moveScript.enabled = true;
        enabled = false;
    }

}
