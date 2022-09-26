using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class CanvasManager : MonoBehaviour
{
    public Slider loadingSlider;
    [Header("win")]
    public GameObject winCanvas;
    public TextMeshProUGUI recordText;
    public TimeManager timeManager;
    public TextWriter writer;
    public TextMeshProUGUI place;
    [TextArea]
    public string winMessage;
    public AudioClip winSound;
    [Header("Lose")]
    public GameObject loseCanvas;
    public TextMeshProUGUI losePlace;
    [TextArea]
    public string loseMessage;
    public AudioClip loseSound;


    // Start is called before the first frame update
    void Start()
    {
        loadingSlider.maxValue = 100;
        loadingSlider.value = 5;
    }

    public void UpdateSlider(int load)
    {
        loadingSlider.value = load;
    }

    public void ShowWinCanvas()
    {
        winCanvas.SetActive(true);
        SoundManager.instance.PlaySound(winSound);
        SoundManager.instance.musicSource.Stop();
        writer.Write(place, winMessage);
        timeManager.finalTime = timeManager.currentTime;
        timeManager.SaveRecord();
        recordText.text = TimeSpan.FromSeconds(timeManager.finalTime).ToString(@"mm\:ss\:fff");
    }

    public void ShowLoseCanvas()
    {
        loseCanvas.SetActive(true);
        writer.Write(losePlace, loseMessage);
        SoundManager.instance.PlaySound(loseSound);
        SoundManager.instance.musicSource.Stop();
    }

    public void GoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

}
