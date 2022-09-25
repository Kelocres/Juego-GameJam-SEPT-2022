using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Slider loadingSlider;

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

}
