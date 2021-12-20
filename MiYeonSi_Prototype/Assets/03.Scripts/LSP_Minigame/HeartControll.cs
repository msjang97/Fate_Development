using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartControll : MonoBehaviour
{
    public bool stopHeart = false;// 하트 정지
    public bool movestopHeart = false; // 가다가 멈춘 하트판단
    public bool worstStop = false; // 가다가 멈춘 하트판단

    public ShotputControll SPC;
    public Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //animator.SetBool("heartbomb", true);

        if (SPC.powerSpeed == 0 )
        {
            if (movestopHeart && worstStop)
            {
                Debug.Log("Worst");

                ChoiceManager.P_instance.selectedNum = 1;
                LovePoint.instance.eunji_LovePoint += -5;
                //animator.SetBool("heartbomb", true);

                //movestopHeart = false;
                worstStop = false;
            }
            animator.SetBool("heartbomb", true);

        }
       
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (SPC.powerSpeed > 0)
        {
            if (col.gameObject.tag == "Left" || col.gameObject.tag == "Up" || col.gameObject.tag == "Right")
            {
                animator.SetBool("heartbomb", true);

                Debug.Log("충돌");
                Debug.Log("Worst");
                SPC.powerSpeed = 0;

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
                worstStop = false;
                if (!worstStop)
                {
                    Debug.Log("Best");
                    ChoiceManager.P_instance.selectedNum = 1;
                     LovePoint.instance.eunji_LovePoint += 5;
                }
                
            }

            else if (col.gameObject.tag == "Good")
            {
                movestopHeart = false;               
                worstStop = false;
                if (!worstStop)
                {
                    Debug.Log("Good");

                    ChoiceManager.P_instance.selectedNum = 2;
                    LovePoint.instance.eunji_LovePoint += 3;
                }
                
            }

            else if (col.gameObject.tag == "Normal")
            {
                movestopHeart = false;
                worstStop = false;
                if (!worstStop)
                {
                    Debug.Log("Normal");

                    ChoiceManager.P_instance.selectedNum = 3;
                    LovePoint.instance.eunji_LovePoint += 0;
                }
               
            }
            else if (col.gameObject.tag == "Worst")
            {
                ChoiceManager.P_instance.selectedNum = 4;
                LovePoint.instance.eunji_LovePoint += -5;
                worstStop = true;
            }
        }     
        
    }
}
