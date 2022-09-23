using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumoFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }
}
