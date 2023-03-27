using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image sliderFill;

    public void SetSliderMaxValue(int myMaxValue)
    {
        slider.maxValue = myMaxValue;

        sliderFill.color = gradient.Evaluate(1f);
    }

    public void SetSliderCurrentValue(int myCurrentValue)
    {
        slider.value = myCurrentValue;

        sliderFill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
