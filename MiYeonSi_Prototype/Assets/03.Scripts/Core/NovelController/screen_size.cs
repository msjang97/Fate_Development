using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screen_size : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1080,2400, true);
        Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true); // 16:9 로 개발시
 
    }


}
