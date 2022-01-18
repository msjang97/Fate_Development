using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BackChange : MonoBehaviour
{

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MMS_Minigame" || SceneManager.GetActiveScene().name == "KAR_Minigame")
        {
            Image backImage = GetComponent<Image>();
            backImage.sprite = Resources.Load<Sprite>("Images/UI/Backdrops/" + LovePoint.instance.back_name) as Sprite;
        }
        
        else if (SceneManager.GetActiveScene().name == "LoveShotPut_Minigame" || SceneManager.GetActiveScene().name == "MJB_MiniGame")
        {
            RawImage backImage2 = GetComponent<RawImage>();
            backImage2.texture = Resources.Load<Texture>("Images/UI/Backdrops/" + LovePoint.instance.back_name) as Texture;
        }
       


        Debug.Log(LovePoint.instance.back_name);

    }
}

