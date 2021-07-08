using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton3 : MonoBehaviour
{
    public int AHKlove3 = 0;  // 0에서 시작

    public int AHKlovePerClick3 = 1;  // 한 번 터치 할 때마다 1씩 증가

    public void OnClick()
    {
        AHKlove3 = AHKlove3 + AHKlovePerClick3;
    }

}
