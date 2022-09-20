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
    
    //--moverlo--//
    private int MascaraSuelo;
    private float camRayLongitud = 1000f;

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

        Ray camaraRay = camforMouse.ScreenPointToRay(Input.mousePosition);
       
        RaycastHit hitSuelo;

        //Función Raycast devuelve true si el Ray detecta algo
        //Physics.Raycast(Ray que detectará o no, RaycastHit que informa, longitud del ray, mascara del suelo donde queremos detectar)    
        //HitSuelo es aquí out para almacenar información en la variable fuera de la función

        if (Physics.Raycast(camaraRay, out hitSuelo, camRayLongitud, MascaraSuelo))
        {
       
            Vector3 jugador_a_raton = hitSuelo.point - _transform.position;
            jugador_a_raton.y = 0f;

             Quaternion nuevaRotacion = Quaternion.LookRotation(jugador_a_raton);
            Debug.Log(nuevaRotacion.eulerAngles);
            _rb.MoveRotation(nuevaRotacion);

        }
    }
    public void Move(float x,float z,float speed,float smoothTime)
    {
        Vector3 moveDir = new Vector3(x,0,z).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * speed, ref smoothMoveVelocity, smoothTime);            
    }
}
