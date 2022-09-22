using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int loadingBar = 5;
    MapGenerationScript mapScript;
    public TextMeshProUGUI loadingText;

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
        mapScript = GetComponent<MapGenerationScript>();
        GameManager.instance.UpdateLoading();
    }
    public void FillBar(int points)
    {
        loadingBar += points;
        mapScript.StartExpandMap(1);
    }

    public void EmptyBar(int points)
    {
        if(loadingBar > 10)
        {
            mapScript.StartDestroyMap(1);
        }
        loadingBar -= points;
    }

    public void UpdateLoading()
    {
        loadingText.text = "Loading..." + loadingBar.ToString() + "%";
    }
}
