using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene_PlFive : MonoBehaviour
{
    public int max;
    int click;


    public void Filled_Image()
    {
        float n = 1.0f / (float)max;
        this.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount += n;
        Debug.Log(n);
    }


    public void ChangeScene_PlFiveBtn()
    {
        click = transform.GetComponent<ClickFive>().love;
        if (click >= max)
        {
            switch (this.gameObject.name)
            {
                case "Button1":
                    name = name.Replace("Button", "");
                    ChoiceManager.P_instance.selectedNum = int.Parse(name);
                    LovePoint.instance.euna_LovePoint += -5;
                    break;
                case "Button2":
                    name = name.Replace("Button", "");
                    ChoiceManager.P_instance.selectedNum = int.Parse(name);
                    LovePoint.instance.euna_LovePoint += 0;
                    break;
                case "Button3":
                    name = name.Replace("Button", "");
                    ChoiceManager.P_instance.selectedNum = int.Parse(name);
                    LovePoint.instance.euna_LovePoint += 3;
                    break;
                case "Button4":
                    name = name.Replace("Button", "");
                    ChoiceManager.P_instance.selectedNum = int.Parse(name);
                    LovePoint.instance.euna_LovePoint += 5;

                    break;
            }
        }
    }
}
