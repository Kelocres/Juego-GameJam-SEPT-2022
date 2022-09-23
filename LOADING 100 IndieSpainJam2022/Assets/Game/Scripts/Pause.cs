using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public bool isPause = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPause)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();    
            }
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        isPause = true;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        pausePanel.SetActive(false);
        isPause = false;
        Time.timeScale = 1;
    }
}
