using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textbox_button : MonoBehaviour
{
    public NovelController novel_con;
    [SerializeField]
    public static  bool skipB;

    public void Next_textbox()
    {
        /*if (!LovePoint.instance._next)
        {
            novel_con.next_box = true;
            LovePoint.instance._next = true;
           
        }
        else
        {
            LovePoint.instance._skip = true;

        }*/

        if (!novel_con.next_box)
        {
            novel_con.next_box = true;
            //LovePoint.instance._next = true;
            if (novel_con.choiceNext)
                LovePoint.instance._choiceNext = true;
        }
  


    }

}
