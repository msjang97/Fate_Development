using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : MonoBehaviour
{
    public GameObject OptionButton;

    private void Awake()
    {
        StartCoroutine("HideOptionButtonForSeconds");
    }

    IEnumerator HideOptionButtonForSeconds()
    {
        OptionButton.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        OptionButton.SetActive(true);
    }
}
