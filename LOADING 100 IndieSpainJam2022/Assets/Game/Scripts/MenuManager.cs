using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject startPanel;
    public GameObject startText;
    public AudioClip menuTheme;
    public AudioClip mainTheme;



    private void Start()
    {
        SoundManager.instance.StartMusic(menuTheme);
    }

    void StartGame()
    {
        SoundManager.instance.StartMusic(mainTheme);
        SceneManager.LoadScene("FinalTest");       
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
