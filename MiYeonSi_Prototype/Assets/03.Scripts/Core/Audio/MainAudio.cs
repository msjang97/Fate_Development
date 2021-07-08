using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainAudio : MonoBehaviour
{
    public GameObject title;

    private new AudioSource audio;

    bool check = false; 

    private static MainAudio instance = null;
    public static MainAudio P_instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        
    }

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        //audio.playOnAwake = false;
        audio.Stop();
        check = false;
    }

    private void Update()
    {      
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            if (title.activeSelf == true)
            {
                check = true;
            }
            if (check == true)
            {
                if(!audio.isPlaying)
                audio.Play();
                check = false;
            }
        }
        else
            audio.Stop();
     
    }
}
