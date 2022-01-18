using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LovePoint : MonoBehaviour
{
    public static LovePoint instance = null;
    public static LovePoint P_instance
    {
        get
        {
            if (null == instance)
            {
                instance = null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    public bool goEnd;
    public int ch_count = 0; //챕터 카운트 (0: 프롤로그 , 1: 챕터1 ~ 7: 챕터7)

    public int eunji_LovePoint = 0; // 최은지 호감도
    public int junbyeong_LovePoint = 0; // 문준병 호감도
    public int arin_LovePoint = 0; // 고아린 호감도
    public int minseok_LovePoint = 0; // 마민석 호감도
    public int end_num = 0;
    public int minigameCount = 0;
    public bool mini_kar = false;
    public bool mini_mms = false;
    public bool mini_mjb = false;
    public bool mini_lsp = false;

    //대화 관련 변수===============================================================
    public bool _skip;
    public bool _next;
    public bool _choiceNext;
    public bool _isAfterMiniGame;

    // 분기점 이야기 변수
    public bool _isBranch; //분기점 열렸는지 확인
    public bool _goBranch;// 선택지 분기점 열어주기
    public int _numBranch; // 분기점 번호

    //미니게임 배경이름 가져오기 변수
    public string back_name;

    void Init()
    {
        SaveData.P_instance.LoadOnceCheckData();

        mini_kar = SaveData.P_instance._onceCheckData.mini_kar;
        mini_mms = SaveData.P_instance._onceCheckData.mini_mms;
        mini_mjb = SaveData.P_instance._onceCheckData.mini_mjb;
        mini_lsp = SaveData.P_instance._onceCheckData.mini_lsp;

    }

    public void ResetData()
    {
        ch_count = 0;

        eunji_LovePoint = 0; // 최은지 호감도
        junbyeong_LovePoint = 0; // 문준병 호감도
        arin_LovePoint = 0; // 고아린 호감도
        minseok_LovePoint = 0; // 마민석 호감도
    }

    // =========================변경가능성 있음 ===================================
    public void Distr_LovePoint_Cal(int point) //방해자 점수 정리해주는 함수 - 챕터 별 방해자 한테 선택지 점수 넘겨주기
    {
        // 챕터 카운트를 가져와서 각각 챕터의 방해자에게 호감도 넣어주기 (문준병-1,4 ,7 / 고아린-3,6  / 마민석-2 ,5)
        if (ch_count == 1) // 문준병       
        {
            junbyeong_LovePoint += point;
            Debug.Log("LovePoint Check");
        }


        else if (ch_count == 2) //고아린  
        {
            arin_LovePoint += point;
            Debug.Log("LovePoint Check");
        }

        else if (ch_count == 3) // 마민석  
        {
            minseok_LovePoint += point;
            Debug.Log("LovePoint Check");
        }

    }

    //===============================================================================

    public void Main_LovePoint_Cal(int point)
    {
        eunji_LovePoint += point;
    }

    // 엔딩 골라서 넘겨주기
    public string Choice_EndingScene()
    {
        string EndingName = "";

        if (eunji_LovePoint >= 85)
        {
            EndingName = "Euna_Ending";
        }
        else
        {
            int higherPoint = junbyeong_LovePoint;
            EndingName = "Junbyoeng_Ending";

            if (higherPoint < arin_LovePoint)
            {
                higherPoint = arin_LovePoint;
                EndingName = "Arin_Ending";
            }

            if (higherPoint < minseok_LovePoint)
            {
                higherPoint = minseok_LovePoint;
                EndingName = "Minseok_Ending";
            }

            if (((junbyeong_LovePoint == arin_LovePoint || arin_LovePoint == minseok_LovePoint) && higherPoint == arin_LovePoint)
                || (junbyeong_LovePoint == minseok_LovePoint && higherPoint == junbyeong_LovePoint))
            {
                EndingName = "Solo_Ending";
            }
        }

        SaveData.P_instance.SaveAndLoadEndingData(EndingName);
        return EndingName;
    }
}
