using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity = Vector3.zero;
    private Rigidbody _rb;
    private Transform _transform;
    RaycastHit hitFloor;
    //--moverlo--//
    private int MascaraSuelo;
    private float camRayLongitud = 1000f;
    private Vector3 posCamara;

    public void Configure(GameObject gobj) {
        _rb = gobj.GetComponent<Rigidbody>();
        _transform = gobj.transform;
        MascaraSuelo = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);


    }
    public void Look(Camera camforMouse,float mouseSensitivity, float getAxisMousex)
    {
        Debug.Log(Input.mousePosition);
        Ray camRay = camforMouse.ScreenPointToRay(Input.mousePosition);
       
        //RaycastHit hitFloor;

        
        if (Physics.Raycast(camRay, out hitFloor, camRayLongitud, MascaraSuelo))
        {
       
            Vector3 playerPointToMouse = hitFloor.point - _transform.position;
            
            playerPointToMouse.y = 0f;
            
            Quaternion nuevaRotacion = Quaternion.LookRotation(playerPointToMouse);
            
            _rb.MoveRotation(nuevaRotacion);

        }
        posCamara = camforMouse.transform.position;
        
    }
    public void Move(float x,float z,float speed,float smoothTime)
    {
        Vector3 moveDir = new Vector3(x,0,z).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * speed, ref smoothMoveVelocity, smoothTime);
    }
    void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawSphere(hitFloor.point, 1);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(posCamara, hitFloor.point);
    }
}
