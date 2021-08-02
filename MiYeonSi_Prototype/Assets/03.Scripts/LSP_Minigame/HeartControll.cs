using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartControll : MonoBehaviour
{
    public bool stopHeart = false;// 하트 정지
    public bool movestopHeart = false; // 가다가 멈춘 하트판단
    public bool worstStop = false; // 가다가 멈춘 하트판단

    public ShotputControll SPC;
   
    private void LateUpdate()
    {
        if (SPC.powerSpeed == 0 && movestopHeart)
        {
            if (worstStop == false)
            {
                //ChoiceManager.P_instance.selectedNum = 1;
                // LovePoint.instance.eunji_LovePoint += -5;
                Debug.Log("Worst");
                //movestopHeart = false;
                worstStop = true;
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (SPC.powerSpeed > 0)
        {
            if (col.gameObject.tag == "Left" || col.gameObject.tag == "Up" || col.gameObject.tag == "Right")
            {
                Debug.Log("충돌");
                Debug.Log("Worst");

                stopHeart = true;
            }
        }
       
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (SPC.powerSpeed == 0)
        {
            if (col.gameObject.tag == "Best")
            {
                movestopHeart = false;               
                worstStop = true;
                if (worstStop)
                {
                    //ChoiceManager.P_instance.selectedNum = 4;
                    // LovePoint.instance.eunji_LovePoint += 5;
                    Debug.Log("Best");
                }
                
            }

            else if (col.gameObject.tag == "Good")
            {
                movestopHeart = false;               
                worstStop = true;
                if (worstStop)
                {
                    // ChoiceManager.P_instance.selectedNum = 3;
                    // LovePoint.instance.eunji_LovePoint += 3;
                    Debug.Log("Good");
                }
                
            }

            else if (col.gameObject.tag == "Normal")
            {
                movestopHeart = false;
                worstStop = true;
                if (worstStop)
                {
                    //ChoiceManager.P_instance.selectedNum = 2;
                    // LovePoint.instance.eunji_LovePoint += 0;
                    Debug.Log("Normal");
                }
               
            }
           
        }     
        
    }
}
