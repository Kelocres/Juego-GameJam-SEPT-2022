using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;

public class TextWriter : MonoBehaviour
{
    public bool isWriting;

    [Header("Editable Data")]
    public float TextVelocity;

    [Header("Speak Effect")]
    public AudioSource aSource;
    public AudioClip aClip;
    int chrIdx;

    private void Awake()
    {
        //FillSoundDictionary();
    }

    public async void Write(TextMeshProUGUI testText, string textToWrite)
    {
        isWriting = true;
        List<string> charsToWrite = PrepareStringToWrite(textToWrite);
        string display = "";
        chrIdx = 0;
        foreach (string chr in charsToWrite)
        {
            string chrToWrite = chr;
            if (!isWriting)
            {
                testText.text = textToWrite;
                break;
            }
            if (chrToWrite != " ")
            {
                chrIdx++;
                VoiceEffect();
                await Task.Delay(TimeSpan.FromSeconds(TextVelocity));

            }
            display += chrToWrite;
            testText.text = display;
        }

        isWriting = false;
    }


    void VoiceEffect()
    {
            aSource.PlayOneShot(aClip);
    }


    List<string> PrepareStringToWrite(string text)
    {
        List<string> chars = new List<string>();
 

        for (int i = 0; i < text.Length; i++)
        {

            chars.Add(text[i].ToString());
        }

        return chars;
    }
}
