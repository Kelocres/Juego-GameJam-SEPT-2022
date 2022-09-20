using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity= Vector3.zero;
    Rigidbody _rb;
    Transform _transform;
    public void Configure(GameObject gobj) {
        _rb = gobj.GetComponent<Rigidbody>();
        _transform = gobj.transform;
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    private void FixedUpdate()
    {
        
        _rb.MovePosition(_rb.position + _transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

    }
    public void Look( float mouseSensitivity, float getAxisMousex)
    {
         
        _transform.Rotate(Vector3.up * getAxisMousex * mouseSensitivity);
    }
    public void Move(float x,float z,float speed,float smoothTime)
    {
        Vector3 moveDir = new Vector3(x,0,z).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * speed, ref smoothMoveVelocity, smoothTime);
        
        
    }
}
