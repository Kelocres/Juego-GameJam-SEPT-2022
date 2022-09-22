using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject startPanel;
    public GameObject startText;

     void StartGame()
    {
        SceneManager.LoadScene("FinalTest");
        Debug.Log("startGame");
    }


    public void startRutine()
    {
        StartCoroutine(startloading());
    }
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    IEnumerator startloading()
    {
        startPanel.SetActive(true);
        yield return new WaitForSeconds(5);
        startText.SetActive(true);
        yield return new WaitForSeconds(5);
        StartGame();
    }
    
}
