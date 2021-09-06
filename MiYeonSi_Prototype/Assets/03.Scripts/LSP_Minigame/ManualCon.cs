using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManualCon : MonoBehaviour
{
    public GameObject upManual;
    public GameObject downManual;
    public int count;

    public Sprite seconManual;

    public GameObject start_scene;
    public GameObject minigame_scene;
    public GameObject manual;
    public GameObject stopB; // 문준병게임에 멈추기 매뉴얼
    public GameObject choice_b; // 문준병게임에 이동 바 

    public bool first_mini = false;
    private float time;

    private void Start()
    {
        count = 0;
        FirstMinigame(); // 미니게임 각각 시작시 false true 판단
    }


    private void Update()
    {
        time += Time.deltaTime;
        if (time < 2.0f)
            start_scene.SetActive(true);
        else
        {
            if (!first_mini) 
            {
                
                if (SceneManager.GetActiveScene().name == "MMS_Minigame")
                {
                    start_scene.SetActive(false);
                    minigame_scene.SetActive(false);
                    manual.SetActive(true);
                    LovePoint.instance.mini_mms = true;
                }
                else
                {
                    start_scene.SetActive(false);
                    minigame_scene.SetActive(true);
                    manual.SetActive(true);
                    Check_Minigame();
                }
            }
            else 
            {
                start_scene.SetActive(false);
                minigame_scene.SetActive(true);
                manual.SetActive(false);
            }

        }

    }

    public void FirstMinigame()
    {
        if (SceneManager.GetActiveScene().name == "MJB_MiniGame")
            first_mini = LovePoint.instance.mini_mjb;
        else if (SceneManager.GetActiveScene().name == "KAR_Minigame")
            first_mini = LovePoint.instance.mini_kar;
        else if (SceneManager.GetActiveScene().name == "LoveShotPut_Minigame")
            first_mini = LovePoint.instance.mini_lsp;
        else if (SceneManager.GetActiveScene().name == "MMS_Minigame")
            first_mini = LovePoint.instance.mini_mms;
    }

    public void Check_Minigame()
    {
        if (SceneManager.GetActiveScene().name == "MJB_MiniGame")
            LovePoint.instance.mini_mjb = true;
        else if (SceneManager.GetActiveScene().name == "KAR_Minigame")
            LovePoint.instance.mini_kar = true;
        else if (SceneManager.GetActiveScene().name == "LoveShotPut_Minigame")
            LovePoint.instance.mini_lsp = true;
    }

    public void Touch_Manual()
    {
        Manual_Controll();
       // manual.SetActive(false);
       //minigame_scene.SetActive(true);
    }


    public void Manual_Controll()
    {
        count++;
        switch (count)
        {
            case 1:
                upManual.SetActive(false);
                downManual.SetActive(true);
                break;
            case 2:
                downManual.transform.GetComponent<Image>().sprite = seconManual;
                if (SceneManager.GetActiveScene().name == "MJB_MiniGame")
                {
                    choice_b.SetActive(false);
                    stopB.SetActive(true);
                }
                break;
            case 3:
                 manual.SetActive(false);
                minigame_scene.SetActive(true);
                break;
            default:
                break;
        }

    }

}
