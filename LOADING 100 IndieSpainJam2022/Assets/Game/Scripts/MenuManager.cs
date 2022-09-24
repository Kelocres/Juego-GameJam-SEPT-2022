using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject startPanel;
    public GameObject startButton;
    public AudioClip menuTheme;
    public AudioClip mainTheme;
    public TextWriter writer;
    public TextMeshProUGUI intro;
    [TextArea]
    public string introText;


    private void Start()
    {
        SoundManager.instance.StartMusic(menuTheme);
    }

    public void StartGame()
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
        yield return new WaitForSeconds(3);
        writer.Write(intro, introText);
        yield return new WaitForSeconds(5);
        startButton.SetActive(true);
    }
    
}
