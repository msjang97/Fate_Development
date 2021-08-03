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
        bgm.value = SaveData.P_instance._settingData._BGM_volume;
        sfx.value = SaveData.P_instance._settingData._SFX_volume;

        bgm.onValueChanged.AddListener(GameObject.Find("AudioManager").GetComponent<AudioManager>().SetSongVolume);
        sfx.onValueChanged.AddListener(GameObject.Find("AudioManager").GetComponent<AudioManager>().SetSFXVolume);

    }

}
