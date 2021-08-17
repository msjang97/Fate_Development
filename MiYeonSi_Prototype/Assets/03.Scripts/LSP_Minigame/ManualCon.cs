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
    public int miniC; //임시변수

    private float time;

    private void Start()
    {
        count = 0;
    }


    private void Update()
    {

        time += Time.deltaTime;
        if (time < 2.0f)
            start_scene.SetActive(true);
        else
        {
            if (miniC == 0) // LovePoint.instance.minigameCount == 0
            {
                //LovePoint.instance.minigameCount ++;
                
                if (SceneManager.GetActiveScene().name == "MMS_Minigame")
                {
                    start_scene.SetActive(false);
                    minigame_scene.SetActive(false);
                    manual.SetActive(true);
                }
                else
                {
                    start_scene.SetActive(false);
                    minigame_scene.SetActive(true);
                    manual.SetActive(true);
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
