using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public GameObject startBackground; // 시작화면 캐릭터 
    public GameObject minigameBackground; // 미니게임 내 캐릭터

    public Sprite[] startCharacters;// 0:고아린 1: 문준병 2: 마민석
    public Sprite[] minigameCharacters; // 0: 고아린 1: 문준병 2: 마민석


    private void Start()
    {
        // 문준병 일떄
        if (LovePoint.instance.ch_count == 1 || LovePoint.instance.ch_count == 4 || LovePoint.instance.ch_count == 7 )
        {
            startBackground.gameObject.GetComponent<Image>().sprite = startCharacters[0];
            minigameBackground.gameObject.GetComponent<Image>().sprite = minigameCharacters[0];
        }
        // 고아린 일때
        else if (LovePoint.instance.ch_count == 3 || LovePoint.instance.ch_count == 6 )
        {
            startBackground.gameObject.GetComponent<Image>().sprite = startCharacters[1];
            minigameBackground.gameObject.GetComponent<Image>().sprite = minigameCharacters[1];
        }
        // 마민석 일때
        else if (LovePoint.instance.ch_count == 2 || LovePoint.instance.ch_count == 5)
        {
            startBackground.gameObject.GetComponent<Image>().sprite = startCharacters[2];
            minigameBackground.gameObject.GetComponent<Image>().sprite = minigameCharacters[2];
        }

    }


}
