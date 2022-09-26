using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamageScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform posRespawn;
    void Start()
    {
        if (posRespawn == null)
            posRespawn = GameObject.FindGameObjectWithTag("position_respawn").transform;

    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collision");
            GameManager.instance.EmptyBar(5);
            other.transform.position = posRespawn.position;
        }
    }
}
