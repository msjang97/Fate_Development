using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LovePoint : MonoBehaviour
{
    public static LovePoint instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    public int ch_count = 0; //챕터 카운트 (0: 프롤로그 , 1: 챕터1 ~ 7: 챕터7)

    public int enji_LovePoint = 0; // 최은지 호감도
    public int sujin_LovePoint = 0; // 사수진 호감도
    public int hagyoung_LovePoint = 0; // 안하경 호감도
    public int minseok_LovePoint = 0; // 마민석 호감도

    public void Distr_LovePoint_Cal(int point) //방해자 점수 정리해주는 함수 - 챕터 별 방해자 한테 선택지 점수 넘겨주기
    {
        // 챕터 카운트를 가져와서 각각 챕터의 방해자에게 호감도 넣어주기 (사수진-1,4 ,7 / 안하경-3,6  / 마민석-2 ,5)
        if (ch_count == 1 || ch_count == 4 || ch_count == 7) // 사수진       
            sujin_LovePoint += point;

        else if (ch_count == 3 || ch_count == 6) //안하경        
            hagyoung_LovePoint += point;

        else if (ch_count == 2 || ch_count == 5) // 마민석       
            minseok_LovePoint += point;

    }

    public void Main_LovePoint_Cal(int point)
    {
        enji_LovePoint += point;
    }

    // 엔딩 골라서 넘겨주기
    public string Choice_EndingScene()
    {
        string EndingName = "";

        if (enji_LovePoint >= 85)
        {
            EndingName = "Enji_Ending";
        }
        else
        {
            int higherPoint = sujin_LovePoint;
            EndingName = "Sujin_Ending";

            if (higherPoint < hagyoung_LovePoint)
            {
                higherPoint = hagyoung_LovePoint;
                EndingName = "Hakyung_Ending";
            }

            if (higherPoint < minseok_LovePoint)
            {
                higherPoint = minseok_LovePoint;
                EndingName = "Minseok_Ending";
            }

            if (((sujin_LovePoint == hagyoung_LovePoint || hagyoung_LovePoint == minseok_LovePoint) && higherPoint == hagyoung_LovePoint)
                || (sujin_LovePoint == minseok_LovePoint && higherPoint == sujin_LovePoint))
            {
                EndingName = "Solo_Ending";
            }
        }

        SaveData.P_instance.SaveAndLoadEndingData(EndingName);
        return EndingName;
    }
}
