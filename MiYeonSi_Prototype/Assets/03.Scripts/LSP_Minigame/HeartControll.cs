using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartControll : MonoBehaviour
{
    public bool stopHeart = false;// 하트 정지
    public bool movestopHeart = false; // 가다가 멈춘 하트판단
    public bool worstStop = false; // 가다가 멈춘 하트판단
    public int worstCount = 0; 

    public ShotputControll SPC;


    private void Update()
    {
        if (worstCount == 1 && worstStop == false)
        {
            //ChoiceManager.P_instance.selectedNum = 1;
            // LovePoint.instance.eunji_LovePoint += -5;
            Debug.Log("Worst");
            //movestopHeart = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Left" || col.gameObject.tag == "Up" || col.gameObject.tag == "Right")
        {
            Debug.Log("충돌");
            Debug.Log("Worst");

            stopHeart = true;
        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Best")
        {
            movestopHeart = false;
            //ChoiceManager.P_instance.selectedNum = 4;
            // LovePoint.instance.eunji_LovePoint += 5;
            worstStop = true;

            Debug.Log("Best");
        }

        else if (col.gameObject.tag == "Good")
        {
            movestopHeart = false;
            // ChoiceManager.P_instance.selectedNum = 3;
            // LovePoint.instance.eunji_LovePoint += 3;
            worstStop = true;
            Debug.Log("Good");

        }

        else if (col.gameObject.tag == "Normal")
        {
            movestopHeart = false;
            //ChoiceManager.P_instance.selectedNum = 2;
            // LovePoint.instance.eunji_LovePoint += 0;
            worstStop = true;
            Debug.Log("Normal");

        }
        
    }
}
