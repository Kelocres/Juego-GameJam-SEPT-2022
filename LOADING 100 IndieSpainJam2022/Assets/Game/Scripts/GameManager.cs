using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int loadingBar = 5;
    MapGenerationScript mapScript;
    public CanvasManager canvasScript;
    

    private void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        mapScript = GameObject.Find("MapGenerator").GetComponent<MapGenerationScript>();
    }
    public void FillBar(int points)
    {
        loadingBar += points;
        canvasScript.UpdateSlider(loadingBar);
        //Vector3 scalame = new Vector3(loadingBar / 100f, 1f, 1f);
        //bar_loading.rectTransform.localScale = scalame;
        mapScript.StartExpandMap(1);
    }

    public void EmptyBar(int points)
    {
        if(loadingBar > 10)
        {
            mapScript.StartDestroyMap(1);
        }
        loadingBar -= points;
        canvasScript.UpdateSlider(loadingBar);
    }
}
