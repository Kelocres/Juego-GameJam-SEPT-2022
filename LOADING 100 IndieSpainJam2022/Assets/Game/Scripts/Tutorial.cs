using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public TextWriter writer;
    public TextMeshProUGUI place;
    [TextArea]
    public string tutorial;


    void Start()
    {
        writer.Write(place, tutorial);
    }

    private void Update()
    {
        if (!writer.isWriting)
        {
            StartCoroutine(CloseTutorial());
        }
    }
    
    IEnumerator CloseTutorial()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
