using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float time = 30;
    private int currentTime;
    public static bool stop = false;
    public Slider TimeSlider;

    // Use this for initialization
    void Start()
    {
        TimeSlider.maxValue = time;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        TimeSlider.value = time;
        currentTime = (int)time;

        if (currentTime <= 0)
        {
            currentTime = 0;
            ChoiceManager.P_instance.selectedNum = 1;
        }
    }
}
