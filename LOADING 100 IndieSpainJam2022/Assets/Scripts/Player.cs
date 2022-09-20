using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float  sprintSpeed, walkSpeed, smoothTime, mouseSensitivity;
    [SerializeField] private PlayerMediator mediator;
    
    void Start()
    {
        var gobj = gameObject.gameObject;
        mediator.Configure(gobj);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
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
}
