using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public ELEMENTS elements;

    void Awake()
    {
        instance = this;
    }

    public void Say(string speech, string speaker = "", bool additive = false)
    {
        StopSpeaking();

        if (additive)
            speechText.text = targetSpeech;

        speaking = StartCoroutine(Speaking(speech, additive, speaker));
    }   

    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        if(textArchitect != null && textArchitect.isConstructing)
        {
            textArchitect.Stop();
        }
        speaking = null;
    }

    public bool isSpeaking { get { return speaking != null; } }
    [HideInInspector] public bool isWaitingForUserInput = false; // HideInInspector but I still want other scripts to access it

    public string targetSpeech = "";
    Coroutine speaking = null;
    TextArchitect textArchitect = null;
    public TextArchitect currentArchitect { get { return textArchitect; } }

    private void ChangeDialogueColor(string speaker)
    {
        string speechBoxPath = "SpeechBox/" + speaker + "/"+speaker;
        Debug.Log(FileManager.savPath + "Resources/SpeechBox/" + speaker + "/" + speaker);
        Debug.Log(Application.persistentDataPath + "Resources/SpeechBox/" + speaker + "/" + speaker);
        UnityEditor.AssetDatabase.IsValidFolder("Assets/Resources/SpeechBox/" + speaker + "/" + speaker);


        if (UnityEditor.AssetDatabase.IsValidFolder("Assets/Resources/SpeechBox/" + speaker))
        {
            speechPanel.transform.GetChild(1).GetComponent<RawImage>().texture = Resources.Load<Texture>(speechBoxPath);
            speechPanel.transform.GetChild(2).GetComponent<RawImage>().texture = Resources.Load<Texture>(speechBoxPath+"다음"); //추후에 수정하기.
        }
        else //엑스트라나 나레이션 일 때
        {
            speechPanel.transform.GetChild(1).GetComponent<RawImage>().texture = Resources.Load<Texture>("SpeechBox/사수진/사수진");
            speechPanel.transform.GetChild(2).GetComponent<RawImage>().texture = Resources.Load<Texture>("SpeechBox/사수진/사수진다음"); //추후에 수정하기.
        }
    }


    IEnumerator Speaking(string speech, bool additive, string speaker = "")
    {
        speechPanel.SetActive(true);
        //speechPanel.transform.GetChild(0).GetComponent<RawImage>().texture = Resources.Load<Texture>("SpeechBox/" + speaker);
        ChangeDialogueColor(speaker);

        string additiveSpeech = additive ? speechText.text : "";
        targetSpeech = additiveSpeech + speech;

        textArchitect = new TextArchitect(speechText, speech, additiveSpeech);

        speakerNameText.text = DetermineSpeaker(speaker); // 화자가 나레이션인지 판별
        isWaitingForUserInput = false;

        while (textArchitect.isConstructing)
        {
            if (Input.GetKey(KeyCode.RightArrow))
                textArchitect.skip = true;

            yield return new WaitForEndOfFrame();
        }

        //text finished
        isWaitingForUserInput = true;
        while (isWaitingForUserInput)
            yield return new WaitForEndOfFrame();
        StopSpeaking();
    }

    string DetermineSpeaker(string s)
    {
        string retVal = speakerNameText.text; // default return is the current name
        if (s != speakerNameText.text && s != "")
            retVal = (s.ToLower().Contains("narrator")) ? "" : s;

        return retVal;
    }

    /// <summary>
    /// Close the entire speech panel. Stop all dialogue.
    /// </summary>
    public void Close()
    {
        StopSpeaking();
        speechPanel.SetActive(false);
    }

    [System.Serializable]
    public class ELEMENTS
    {
        // The main panel containing all dialogue related elements on the UI
        public GameObject speechPanel;
        public Text speakerNameText;
        public Text speechText;
    }
    public GameObject speechPanel { get { return elements.speechPanel; } }
    public Text speakerNameText { get { return elements.speakerNameText; } }
    public Text speechText { get { return elements.speechText; } }
}