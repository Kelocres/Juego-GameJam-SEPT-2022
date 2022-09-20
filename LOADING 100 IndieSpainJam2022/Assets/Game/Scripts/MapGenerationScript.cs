using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationScript : MonoBehaviour
{
    private int[,] mapMatrix;
    private Vector3 mapUnitMeasures;
    public GameObject mapUnit;

    //Initial matrix: 20x20
    public int mapHeight = 20;
    public int mapWidth = 20;

    //Center of the map, initial position of the player
    public int initialPositionX = 10;
    public int initialPositionY = 10;
    void Start()
    {
        // Initialize map matrix
        mapMatrix = new int[mapHeight,mapWidth];
        Debug.Log("Default matrix value: " + mapMatrix[initialPositionX, initialPositionY]);

        // Get measures from unitMapMeasures
        if(mapUnit != null)
        {
            mapUnitMeasures = new Vector3(8,8,8);
            Debug.Log("Measures: x=" + mapUnitMeasures.x + ", y=" + mapUnitMeasures.y + ", z=" + mapUnitMeasures.z);

            FirstMapUnits();
        }
    }

    private void FirstMapUnits()
    {
        for (int x = initialPositionX - 1; x <= initialPositionX + 1; x++)
            for (int y = initialPositionY - 1; y <= initialPositionY + 1; y++)
                CreateMapUnit(x, y);
    }

    private void CreateMapUnit(int posX, int posY)
    {
        float x = (posX - initialPositionX) * mapUnitMeasures.x * 2;
        float z = (posY - initialPositionY) * mapUnitMeasures.z * 2;
        Instantiate(mapUnit, new Vector3(x, 0, z), Quaternion.Euler(new Vector3(-90, 0, 0)));
    }
}
