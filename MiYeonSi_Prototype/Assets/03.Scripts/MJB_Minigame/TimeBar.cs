using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{   
    public float TimeCost;
    public Bar bar;
    public Slider TimeSlider;

    private void Awake()
    {
        TimeSlider.maxValue = TimeCost;             
    }

    void Update()
    {
        if (bar.P_isStoped == false)
            CountTime();
    }

    private void CountTime()
    {
        if (TimeCost <= 0)
            TimeCost = 0;
        TimeCost -= Time.deltaTime;
        TimeSlider.value = TimeCost;       
    }
}
