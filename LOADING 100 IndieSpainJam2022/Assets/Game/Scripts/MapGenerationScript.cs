using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationScript : MonoBehaviour
{
    private int[,] matrixMap;
    private MapUnitScript[,] matrixUnits;
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
    //private int[,] matrixResearch; //Values: 0-> not checked, 1-> Waiting for being checked, 2-> Checked
    //private int[] arrayDesirableValues = { 1, 2, 3, 4, 5, 6, 7 };
    //private int desiredValue;
    private List<MatrixPosition> desirablePositions = new List<MatrixPosition>();
    private MatrixPosition[] routeOrientations = { new MatrixPosition(1, 0), new MatrixPosition(-1, 0), new MatrixPosition(0, 1), new MatrixPosition(0, -1) };

    //Para que la corutina ExpandMap se mantenga latente 
    private MatrixPosition origin;

    //Cola para los suelos que se deban evaluar
    private Queue<MatrixPosition> queueEvaluables;

    //Contador de los suelos creados, se utilizará también como indicador de vida
    private int created;


    void Start()
    {
        // Initialize map matrix
        created = 0;
        matrixMap = new int[mapHeight, mapWidth];
        matrixUnits = new MapUnitScript[mapHeight, mapWidth];
        queueEvaluables = new Queue<MatrixPosition>();
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
        GameObject center = Instantiate(mapCenter, new Vector3(0, 0, 0), Quaternion.Euler(mapUnitRotation));
        created = 1;
        MapUnitScript unitCenter;
        //if (center.GetComponent<MapUnitScript>() != null)
        unitCenter = center.GetComponent<MapUnitScript>();
        if (unitCenter != null)
            unitCenter.unitKind = "center";

            for (int x = initialPositionX - 1; x <= initialPositionX + 1; x++)
            for (int y = initialPositionY - 1; y <= initialPositionY + 1; y++)
            {
                matrixMap[x, y] = 1;
                if (unitCenter != null)
                {
                    matrixUnits[x, y] = unitCenter;
                    Debug.Log("FirstMapUnits() La unidad x=" + x + ", y=" + y + " es " + matrixUnits[x, y].unitKind);
                }
                queueEvaluables.Enqueue(new MatrixPosition(x, y));
                //CreateMapUnit(x, y);
            }
        //Instantiate(mapCenter, new Vector3(initialPositionX, 0, initialPositionY), Quaternion.Euler(mapUnitRotation));
        
        StartCoroutine(EvaluatePositions());

    }

    private IEnumerator EvaluatePositions()
    {
        MatrixPosition posEval;
        MatrixPosition direction;
        while (queueEvaluables.Count > 0)
        {
            posEval = queueEvaluables.Peek();
            queueEvaluables.Dequeue();
            if (matrixMap[posEval.x, posEval.y] == 0 || matrixUnits[posEval.x, posEval.y] == null)
                continue;

            for (int i = 0; i < routeOrientations.Length; i++)
            {
                direction = routeOrientations[i];
                if (matrixMap[posEval.x + direction.x, posEval.y + direction.y] == 0)
                {
                    desirablePositions.Add(posEval);
                    break;
                }
                else
                    yield return null;

            }

            yield return null;
        }
    }

    public void StartExpandMap(int numUnits)
    {
        //int desiredValue = arrayDesirableValues[Random.Range(0, arrayDesirableValues.Length)];
        //StartCoroutine(SearchNewRouteOrigin(desiredValue));
        //StartCoroutine(WaitingForOrigin(numUnits));
        int index = Random.Range(0, desirablePositions.Count);
        origin = desirablePositions[index];
        desirablePositions.RemoveAt(index);

        StopAllCoroutines();
        StartCoroutine(ExpandMap(numUnits));
        //ExpandMap(numUnits);
    }

    

    /*private IEnumerator WaitingForOrigin(int numUnits)
    {
        while (origin == null)
            yield return null;

        StartCoroutine(ExpandMap(numUnits));
    }*/

    private IEnumerator ExpandMap(int numUnits)
    //private void ExpandMap(int numUnits)
    {
        Debug.Log("ExpandMap() Posicion origin x=" + origin.x + ", y=" + origin.y);
        
        Debug.Log("ExpandMap() For loop para identificar la orientación");
       
        int i = Random.Range(0, routeOrientations.Length);
        int counter = 30;
        MatrixPosition direction = routeOrientations[i];
        while (matrixMap[origin.x + direction.x, origin.y + direction.y] != 0)
        {
            i++;
            counter--;
            if (i >= routeOrientations.Length)
                i = 0;
            if (counter <= 0)

                StartExpandMap(numUnits);

            yield return null;
        }


        //Begin the instantiation
        int unitsCreated = 0;
        Debug.Log("ExpandMap() While loop para crear las unidades de suelo");
        while (unitsCreated < numUnits && numUnits < 100)
        {
            unitsCreated++;
            MatrixPosition newPos = new MatrixPosition(origin.x + direction.x * unitsCreated, origin.y + direction.y * unitsCreated);
            Debug.Log("ExpandMap() While(" + unitsCreated + " > " + numUnits + ") loop con newPos x=" + newPos.x + ", y=" + newPos.y);
            //NOTA: Esta verificación no la pongo porque no sé que hacer en caso de que ocurra esto
            // tal vez multiplicar unitsCreated por -1 y que continue en la otra dirección
            if (newPos.x < 0 || newPos.x >= mapHeight || newPos.y < 0 || newPos.y >= mapWidth)
            {
                Debug.Log("ExpandMap() if (" + newPos.x + " < 0 || " + newPos.x + " >= " + mapHeight + " || " + newPos.y + " < 0 || " + newPos.y + " >= " + mapWidth + ")");
                break;
            }

            if (matrixMap[newPos.x, newPos.y] == 0)
            {
                Debug.Log("ExpandMap() NewPos x=" + newPos.x + ", y=" + newPos.y + " está vacío y se va a construir aquí");
                CreateMapUnit(newPos.x, newPos.y);
                //Añadir nuevo suelo a la cola de evaluables
                queueEvaluables.Enqueue(newPos);
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

        //Añadir origin a cola de evaluables, y proceder a evaluar
        queueEvaluables.Enqueue(origin);
        StartCoroutine(EvaluatePositions());
    }

    public void StartDestroyMap(int numUnits)
    {
        int index = Random.Range(0, desirablePositions.Count);
        //El suelo central debe ser el último en destruirse

        MatrixPosition comprobar = desirablePositions[index];
        origin = null;

        int tries = desirablePositions.Count;
        //while(matrixUnits[origin.x, origin.y].unitKind == "center" && created > 1)
        while (origin == null && tries > 0)
        {
            if (matrixUnits[comprobar.x, comprobar.y] != null)
                Debug.Log("matrixUnits[" + comprobar.x + ", " + comprobar.y + "] != null");
            if (matrixUnits[comprobar.x, comprobar.y].unitKind == "center")
                Debug.Log("matrixUnits[" + comprobar.x + ", " + comprobar.y + ".unitKind = " + matrixUnits[comprobar.x, comprobar.y].unitKind);
            if (matrixUnits[comprobar.x, comprobar.y] != null && matrixUnits[comprobar.x, comprobar.y].unitKind == "center" && created > 1)
            {
                Debug.Log("StartDestroyMap() ha seleccionado el sueloCentral para borrarlo, pero aún quedan " + created + " suelos");
                Debug.Log("desirablePositions.Count = " + desirablePositions.Count);
                //index = Random.Range(0, desirablePositions.Count);
                index++;
                if (index >= desirablePositions.Count) index = 0;
                comprobar = desirablePositions[index];
                tries--;
            }
            else
            {

                origin = comprobar;
                desirablePositions.RemoveAt(index);

                StartCoroutine(DestroyMap(numUnits));
                break;
            }


        }



    }

    public IEnumerator DestroyMap(int numUnits)
    {
        MatrixPosition actualPos = origin;
        while(numUnits > 0)
        {
            Debug.Log("DestroyMap() empezar destruccion de x=" + actualPos.x + ", y=" + actualPos.y);
            DestroyMapUnit(origin.x, origin.y);
            numUnits--;

            //Encolar sus vecinos para evaluar
            //El orden no entorpece el resultado, así que podemos recorrer el array de principio a fin
            for(int i=0; i<routeOrientations.Length; i++)
            {
                MatrixPosition vecino = routeOrientations[i];
                if (matrixMap[actualPos.x + vecino.x, actualPos.y + vecino.y] == 1)
                    queueEvaluables.Enqueue(new MatrixPosition(actualPos.x + vecino.x, actualPos.y + vecino.y));
            }

            //Encontrar vecino que se pueda borrar
            //Ahora las orientaciones se deben comprobar por orden aleatorio
            int j = Random.Range(0, routeOrientations.Length);
            MatrixPosition direction = routeOrientations[j];
            while (matrixMap[actualPos.x + direction.x, actualPos.y + direction.y] != 1)
            {
                j++;
                if (j >= routeOrientations.Length)
                    j = 0;

                yield return null;
            }

            actualPos = new MatrixPosition(actualPos.x + direction.x, actualPos.y + direction.y);
            
            //En el caso de que no se haya encontrado ningún suelo vecino, la corutina acaba aquí
            if (matrixMap[actualPos.x, actualPos.y] != 1)
                break;


            yield return null;
        }


        //yield return null;
    }
    /*
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

            for (int x = posActual.x - 1; x <= posActual.x + 1; x++)
                for (int y = posActual.y - 1; y <= posActual.y + 1; y++)
                {
                    if(x>=0 && x < mapHeight && y >= 0 && y<mapWidth)
                        if(matrixMap[x,y] == 1)
                        {

                        }
                }
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
    }*/

    private void CreateMapUnit(int posX, int posY)
    {
        Debug.Log("CreateMapUnit() crear en posX=" + posX + ", posY=" + posY);
        matrixMap[posX, posY] = 1;

        float x = (posX - initialPositionX) * mapUnitMeasures * 2;
        float z = (posY - initialPositionY) * mapUnitMeasures * 2;

        GameObject unit = Instantiate(mapUnit, new Vector3(x, 0, z), Quaternion.Euler(mapUnitRotation));
        created++;
        if (unit.GetComponent<MapUnitScript>() != null)
            matrixUnits[posX, posY] = unit.GetComponent<MapUnitScript>();

    }

    private void DestroyMapUnit(int posX, int posY)
    {
        Debug.Log("DestroyMapUnit() crear en posX=" + posX + ", posY=" + posY);
        matrixMap[posX, posY] = 0;
        matrixUnits[posX, posY].UnitDestruction();
        created--;
        matrixUnits[posX, posY] = null;
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
