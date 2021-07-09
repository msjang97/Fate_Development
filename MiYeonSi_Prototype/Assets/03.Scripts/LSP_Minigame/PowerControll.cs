using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerControll : MonoBehaviour
{
    private float fullPower = 0;
    private float count = 10;
    public Slider TimeSlider;

    void Start()
    {
        this.TimeSlider.maxValue = 15;
    }

    private void Update()
    {
        PowerCharging();
        
    }
    void PowerCharging()
    {
        fullPower += count * Time.deltaTime;
        this.TimeSlider.value = fullPower;

        if (this.TimeSlider.value == this.TimeSlider.maxValue || this.TimeSlider.value == this.TimeSlider.minValue)
            count = count * -1;

    }
}
