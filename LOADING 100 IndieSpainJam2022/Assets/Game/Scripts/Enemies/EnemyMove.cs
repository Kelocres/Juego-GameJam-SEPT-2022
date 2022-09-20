using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : MonoBehaviour
{
    public enum MoveType {Towards,ZigZag}

    public MoveType moveType = MoveType.Towards;
    public float speed;
    Transform player;
    [Header("Towards")]
    public float height;
    [Header("ZigZag")]
    public float frequency = 10.0f; 
    public float magnitude = 1f; 
    Vector3 pos;
    Vector3 axis;
    [Header("distance")]
    public bool keepDistance;
    public float distance;
    public bool isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player);
        pos = transform.position;
        axis = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        if(keepDistance)
        {
            KeepDistance();
        }
        else
        {
            Move();
        }
    }

    void KeepDistance()
    {
        if(Vector3.Distance(transform.position,player.position)>distance)
        {
            Move();
        }
        else
        {
            isMoving = false;
        }
    }

    void Move()
    {
        switch (moveType)
        {
            case MoveType.Towards:
                MoveToward();
                break;
            case MoveType.ZigZag:
                MoveZigZag();
                break;
            default:
                break;
        }
    }

    void MoveToward()
    {
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x,height,transform.position.z)
            , new Vector3(player.position.x, height, player.position.z), speed * Time.deltaTime);
    }

    void MoveZigZag()
    {
        pos += transform.forward * Time.deltaTime * speed;
        transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
    }


}
