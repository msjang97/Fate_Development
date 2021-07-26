using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_Controll : MonoBehaviour
{
    public GameObject start_scene; 
    public GameObject minigame_scene;
    public GameObject manual;
    public int miniC; //임시변수
        
    private float time;

    private void Update()
    {
       
        time += Time.deltaTime;
        if (time < 2.0f)
            start_scene.SetActive(true);
        else {
            if (miniC == 0) // LovePoint.instance.minigameCount == 0
            {
                //LovePoint.instance.minigameCount ++;
                start_scene.SetActive(false);
                minigame_scene.SetActive(false);
                manual.SetActive(true);
                
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
        manual.SetActive(false);
        minigame_scene.SetActive(true);
    }
}
