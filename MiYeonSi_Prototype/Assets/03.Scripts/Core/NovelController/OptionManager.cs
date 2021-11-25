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

        AudioManager.instance.SetSFXVolume(sfx.value);

        bgm.onValueChanged.AddListener(AudioManager.instance.SetSongVolume);
        sfx.onValueChanged.AddListener(AudioManager.instance.SetSFXVolume);

    }

    public void SetActiveOption()
    {
        this.gameObject.SetActive(true);
        AudioManager.instance.PlayButtonSound();
    }

    public void SetInActiveOption()
    {
        this.gameObject.SetActive(false);
    }


}
