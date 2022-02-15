using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class SliderHandler : MonoBehaviour
{
    [SerializeField]
    private PhysicsGadgetConfigurableLimitReader _slider;
    public void DisplaySliderValue()
    {
        float value = _slider.GetValue();
        VRDebugMessage.Instance.Print($"Slider value is {value}");
    }
}
