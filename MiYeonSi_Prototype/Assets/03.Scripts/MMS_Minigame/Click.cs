using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{ 
    public void Click_Button()
    {
        SelectChoice();
        this.gameObject.SetActive(false);
    }

    private void SelectChoice()
    {                 
        name = name.Replace("Button", "");
        ChoiceManager.P_instance.selectedNum = int.Parse(name);

        switch (ChoiceManager.P_instance.selectedNum)
        {
            case 1:
                LovePoint.instance.eunji_LovePoint += 5;
                break;
            case 2:
                LovePoint.instance.eunji_LovePoint += 3;
                break;
            case 3:
                LovePoint.instance.eunji_LovePoint += 0;
                break;
            case 4:
                LovePoint.instance.eunji_LovePoint += -5;
                break;
            default:
                break;
        }
    }

}
