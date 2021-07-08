using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    public Text textA;
    public string text { get { return textA.text; } set { textA.text = value; } }

    [HideInInspector]
    public int choiceIndex = -1;
}
