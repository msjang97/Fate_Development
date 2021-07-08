using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choices : MonoBehaviour
{
    public Text[] ChoiceTexts;

    void Start()
    {
        InputText(); //mainScene에서 넘어온 텍스트를 미니게임 선택지에 적용.
    }

    private void InputText()
    {
        for(int i = 0; i < 4; i++)
        {
            ChoiceTexts[i].text = ChoiceManager.P_instance.choices[i];
        }
    }
}
