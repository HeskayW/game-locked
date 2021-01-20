using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SliderValueToText : MonoBehaviour
{
    public Slider sliderUI;
    private TMP_Text textSliderValue;

    void Start()
    {
        textSliderValue = GetComponent<TMP_Text>();
        ShowSliderValue(0);
    }

    public void ShowSliderValue(float var)
    {
        
        string sliderMessage = "" + sliderUI.value;
        textSliderValue.text = sliderMessage;
    }
}
