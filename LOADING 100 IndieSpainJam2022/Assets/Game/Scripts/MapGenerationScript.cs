using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationScript : MonoBehaviour
{
    private int[,] matrixMap;
    private Vector3 mapUnitMeasures;
    public GameObject mapUnit;

    //Initial matrix: 20x20
    public int mapHeight = 20;
    public int mapWidth = 20;

    //Center of the map, initial position of the player
    public int initialPositionX = 10;
    public int initialPositionY = 10;

    //In order to expand the map in an interesting way, we should find positions for new routes
    //These positions will be selected according of how many of their neighbour cells have unitMaps
    //To do this research, we will use Graph Research Algorithm
    private int[,] matrixResearch; //Values: 0-> not checked, 1-> Waiting for being checked, 2-> Checked
    private int[] arrayDesirableValues = { 1, 2, 3, 4, 5, 6, 7 };
    private int desiredValue;
    private List<MatrixPosition> desirablePositions = new List<MatrixPosition>();
    private MatrixPosition[] routeOrientations = { new MatrixPosition(1, 0), new MatrixPosition(-1, 0), new MatrixPosition(0, 1), new MatrixPosition(0, -1) };



    void Start()
    {
        // Initialize map matrix
        matrixMap = new int[mapHeight, mapWidth];
        Debug.Log("Default matrix value: " + matrixMap[initialPositionX, initialPositionY]);

        // Get measures from unitMapMeasures
        if (mapUnit != null)
        {
            mapUnitMeasures = new Vector3(8, 8, 8);
            Debug.Log("Measures: x=" + mapUnitMeasures.x + ", y=" + mapUnitMeasures.y + ", z=" + mapUnitMeasures.z);

            FirstMapUnits();
        }
    }

    private void FirstMapUnits()
    {
        for (int x = initialPositionX - 1; x <= initialPositionX + 1; x++)
            for (int y = initialPositionY - 1; y <= initialPositionY + 1; y++)
            {
                //matrixMap[x, y] = 1;
                CreateMapUnit(x, y);
            }
    }

    public void ExpandMap(int numUnits)
    {
        //Set origin position
        int desiredValue = arrayDesirableValues[Random.Range(0, arrayDesirableValues.Length)];
        MatrixPosition origin = SearchNewRouteOrigin(desiredValue);

        //Set orientation
        MatrixPosition direction = routeOrientations[0];

        //NOTA: con esta indicación, habrá una tendencia a crear suelos en dirección routeOrietations[0]
        // Se deberá modificar en el futuro para asegurar la variedad
        for (int i = 0; i < routeOrientations.Length; i++)
        {
            direction = routeOrientations[i];
            if (matrixMap[origin.x + direction.x, origin.y + direction.y] == 0)
                break;

        }

        //Begin the instantiation
        int unitsCreated = 0;
        while (unitsCreated < numUnits)
        {
            unitsCreated = 1;
            MatrixPosition newPos = new MatrixPosition(origin.x + direction.x * unitsCreated, origin.y + direction.y * unitsCreated);

            //NOTA: Esta verificación no la pongo porque no sé que hacer en caso de que ocurra esto
            // tal vez multiplicar unitsCreated por -1 y que continue en la otra dirección
            //if(newPos.x >= 0 && newPos.x < mapHeight && newPos.y >= 0 && newPos.y < mapWidth)

            if (matrixMap[newPos.x, newPos.y] == 0)
                CreateMapUnit(newPos.x, newPos.y);
            else
                numUnits++;
        }
    }

    public MatrixPosition SearchNewRouteOrigin(int desiredValue)
    {
        //Crear matriz para comprobar valores
        matrixResearch = new int[mapHeight, mapWidth];

        //Cola de comprobaciones
        Queue<MatrixPosition> queuePositions = new Queue<MatrixPosition>();

        //Poner root en "waiting for being checked" y encolar
        matrixResearch[initialPositionX, initialPositionY] = 1;
        queuePositions.Enqueue(new MatrixPosition(initialPositionX, initialPositionY));

        //Decidir valor deseado
        //desiredValue = arrayDesirableValues[Random.Range(0, arrayDesirableValues.Length)];
        desirablePositions = new List<MatrixPosition>();

        //Loop
        while (queuePositions.Count > 0)
        {
            //Sacar elemento de la cola
            MatrixPosition posActual = queuePositions.Peek();
            queuePositions.Dequeue();

            int countingNeighbours = 0;

            /*for (int x = posActual.x - 1; x <= posActual.x + 1; x++)
                for (int y = posActual.y - 1; y <= posActual.y + 1; y++)
                {
                    if(x>=0 && x < mapHeight && y >= 0 && y<mapWidth)
                        if(matrixMap[x,y] == 1)
                        {

                        }
                }*/
            for (int i = 0; i < routeOrientations.Length; i++)
            {
                MatrixPosition newPos = new MatrixPosition(posActual.x + routeOrientations[i].x, posActual.y + routeOrientations[i].y);
                if (newPos.x >= 0 && newPos.x < mapHeight && newPos.y >= 0 && newPos.y < mapWidth)
                    if (matrixMap[newPos.x, newPos.y] == 1)
                    {
                        countingNeighbours++;
                        if (matrixResearch[newPos.x, newPos.y] == 0)
                        {
                            matrixResearch[newPos.x, newPos.y] = 1;
                            queuePositions.Enqueue(newPos);
                        }
                    }
            }

            if (countingNeighbours == desiredValue)
                desirablePositions.Add(posActual);
        }

        if (desirablePositions.Count <= 0)
        {
            int newValue = desiredValue + 1;
            if (newValue >= arrayDesirableValues[arrayDesirableValues.Length - 1]) newValue = arrayDesirableValues[0];
            return SearchNewRouteOrigin(newValue);
        }
        else
            return desirablePositions[Random.Range(0, desirablePositions.Count)];
    }

    private void CreateMapUnit(int posX, int posY)
    {
        matrixMap[posX, posY] = 1;

        float x = (posX - initialPositionX) * mapUnitMeasures.x * 2;
        float z = (posY - initialPositionY) * mapUnitMeasures.z * 2;
        Instantiate(mapUnit, new Vector3(x, 0, z), Quaternion.Euler(new Vector3(-90, 0, 0)));
    }
}

public class MatrixPosition
{
    public int x;
    public int y;

    public MatrixPosition(int introX, int introY)
    {
        x = introX;
        y = introY;
    }
}
/*
public class NodeUnitMap
{
    public MatrixPosition position;
    public int freeNeighbours;
}*/
