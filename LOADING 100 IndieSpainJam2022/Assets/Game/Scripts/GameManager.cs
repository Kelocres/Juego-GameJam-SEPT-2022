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
        canvasScript = GameObject.Find("UI_Canvas").GetComponent<CanvasManager>();
    }
    public void FillBar(int points)
    {
        loadingBar += points;
        canvasScript.UpdateSlider(loadingBar);
        //Vector3 scalame = new Vector3(loadingBar / 100f, 1f, 1f);
        //bar_loading.rectTransform.localScale = scalame;
        if(loadingBar%2 == 0)
        {
            mapScript.StartExpandMap(1);
            Debug.Log("se amplia");
        }
        if(loadingBar >= 100)
        {
            WinGame();
        }
    }

    public void EmptyBar(int points)
    {
        if(loadingBar > 10)
        {
            mapScript.StartDestroyMap(1);
        }
        loadingBar -= points;
        canvasScript.UpdateSlider(loadingBar);
        if(loadingBar <= 0)
        {
            LoseGame();
        }
    }

    public void WinGame()
    {
        GameObject.Find("EnemySpawner").SetActive(false);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {Destroy(enemy);}
        canvasScript.ShowWinCanvas();
    }

    public void LoseGame()
    {
        GameObject.Find("EnemySpawner").SetActive(false);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) { Destroy(enemy); }
        canvasScript.ShowLoseCanvas();
    }
}
