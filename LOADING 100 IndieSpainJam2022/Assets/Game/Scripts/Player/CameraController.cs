using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Camera camforMouse;
    [SerializeField]Transform objective;
    public float smoothing = 5f;

    Vector3 offset;

    public Camera CamforMouse { get => camforMouse; set => camforMouse = value; }

    void Start()
    {
        //offset = camforMouse.transform.position - objective.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Vector3 objetivoCamaraPos = objective.position + offset;
        //camforMouse.transform.position = Vector3.Lerp(camforMouse.transform.position, objetivoCamaraPos, smoothing * Time.deltaTime);
    }
}
