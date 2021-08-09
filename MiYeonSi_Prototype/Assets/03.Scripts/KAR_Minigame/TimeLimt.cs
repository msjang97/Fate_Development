using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeLimt : MonoBehaviour
{
    public static float rTime;
    public Slider TimeSlider;
    public GameObject manual; 

    void Start()
    {
        rTime = 10f;
        TimeSlider.maxValue = rTime;
    }

    void Update()
    {
        if (manual.activeSelf == true)
            rTime = 10f;


        else
        {
            rTime -= Time.deltaTime;
            TimeSlider.value = rTime;

            if (rTime < 0)
            {
                rTime = 0;
                ChoiceManager.P_instance.selectedNum = 1;
            }
        }
       
    }
}
