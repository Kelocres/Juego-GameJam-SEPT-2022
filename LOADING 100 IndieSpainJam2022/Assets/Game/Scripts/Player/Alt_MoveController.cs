using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alt_MoveController : MonoBehaviour
{

    private Vector3 inputMovement;
    public float velocity = 5f;
    private Animator animator;

    public Camera camera;
    CharacterController controlador;

    float getInputX() { return Input.GetAxis("Horizontal"); }
    float getInputY() { return Input.GetAxis("Vertical"); }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //camera = Camera.main;

        controlador = GetComponent<CharacterController>();
    }

    void Update()
    {
        inputMovement = new Vector3(getInputX(), 0, getInputY());
        SnapAlignCharacterWithCamera();

        /*if (inputMovement.x == 0 && inputMovement.z == 0)
            animator.SetBool("isWalking", false);
        else animator.SetBool("isWalking", true);*/

        if (inputMovement.x != 0 && inputMovement.z != 0)
            inputMovement = new Vector3(0, 0, Mathf.Abs(inputMovement.x * inputMovement.z));
        else if (inputMovement.x == 0)
            inputMovement = new Vector3(0, 0, Mathf.Abs(inputMovement.z));
        else if (inputMovement.z == 0)
            inputMovement = new Vector3(0, 0, Mathf.Abs(inputMovement.x));

        inputMovement.Normalize();

        //cambiar velocidad a "ir adelante"
        Vector3 velocidadActual = transform.TransformDirection(inputMovement);
        velocidadActual = new Vector3(velocidadActual.x * velocity, 0f, velocidadActual.z * velocity);

        //Adició del moviment vertical
        //velocidadActual.y += Physics.gravity.y; //Cau molt ràpid
        velocidadActual.y += Physics.gravity.y * 0.7f;


        velocidadActual = velocidadActual * Time.smoothDeltaTime * 4;

        controlador.Move(velocidadActual);
    }

    private void SnapAlignCharacterWithCamera()
    {

        if (inputMovement.z != 0 || inputMovement.x != 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                                    camera.transform.eulerAngles.y,
                                    transform.eulerAngles.z);

            float rot = 0;
            //z = delante, x= a los lados

            // si se va en la dirección opuesta de la actual, 
            //rotar inmediatamente; si no, rotación gradual    

            //si se va hacia atrás, rotar 180
            if (inputMovement.z < 0) rot = 180;

            //si se va hacia los lados, rotar 90 según el vector horizontal
            if (inputMovement.z == 0)
                rot += (inputMovement.x / Mathf.Abs(inputMovement.x)) * 90f;
            //en caso contrario, se asumirán horizontal y vertical
            else
                rot += (Mathf.Atan(inputMovement.x / inputMovement.z)) * 45f;

            transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                                                    transform.eulerAngles.y + rot,
                                                    transform.eulerAngles.z);

        }
    }

    // Update is called once per frame
    
}
