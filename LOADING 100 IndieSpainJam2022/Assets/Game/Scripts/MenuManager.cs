using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Linq;

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
    [Header("records")]
    public TextMeshProUGUI record01;
    public TextMeshProUGUI record02;
    public TextMeshProUGUI record03;
    public GameObject recordsPanel;


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

    public void LoadRecords()
    {
        recordsPanel.SetActive(true);

        List<float> records = Array.ConvertAll(PlayerPrefs.GetString("record", "0,0,0").Split(","), float.Parse).ToList().OrderBy(x => x).ToList();
        record01.text = TimeSpan.FromSeconds(records[0]).ToString(@"mm\:ss\:fff");
        record02.text = TimeSpan.FromSeconds(records[1]).ToString(@"mm\:ss\:fff");
        record03.text = TimeSpan.FromSeconds(records[2]).ToString(@"mm\:ss\:fff");
    }

    public void CloseRecords()
    {
        recordsPanel.SetActive(false);
    }
    
}
