using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnitScript : MonoBehaviour
{
    public string unitKind = "floor";   //Puede ser floor, wall o center
    public GameObject[] walls;
    
    void Start()
    {
        GetWalls();
        ShowOrHideWalls();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Destroy(gameObject);
    }
}
