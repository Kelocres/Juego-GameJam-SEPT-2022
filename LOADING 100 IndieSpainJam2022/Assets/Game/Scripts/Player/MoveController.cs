using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 moveAmount, moveDir;
    private Vector3 smoothMoveVelocity = Vector3.zero;
    private GameObject _gameObject;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Quaternion newRotation;
    RaycastHit hitFloor;
    //--moverlo--//
    private int MascaraSuelo;
    private float camRayLongitud = 1000f;
    private Vector3 posCamara;
    /// <summary>
    /// 
    /// </summary>
    bool isSprinting;
    public bool IsSprinting { get => isSprinting; set => isSprinting = value; }
    public Quaternion NewRotation { get => newRotation; set => newRotation = value; }

    private void Start()
    {
        MascaraSuelo = LayerMask.GetMask("Floor");
        IsSprinting = false;
    }
    public void Configure(GameObject gobj) {
        _gameObject=gobj;
        _rigidbody = gobj.GetComponent<Rigidbody>();
        _transform = gobj.transform;
       
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    private void FixedUpdate()
    {
         
        //_rigidbody.MovePosition(_rigidbody.position + _transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
  

    }
    public void Look(Camera camforMouse)
    {
        Ray camRay = camforMouse.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(camforMouse);
        
        if (Physics.Raycast(camRay, out hitFloor, camRayLongitud, MascaraSuelo))
        {
           // Debug.Log(Input.mousePosition);
       
            Vector3 playerPointToMouse = hitFloor.point - _transform.position;
            
            playerPointToMouse.y = 0f;
            
             NewRotation = Quaternion.LookRotation(playerPointToMouse);
            
            _rigidbody.MoveRotation(NewRotation);

        }
        posCamara = camforMouse.transform.position;
        
    }
    public void Move(float x,float z,float speed,float smoothTime)
    {
        if (x == 0 && z == 0)
        {
            _rigidbody.velocity = Vector3.zero;
        }
        //moveDir.Set(x, 0, z);
        Vector3 moveDir = new Vector3(x,0,z).normalized;
        //moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * speed, ref smoothMoveVelocity, smoothTime);
        //_rigidbody.MovePosition(_rigidbody.position + _transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);       
        moveAmount = moveDir.normalized * speed * Time.deltaTime;
        _rigidbody.MovePosition(_transform.position + moveAmount);
    }



    void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawSphere(hitFloor.point, 1);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(hitFloor.point,1);
    }
}
