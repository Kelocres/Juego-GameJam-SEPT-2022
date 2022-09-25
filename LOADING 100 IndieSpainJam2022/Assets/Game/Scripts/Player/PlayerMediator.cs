using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponController;

public class PlayerMediator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] MoveController movecontroller;
    [SerializeField] CameraController cameracontroller;
    [SerializeField] AnimationController animationcontroller;
    [SerializeField] WeaponController weaponcontroller;
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
    public void TryShoot(Quaternion rotation)
    {
        if (InputController.Instance.IsFirePress())
        {
            
            weaponcontroller.TryShoot(rotation);
        }
           
    }
    public void CanChangeProjectileType(ProjectileT projectileT)
    {
              
            weaponcontroller.ChangeProjectileType(projectileT);
            
       
      
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
        
        movecontroller.Move(x, z, speed, smoothTime);
        animationcontroller.moveAnim();         
    }

    public Quaternion getMouseRotation()
    {
        return movecontroller.NewRotation;
    }
}
