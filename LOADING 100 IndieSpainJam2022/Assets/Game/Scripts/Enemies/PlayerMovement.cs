using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    GameObject sword;
    [SerializeField]
    GameObject shield;

    [Header("Movement")]
    public float speed = 7f;
    [SerializeField]
    bool isShielding = false;

    [Header("Stats")]
    [SerializeField]
    public float maxHealth = 10;
    [SerializeField]
    public float health;
    [SerializeField]
    AudioSource audioS;
    [SerializeField]
    AudioClip damageSound;


    private void Start()
    {
        health = maxHealth;
    }
    void Update()
    {
        MovePlayer();
        //Fall();
    }


    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(x, 0f, z).normalized * speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.World);
        if (playerMovement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerMovement.normalized), 0.2f);
        }
    }


    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(0.3f);
        sword.SetActive(false);
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        audioS.PlayOneShot(damageSound);
        if(health <= 0)
        {
            GameOver();
        }
    }

    void Fall()
    {
        if(transform.position.y <= -4)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
