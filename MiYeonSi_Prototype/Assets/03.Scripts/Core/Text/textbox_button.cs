using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textbox_button : MonoBehaviour
{

    public NovelController novel_con;

    public void Next_textbox()
    {
        novel_con.next_box = true;
        Debug.Log("test");
    }



}
