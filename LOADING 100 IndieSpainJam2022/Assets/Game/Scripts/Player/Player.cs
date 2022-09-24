using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    
}
