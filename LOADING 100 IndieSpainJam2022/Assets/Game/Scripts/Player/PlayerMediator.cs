using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static WeaponController;

public class PlayerMediator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] MoveController movecontroller;
    [SerializeField] CameraController cameracontroller;
    [SerializeField] AnimationController animationcontroller;
    [SerializeField] WeaponController weaponcontroller;
    [SerializeField] UiController uicontroller;
    //---///
   

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
        float speed = movecontroller.IsSprinting ? sprintSpeed : walkSpeed;
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        
        movecontroller.Move(x, z, speed, smoothTime);
        animationcontroller.moveAnim();         
    }
    //esto no se deberia hacer aca por tiempo lo pongo aca deberia crear otro mediator que se encargue del ui y se comunique con los controladores de ui y entre los mediators se comuniquen :(
    public bool canfillBarActivatePower(Image bar_power )
    {             
        return uicontroller.fillBarActivatePower(bar_power);//
    }
    public void resetBarR()
    {
        uicontroller.EnergyForSpeed=0;
    }
    public Quaternion getMouseRotation()
    {
        return movecontroller.NewRotation;
    }
    public void trychangeMoveT()
    {
        movecontroller.IsSprinting = !movecontroller.IsSprinting;
    }
}
