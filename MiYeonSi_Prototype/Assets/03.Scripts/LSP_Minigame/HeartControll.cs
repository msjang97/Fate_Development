using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartControll : MonoBehaviour
{
    public bool stopHeart = false;// 하트 정지
    public bool movestopHeart = false; // 가다가 멈춘 하트판단
    public ShotputControll SPC;

    


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Left" || col.gameObject.tag == "Up" || col.gameObject.tag == "Right")
        {
            Debug.Log("충돌");
            stopHeart = true;
        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (movestopHeart)
        {
            if (col.gameObject.tag == "Best")
            {
               //ChoiceManager.P_instance.selectedNum = 4;
               // LovePoint.instance.eunji_LovePoint += 5;
                movestopHeart = false;

                Debug.Log("Best");
            }

            else if (col.gameObject.tag == "Good")
            {
               // ChoiceManager.P_instance.selectedNum = 3;
               // LovePoint.instance.eunji_LovePoint += 3;
                movestopHeart = false;

                Debug.Log("Good");

            }

            else if (col.gameObject.tag == "Normal")
            {
                //ChoiceManager.P_instance.selectedNum = 2;
               // LovePoint.instance.eunji_LovePoint += 0;
                movestopHeart = false;

                Debug.Log("Normal");

            }

            else //if (col.gameObject.tag == "Worst")
            {
                //ChoiceManager.P_instance.selectedNum = 1;
               // LovePoint.instance.eunji_LovePoint += -5;
                movestopHeart = false;

                Debug.Log("Worst");

            }
        }
    }
}
