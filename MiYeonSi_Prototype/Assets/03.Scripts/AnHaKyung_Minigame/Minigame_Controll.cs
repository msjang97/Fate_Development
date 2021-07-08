using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_Controll : MonoBehaviour
{
    public GameObject start_scene; 
    public GameObject minigame_scene;

    private float time;

    private void Update()
    {
        time += Time.deltaTime;
        if (time < 2.0f)
            start_scene.SetActive(true);
        else {
            start_scene.SetActive(false);
            minigame_scene.SetActive(true);
        }

    }

}
