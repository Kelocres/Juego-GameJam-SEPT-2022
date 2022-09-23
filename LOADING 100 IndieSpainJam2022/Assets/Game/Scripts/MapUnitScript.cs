using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnitScript : MonoBehaviour
{
    public string unitKind = "floor";   //Puede ser floor o center
    public GameObject[] walls;
    
    
    private bool parked;
    private bool received_heights = false;
    //private float initialHeight;
    //private float finalHeight;
    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private float desiredDuration = 1f;
    private float elapsedTime;

    private ParticleSystem destructionParticles;

    [SerializeField]
    private AnimationCurve curve;
    
    
    void Start()
    {

        parked = false;
        destructionParticles = GetComponentInChildren<ParticleSystem>();
        //received_heights = false;
        if (unitKind == "floor")
        {
            GetWalls();
            ShowOrHideWalls();
        }
    }

    public void SetHeightsForMovement(float _finalHeight)
    {
        received_heights = true;
        //if (received_heights) Debug.Log("MapUnitScript Update() " + unitKind + " receibed_heights = true");
        //finalHeight = _finalHeight;
        //initialHeight = transform.position.y;
        initialPosition = transform.position;
        finalPosition = new Vector3(transform.position.x, 
            _finalHeight, transform.position.z);
        
        Debug.Log("MapUnitScript SetHeightsForMovement() hecho");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(" ");
        if (unitKind == "floor")
        {
            //if (!parked) Debug.Log("MapUnitScript Update() " + unitKind + " parked = false");
            //if (received_heights) Debug.Log("MapUnitScript Update() " + unitKind + "receibed_heights = true");
            if (!parked && received_heights)
            {
                Debug.Log("MapUnitScript Ascensión");
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / desiredDuration;

                transform.position = Vector3.Lerp(initialPosition, finalPosition,
                    curve.Evaluate(percentageComplete));

                if (elapsedTime >= desiredDuration) parked = true;
            }
        }
    }

    public void GetWalls()
    {
        walls = new GameObject[4];
        int j = 0;
        for(int i=0; i<transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "firewall") walls[j++] = child;

        }
    }

    public void ShowOrHideWalls()
    {
        //Decidir un número aleatorio de paredes
        //Si es 0 o menor, no se crea ninguna
        int randAmount = Random.Range(0, 6);

        if (randAmount <= 0) return;

        //randAmount = 4 - randAmount;

        int randIndex = Random.Range(0, 4);

        while(randAmount-- >0)
        {
            if (randIndex >= walls.Length)
                randIndex = 0;

            walls[randIndex++].SetActive(false);            
        }
    }

    public void UnitCreation()
    {

    }

    public void UnitDestruction()
    {
        Debug.Log("UnitDestruction()");

        //Desenparejar partículas
        destructionParticles.transform.parent = null;
        destructionParticles.Play();

        Destroy(destructionParticles.gameObject, 3f);
        Destroy(gameObject);
    }
}
