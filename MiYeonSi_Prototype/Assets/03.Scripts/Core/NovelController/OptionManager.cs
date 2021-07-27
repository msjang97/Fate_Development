using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public Slider bgm;
    public Slider sfx;

    private void Start()
    {
        SetAudio();
    }

    void SetAudio()
    {
        bgm.onValueChanged.AddListener( GameObject.Find("AudioManager").GetComponent<AudioManager>().SetSongVolume);
        sfx.onValueChanged.AddListener(GameObject.Find("AudioManager").GetComponent<AudioManager>().SetSFXVolume);
    }

}
