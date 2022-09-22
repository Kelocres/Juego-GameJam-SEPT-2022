using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private float acceleration=2.0f;
    private float velocityZ;
    private float velocityX;
    void Start()
    {
        velocityZ = 0;
        velocityX = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveAnim()
    {
        bool isforwP = InputController.Instance.IsForwardPress();
        bool isbackP = InputController.Instance.IsBacktPress();
        bool isleftP=InputController.Instance.IsLeftPress();
        bool isrightP = InputController.Instance.IsRighttPress();
        bool isrunP=InputController.Instance.IsRunPress(); 

        if (isforwP && velocityZ<0.5f && !isrunP)
            velocityZ += Time.deltaTime * acceleration;

        if (isbackP && velocityZ < 0.5f && !isrunP)
            velocityZ += Time.deltaTime * acceleration;

        if (isleftP && velocityX < -0.5f && !isrunP)
            velocityX += Time.deltaTime * acceleration;

        if (isleftP && velocityX < 0.5f && !isrunP)
            velocityX += Time.deltaTime * acceleration;

        animator.SetFloat("MovX", velocityX);
        animator.SetFloat("MovZ", velocityZ);
    }
    public void clearAnim()
    {
        animator.SetFloat("MovX", 0);
        animator.SetFloat("MovY", 0);
    }
}
