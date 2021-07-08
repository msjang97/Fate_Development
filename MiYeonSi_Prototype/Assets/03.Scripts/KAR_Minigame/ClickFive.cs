using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFive : MonoBehaviour
{
    public int love = 0;

    public int lovePerClick = 1;

    public void Onclick()
    {
        love = love + lovePerClick;
    }
    
}
