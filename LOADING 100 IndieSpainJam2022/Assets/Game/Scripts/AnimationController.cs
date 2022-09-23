using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private float acceleration=2.0f;
    private float deceleration = 2.0f;
    private float maxWV = 0.5f;
    private float maxRV=2.0f;
    private float velocityZ;
    private float velocityX;
    private bool isforwP, isbackP, isleftP, isrightP, isrunP;
    void Start()
    {
        velocityZ = 0;//for,back
        velocityX = 0;//iz,dere
    }

    // Update is called once per frame
    void Update()
    {
            isforwP = InputController.Instance.IsForwardPress();
            isbackP = InputController.Instance.IsBacktPress();
            isleftP = InputController.Instance.IsLeftPress();
            isrightP = InputController.Instance.IsRighttPress();
            isrunP = InputController.Instance.IsRunPress();
    }
  
    public void moveAnim()
    {   
        float currenMaxtV = isrunP ? maxRV : maxWV;
        ChangeVelocity(isforwP, isbackP, isleftP, isrightP, currenMaxtV);

        //if (!isforwP && velocityZ < 0.0f && !isrunP)
        //  velocityZ = 0;

        //lookingforw
        if (isforwP && isrunP && velocityZ > currenMaxtV)
            velocityZ = currenMaxtV;

        else if (isforwP && velocityZ > currenMaxtV)
        {

            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currenMaxtV && velocityZ < (currenMaxtV + 0.05f))
                velocityZ = currenMaxtV;
        }
        else if (isforwP && velocityZ < currenMaxtV && velocityZ > (currenMaxtV - 0.05f))
            velocityZ = currenMaxtV;

        //--lookingleft
        if (isleftP && isrunP && velocityX < -currenMaxtV)
            velocityX = -currenMaxtV;

        else if (isleftP && velocityX < -currenMaxtV)
        {

            velocityX += Time.deltaTime * deceleration;
            if (velocityX < -currenMaxtV && velocityX > (-currenMaxtV - 0.05f))
                velocityX = -currenMaxtV;
        }
        else if (isleftP && velocityX > -currenMaxtV && velocityX < (-currenMaxtV + 0.05f))
            velocityX = -currenMaxtV;
        // looking-ri
        if (isrightP && isrunP && velocityX > currenMaxtV)
            velocityX = currenMaxtV;

        else if (isrightP && velocityX > currenMaxtV)
        {

            velocityX -= Time.deltaTime * deceleration;
            if (velocityX > currenMaxtV && velocityX < (currenMaxtV + 0.05f))
                velocityX = currenMaxtV;
        }
        else if (isrightP && velocityX < currenMaxtV && velocityX > (currenMaxtV - 0.05f))
            velocityX = currenMaxtV;
        //looking-back
        if (isbackP && isrunP && velocityZ < -currenMaxtV)
            velocityZ = -currenMaxtV;

        else if (isbackP && velocityZ < -currenMaxtV)
        {

            velocityZ += Time.deltaTime * deceleration;
            if (velocityZ < -currenMaxtV && velocityZ > (-currenMaxtV - 0.05f))
                velocityZ = -currenMaxtV;
        }
        else if (isbackP && velocityZ > -currenMaxtV && velocityZ < (-currenMaxtV + 0.05f))
            velocityZ = -currenMaxtV;

        animator.SetFloat("MovX", velocityX);
        animator.SetFloat("MovZ", velocityZ);
    }

    private void ChangeVelocity(bool isforwP, bool isbackP, bool isleftP, bool isrightP, float currenMaxtV)
    {
        if (isforwP && velocityZ < currenMaxtV)
            velocityZ += Time.deltaTime * acceleration;

        if (isbackP && velocityZ > -currenMaxtV)
            velocityZ -= Time.deltaTime * acceleration;
            
       
           

        if (isleftP && velocityX > -currenMaxtV)
            velocityX -= Time.deltaTime * acceleration;

        if (isrightP && velocityX < currenMaxtV)
            velocityX += Time.deltaTime * acceleration;

        if (!isforwP && velocityZ > 0.0f)
            velocityZ -= Time.deltaTime * deceleration;

        if (!isbackP && velocityZ < 0.0f)
            velocityZ += Time.deltaTime * deceleration;

        if (!isleftP && velocityX < 0.0f)
            velocityX += Time.deltaTime * deceleration;

        if (!isrightP && velocityX > 0.0f)
            velocityX -= Time.deltaTime * deceleration;
        
        if (!isforwP && velocityZ > 0.0f )
            velocityZ = 0;
        if (!isleftP && velocityX < 0.0f)
            velocityX = 0;
        /*if (!isrightP && velocityZ > 0.0f)
            velocityZ = 0;
        
        if (!isbackP && velocityX < 0.0f)
            velocityX = 0;*/
    }
}
