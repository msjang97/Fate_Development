using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{   
    private Vector3 myPosition;
    public Bar bar;
    public TimeBar timeBar;
    static public bool lovecheck = false;

    void Start()
    {
        myPosition = this.transform.position;
        lovecheck = false;
    }

    void Update()
    {
        if (bar.P_isStoped == true) //바가 멈추면 SelectChoice() 실행.
        {
            SelectChoice();  
        }
        else if (timeBar.TimeCost <= 0.0f ) //시간초가 다 되면 TiemOutSelectChoice() 실행.
        {
            if(this.name == "Choice1")
            TimeOutSelectChoice();
        }    
    }

    private void SelectChoice() //바 위치 검사해서 선택된 번호 넘겨주기.
    {
        if (bar.BarPosition.y < myPosition.y + 120 && bar.BarPosition.y > myPosition.y -120)
        {
            bar.BarPosition = Vector2.Lerp(bar.BarPosition, myPosition, 0.9f);
            name = name.Replace("Choice", "");
            ChoiceManager.P_instance.selectedNum = int.Parse(name);

            if (lovecheck == true) //호감도 조정해주기.
            {
                Set_Lovepoint();
                lovecheck = false;
            }
        }
    }

    private void TimeOutSelectChoice() //시간이 다 지났을 때 1번 선택지가 자동으로 선택되게.
    {
        bar.BarPosition = Vector2.Lerp(bar.BarPosition, myPosition, 0.9f);
        ChoiceManager.P_instance.selectedNum = 4;

        if (lovecheck == true) //호감도 조정해주기.
        {
            Set_Lovepoint();
            lovecheck = false;
        }
    }

    public void Set_Lovepoint() //호감도 조정.
    {
        switch (ChoiceManager.P_instance.selectedNum)
        {
            case 1:
                LovePoint.instance.Main_LovePoint_Cal(5);
                break;
            case 2:
                LovePoint.instance.Main_LovePoint_Cal(3);
                break;
            case 3:
                LovePoint.instance.Main_LovePoint_Cal(0);
                break;
            case 4:
                LovePoint.instance.Main_LovePoint_Cal(-5);
                break;
            default:
                break;
        }
    }
}
