using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnitScript : MonoBehaviour
{
    public string unitKind = "floor";   //Puede ser floor, wall o center
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnitCreation()
    {

    }

    public void UnitDestruction()
    {
        Debug.Log("UnitDestruction()");
        Destroy(gameObject);
    }
}
