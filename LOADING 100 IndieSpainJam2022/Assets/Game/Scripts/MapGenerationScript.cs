using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationScript : MonoBehaviour
{
    private int[,] matrixMap;
    public float mapUnitMeasures = 8f;
    public Vector3 mapUnitRotation = new Vector3(-90, 0, 0);
    public GameObject mapUnit;
    public GameObject mapCenter;

    //Initial matrix: 20x20
    public int mapHeight = 100;
    public int mapWidth = 100;

    //Center of the map, initial position of the player
    public int initialPositionX = 50;
    public int initialPositionY = 50;

    //In order to expand the map in an interesting way, we should find positions for new routes
    //These positions will be selected according of how many of their neighbour cells have unitMaps
    //To do this research, we will use Graph Research Algorithm
    private int[,] matrixResearch; //Values: 0-> not checked, 1-> Waiting for being checked, 2-> Checked
    private int[] arrayDesirableValues = { 1, 2, 3, 4, 5, 6, 7 };
    private int desiredValue;
    private List<MatrixPosition> desirablePositions = new List<MatrixPosition>();
    private MatrixPosition[] routeOrientations = { new MatrixPosition(1, 0), new MatrixPosition(-1, 0), new MatrixPosition(0, 1), new MatrixPosition(0, -1) };

    //Para que la corutina ExpandMap se mantenga latente 
    private MatrixPosition origin;




    void Start()
    {
        // Initialize map matrix
        matrixMap = new int[mapHeight, mapWidth];
        Debug.Log("Default matrix value: " + matrixMap[initialPositionX, initialPositionY]);

        // Get measures from unitMapMeasures
        if (mapCenter != null)
        {
            
            //Debug.Log("Measures: =" + mapUnitMeasures + ", y=" + mapUnitMeasures + ", z=" + mapUnitMeasures);

            FirstMapUnits();
        }
    }

    private void FirstMapUnits()
    {

        for (int x = initialPositionX - 1; x <= initialPositionX + 1; x++)
            for (int y = initialPositionY - 1; y <= initialPositionY + 1; y++)
            {
                matrixMap[x, y] = 1;
                //CreateMapUnit(x, y);
            }
        Instantiate(mapCenter, new Vector3(initialPositionX - 1, 0, initialPositionY - 1), Quaternion.Euler(mapUnitRotation));

    }

    public void StartExpandMap(int numUnits)
    {
        int desiredValue = arrayDesirableValues[Random.Range(0, arrayDesirableValues.Length)];
        StartCoroutine(SearchNewRouteOrigin(desiredValue));
        StartCoroutine(WaitingForOrigin(numUnits));
    }

    private IEnumerator WaitingForOrigin(int numUnits)
    {
        while (origin == null)
            yield return null;

        StartCoroutine(ExpandMap(numUnits));
    }

    private IEnumerator ExpandMap(int numUnits)
    {
        Debug.Log("ExpandMap() Posicion origin x=" + origin.x + ", y=" + origin.y);
        //Set origin position
        //int desiredValue = arrayDesirableValues[Random.Range(0, arrayDesirableValues.Length)];
        //MatrixPosition origin = SearchNewRouteOrigin(desiredValue);

        //Set orientation


        //NOTA: con esta indicaci�n, habr� una tendencia a crear suelos en direcci�n routeOrietations[0]
        // Se deber� modificar en el futuro para asegurar la variedad
        Debug.Log("ExpandMap() For loop para identificar la orientaci�n");
        /*for (int i = 0; i < routeOrientations.Length; i++)
        {
            direction = routeOrientations[i];
            if (matrixMap[origin.x + direction.x, origin.y + direction.y] == 0)
                break;
            else
                yield return null;

        }*/
        int i = Random.Range(0, routeOrientations.Length);
        MatrixPosition direction = routeOrientations[i];
        while (matrixMap[origin.x + direction.x, origin.y + direction.y] != 0)
        {
            i++;
            if (i >= routeOrientations.Length)
                i = 0;

            yield return null;
        }


        //Begin the instantiation
        int unitsCreated = 0;
        Debug.Log("ExpandMap() While loop para crear las unidades de suelo");
        while (unitsCreated < numUnits)
        {
            unitsCreated++;
            MatrixPosition newPos = new MatrixPosition(origin.x + direction.x * unitsCreated, origin.y + direction.y * unitsCreated);
            Debug.Log("ExpandMap() While(" + unitsCreated + " > " + numUnits + ") loop con newPos x=" + newPos.x + ", y=" + newPos.y);
            //NOTA: Esta verificaci�n no la pongo porque no s� que hacer en caso de que ocurra esto
            // tal vez multiplicar unitsCreated por -1 y que continue en la otra direcci�n
            if (newPos.x < 0 || newPos.x >= mapHeight || newPos.y < 0 || newPos.y >= mapWidth)
            {
                Debug.Log("ExpandMap() if (" + newPos.x + " < 0 || " + newPos.x + " >= " + mapHeight + " || " + newPos.y + " < 0 || " + newPos.y + " >= " + mapWidth + ")");
                break;
            }

            if (matrixMap[newPos.x, newPos.y] == 0)
            {
                Debug.Log("ExpandMap() NewPos x=" + newPos.x + ", y=" + newPos.y + " est� vac�o y se va a construir aqu�");
                CreateMapUnit(newPos.x, newPos.y);
                yield return null;
            }
            else
            {
                numUnits++;
                //unitsCreated++;
                Debug.Log("ExpandMap() NewPos x=" + newPos.x + ", y=" + newPos.y + " aumentar numUnits a " + numUnits);
                yield return null;
            }
        }
    }

    public IEnumerator SearchNewRouteOrigin(int desiredValue)
    {
        Debug.Log("SearchNewRouteOrigin() con desiredValue = " + desiredValue);
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
        Debug.Log("SearchNewRouteOrigin() While loop para registrar posicion");
        while (queuePositions.Count > 0)
        {
            //Sacar elemento de la cola
            MatrixPosition posActual = queuePositions.Peek();
            queuePositions.Dequeue();
            Debug.Log("SearchNewRouteOrigin() While loop, posicion x=" + posActual.x + ", y=" + posActual.y);

            int countingNeighbours = 0;

            /*for (int x = posActual.x - 1; x <= posActual.x + 1; x++)
                for (int y = posActual.y - 1; y <= posActual.y + 1; y++)
                {
                    if(x>=0 && x < mapHeight && y >= 0 && y<mapWidth)
                        if(matrixMap[x,y] == 1)
                        {

                        }
                }*/
            Debug.Log("SearchNewRouteOrigin() For loop para registrar vecinos de la posicion");
            for (int i = 0; i < routeOrientations.Length; i++)
            {
                MatrixPosition newPos = new MatrixPosition(posActual.x + routeOrientations[i].x, posActual.y + routeOrientations[i].y);
                Debug.Log("SearchNewRouteOrigin() While loop, vecino x=" + newPos.x + ", y=" + newPos.y);
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

                yield return null;
            }

            if (countingNeighbours == desiredValue)
                desirablePositions.Add(posActual);

            yield return null;
        }

        if (desirablePositions.Count <= 0)
        {
            int newValue = desiredValue + 1;
            if (newValue >= arrayDesirableValues[arrayDesirableValues.Length - 1]) newValue = arrayDesirableValues[0];
            {
                Debug.Log("Reintentar SearchNewRouteOrigin() con desiredValue = " + newValue);
                yield return SearchNewRouteOrigin(newValue);
            }
        }
        else
        {
            MatrixPosition resultPosition = desirablePositions[Random.Range(0, desirablePositions.Count)];
            Debug.Log("SearchNewRouteOrigin() devolver posicion x=" + resultPosition.x + ", y=" + resultPosition.y);
            origin = resultPosition;
            //yield return resultPosition;
        }
    }

    private void CreateMapUnit(int posX, int posY)
    {
        Debug.Log("CreateMapUnit() crear en posX=" + posX + ", posY=" + posY);
        matrixMap[posX, posY] = 1;

        float x = (posX - initialPositionX) * mapUnitMeasures * 2;
        float z = (posY - initialPositionY) * mapUnitMeasures * 2;
        Instantiate(mapUnit, new Vector3(x, 0, z), Quaternion.Euler(mapUnitRotation));
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
