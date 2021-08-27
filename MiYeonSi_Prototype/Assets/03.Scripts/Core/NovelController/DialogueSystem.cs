using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using static System.IO.Directory;


public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public ELEMENTS elements;

    //주인공 배열 생성
    private string[] MainCharacterList = new string[6] { "지찬우", "최은지", "연애세포", "문준병", "고아린", "마민석"}; 

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

    private void ChangeDialogueColor(string speaker) //화자를 인자로 받아, 각 화자에 따라 채팅창, 이름표, 동그라미버튼 색 바꾸기.
    {
        string speechBoxPath = "SpeechBox/" + speaker+"/"; //각 캐릭터의 폴더 경로        

        foreach (string mainCharacter in MainCharacterList)
        {
            if(speaker == mainCharacter) //화자가 메인캐릭터라면
            {
                speechPanel.transform.GetChild(1).GetComponent<RawImage>().texture = Resources.Load<Texture>(speechBoxPath + "이름");//이름 색 변경
                speechPanel.transform.GetChild(0).GetComponent<RawImage>().texture = Resources.Load<Texture>(speechBoxPath + "대화창");//채팅창 색 변경
                speechPanel.transform.GetChild(2).GetComponent<RawImage>().texture = Resources.Load<Texture>(speechBoxPath + "다음"); //다음버튼 변경
                break;
            }
            else //엑스트라나 나레이션 일 때
            {
                speechPanel.transform.GetChild(1).GetComponent<RawImage>().texture = Resources.Load<Texture>("SpeechBox/엑스트라/이름");//이름 색 변경
                speechPanel.transform.GetChild(0).GetComponent<RawImage>().texture = Resources.Load<Texture>("SpeechBox/엑스트라/대화창");//채팅창 색 변경
                speechPanel.transform.GetChild(2).GetComponent<RawImage>().texture = Resources.Load<Texture>("SpeechBox/엑스트라/다음"); //다음버튼 변경
            }
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
            if (NovelController.instance._next == true)// 대화 진행중 한번 더 입력이 들어오면 대화 강제 스킵.
            {
                textArchitect.skip = true;
                NovelController.instance._next = false;
            }
                

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