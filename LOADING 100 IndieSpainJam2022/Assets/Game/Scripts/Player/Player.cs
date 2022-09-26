using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static WeaponController;

public class Player : MonoBehaviour
{
    [SerializeField] private float  sprintSpeed, walkSpeed, smoothTime, mouseSensitivity;
    [SerializeField] private PlayerMediator mediator;

    


    public Animator anim;
    private void Awake()
    {
 
        
    }
    void Start()
    {
         
        mediator.Configure(gameObject.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        TryShoot(getMouseRotation());
       // if (Input.GetKey(KeyCode.Z))
         //   mediator.CanChangeProjectileType(ProjectileT.Especial);
        //if (Input.GetKey(KeyCode.X))
          //  mediator.CanChangeProjectileType(ProjectileT.Normal);
    }
    private void FixedUpdate()
    {
        CanMove();
        CanLook();
    }
    public void CanMove()
    {     
        mediator.CanMove(sprintSpeed, walkSpeed, smoothTime, mouseSensitivity);
    }
    public void CanLook()
    {
        mediator.CanLook(mouseSensitivity);
    }
    public void TryShoot(Quaternion rotation)
    {
        mediator.TryShoot(rotation);
    }
    public Quaternion getMouseRotation()
    {
        return mediator.getMouseRotation();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "collider_caida")
        {

            GameManager.instance.EmptyBar(5);
            transform.position = posRespawn.position;
        }
    }*/

}
