using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMediator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] MoveController movecontroller;
    [SerializeField] CameraController cameracontroller;
    [SerializeField] AnimationController animationcontroller;
    void Start()
    {
        
    }
    public void Configure(GameObject gobj)
    {
        movecontroller.Configure(gobj);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void CanLook(float mouseSensitivity)
    {
       // float getAxisMousex = Input.GetAxisRaw("Mouse X");
        movecontroller.Look(cameracontroller.CamforMouse);
    }
    public void CanMove(float sprintSpeed,float walkSpeed, float smoothTime,float mouseSensitivity )
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        if(x==0 && z == 0)
        {
            movecontroller._rigidbody.velocity = Vector3.zero;
        }
        movecontroller.Move(x, z, speed, smoothTime);
        animationcontroller.moveAnim();         
    }
}
