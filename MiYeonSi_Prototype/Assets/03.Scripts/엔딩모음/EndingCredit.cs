using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    private float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 17.5f)
        {
            AudioManager.instance.StopSong();
            SceneManager.LoadScene("StartScene");
        }
    }
}
