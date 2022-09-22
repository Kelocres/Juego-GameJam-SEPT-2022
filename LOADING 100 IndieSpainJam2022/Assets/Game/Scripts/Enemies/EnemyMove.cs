using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : MonoBehaviour
{
    public enum MoveType {Towards,ZigZag}

    public MoveType moveType = MoveType.Towards;
    public float speed;
    Rigidbody rb;
    Transform player;
    [Header("ZigZag")]
    public float frequency = 10.0f; 
    public float magnitude = 1f; 
    Vector3 pos;
    Vector3 axis;
    [Header("Distance")]
    public bool keepDistance;
    public float distance;
    public bool isMoving = true;
    [Header("Avoid Obstacles")]  
    public float numberOfRays = 15;
    public float angle = 90;
    public float rayRange = 2;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        pos = transform.position;
        frequency += Random.Range(2, 5);
        speed += Random.Range(1, 3); ;
        if(GetComponent<EnemyAttack>().combatType == Combat.Distance)
        {
            distance += Random.Range(1, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        axis = transform.right;
        AvoidCol();
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
            rb.isKinematic = false;
            Move();
            isMoving = true;
        }
        else
        {
            rb.isKinematic = true;
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
        //transform.position = Vector3.MoveTowards(new Vector3(transform.position.x,height,transform.position.z)
        //    , new Vector3(player.position.x, height, player.position.z), speed * Time.deltaTime);
        rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
    }

    void MoveZigZag()
    {
        //pos += transform.forward * Time.deltaTime * speed;
        rb.MovePosition((pos += transform.forward * Time.deltaTime * speed) + axis * Mathf.Sin(Time.time * frequency) * magnitude);
    }

    void AvoidCol()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis(i / numberOfRays * angle * 2 - angle, transform.up);
            var direction = rotation * rotationMod * Vector3.forward;
            var ray = new Ray(transform.position, direction);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray,out hitInfo, rayRange))
            {
                pos -= 1/numberOfRays * direction;
            }

          
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis(i /numberOfRays * angle * 2 - angle, transform.up);
            var direction = rotation * rotationMod * Vector3.forward;
            Gizmos.DrawRay(transform.position, direction);
        }
    }





}
