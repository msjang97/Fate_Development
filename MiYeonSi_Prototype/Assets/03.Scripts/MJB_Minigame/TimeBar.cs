using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{   
    public float TimeCost;
    public Bar bar;
    public Slider TimeSlider;
    public GameObject manual;
    private void Awake()
    {
        TimeSlider.maxValue = TimeCost;             
    }

    void Update()
    {
        if (bar.P_isStoped == false &&manual.activeSelf == false)
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
