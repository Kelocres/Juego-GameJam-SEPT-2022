using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
public class TimeManager : MonoBehaviour
{
    public float currentTime;
    public TextMeshProUGUI currentTimeText;
    bool timeActive;
    public float finalTime;


    private void Start()
    {
        currentTime = 0;
        timeActive = true;
    }

    private void Update()
    {
        if (timeActive)
        {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
    }

    public void SaveRecord()
    {
        List<float> records = Array.ConvertAll(PlayerPrefs.GetString("record", "0,0,0").Split(","), float.Parse).ToList();
        records.Add(finalTime);
        List<float> newRecords = records.OrderBy(x=>-x).ToList();
        newRecords.RemoveAt(newRecords.Count - 1);
        if(PlayerPrefs.GetFloat("record", 0) < finalTime)
        {
            
            PlayerPrefs.SetString("Record",String.Join(",",newRecords.ToArray()));
        }
    }


}
