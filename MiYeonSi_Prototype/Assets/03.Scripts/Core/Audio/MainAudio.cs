using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainAudio : MonoBehaviour
{
    private void Start()
    {
        AudioClip clip = Resources.Load("Audio/Music/" + "Title") as AudioClip;

        if (clip != null)
        {
            AudioManager.instance.PlaySong(clip);
        }

    }

}
