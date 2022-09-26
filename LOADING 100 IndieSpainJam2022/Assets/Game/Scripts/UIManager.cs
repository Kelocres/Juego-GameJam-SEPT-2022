using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UIManager : MonoBehaviour
{
    private Player _player;
    private MapGenerationScript _mapGeneration;
    private GameManager _gameManager;

    public RawImage bar_loading;
    public RawImage bar_powerWhite;
    //public RawImage bar_powerRed;
    public RawImage bar_powerBlue;
    private Image bar_powerRed;
    [SerializeField]
    private PlayerMediator _playerMediator;
    bool canFillRed;
    [SerializeField]
    private float initTimeUntilRefillR;
    private float initTimeR;
    // Start is called before the first frame update
    void Start()
    {
        canFillRed = true;
        var barra = GameObject.FindGameObjectWithTag("bar_powerRed");
        bar_powerRed = GameObject.FindGameObjectWithTag("bar_powerRed").GetComponent<Image>();
        /*_player = FindObjectOfType<Player>();
        _mapGeneration = FindObjectOfType<MapGenerationScript>();
        _gameManager = FindObjectOfType<GameManager>();

        bar_loading = GameObject.FindGameObjectWithTag("bar_loading").GetComponent<RawImage>();
        bar_powerWhite = GameObject.FindGameObjectWithTag("bar_powerWhite").GetComponent<RawImage>();
        
        bar_powerBlue = GameObject.FindGameObjectWithTag("bar_powerBlue").GetComponent<RawImage>();*/
        
        InvokeRepeating("fillBarPower", 5.0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("UIManager Update() _gameManager.loadingBar = "+_gameManager.loadingBar);
        /*Vector3 scalame = new Vector3(_gameManager.loadingBar / 100f, 1f, 1f);
        if (scalame.x < 0f) scalame.x = 0f;
        else if (scalame.x > 1f) scalame.x = 1f;
        bar_loading.rectTransform.localScale = scalame;*/
         
            if(Time.time> initTimeR && initTimeR>0)
            {
                initTimeR=0;
                InvokeRepeating("fillBarPower", 2.0f, 2f);
                _playerMediator.resetBarR();
                _playerMediator.trychangeMoveT();
        }
                
         
    }
    public void fillBarPower()
    {
          canFillRed=_playerMediator.canfillBarActivatePower(bar_powerRed);
        if (!canFillRed)
        {
            initTimeR = initTimeUntilRefillR + Time.time;
            CancelInvoke("fillBarPower");
            _playerMediator.trychangeMoveT();
        }
            
    }

}
