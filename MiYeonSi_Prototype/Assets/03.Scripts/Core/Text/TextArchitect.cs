using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextArchitect
{
    /// <summary>A dictionary keeping tabs on all architects present in a scene. Prevents multiple architects from influencing the same text object simultaneously.</summary>
    private static Dictionary<Text, TextArchitect> activeArchitects = new Dictionary<Text, TextArchitect>();

    private string preText;
    private string targetText;

    private int charactersPerFrame = 1;
    private float speed = 1f;

    public bool skip = false;
    public static bool skip2 = false;

    public bool isConstructing { get { return buildProcess != null; } }
    Coroutine buildProcess = null;

    Text text1;

    public TextArchitect(Text textA, string targetText, string preText = "", int charactersPerFrame = 1, float speed = 1f)
    {
        this.text1 = textA;
        this.targetText = targetText;
        this.preText = preText;
        this.charactersPerFrame = charactersPerFrame;
        this.speed = Mathf.Clamp(speed, 1f, 300f);

        Initiate();
    }

    public void Stop()
    {
        if (isConstructing)
        {
            DialogueSystem.instance.StopCoroutine(buildProcess);
        }
        buildProcess = null;
    }


    IEnumerator Construction()
    {
        int runsThisFrame = 0;

        text1.text = ""; //텍스트 초기화
        text1.text += preText;

        int vis = text1.text.Length;

        /*
        if (!LovePoint.instance._skip ) //준병선배가 추가한 부분(오류 수정 실패 시 백업용)
        {
            //yield return new WaitForSeconds(0.5f);
            for (int i = 0; i <= targetText.Length; i++)
            {
                text1.text = targetText.Substring(0, i);
                if (!LovePoint.instance._skip)
                    yield return new WaitForSeconds(0.15f);
                else if (LovePoint.instance._skip && LovePoint.instance._next)
                {
                    yield return new WaitForSeconds(0f);
                   
                }
            }
            LovePoint.instance._next = false;
            LovePoint.instance._skip = false;
            //LovePoint.instance._next = false;
            
        }
        if (LovePoint.instance._isAfterMiniGame)
            LovePoint.instance._choiceNext = true;*/

        int max = targetText.Length; //최대 문장길이를 저장.

        //text1 = maxVisibleCharacters;

        while (vis < max) //max변수 : 현재 문장 길이  vis변수 : 현재 출력가능한 문장 길이
        {
            //allow skipping by increasing the characters per frame and the speed of occurance.
            if (skip)
            {
                //speed = 0.1f;
                text1.text += targetText;
                skip = false;
                break;
                //charactersPerFrame = charactersPerFrame < 5 ? 5 : charactersPerFrame + 3;
            }

            //reveal a certain number of characters per frame.
            while (runsThisFrame < charactersPerFrame) //여기가 한글자씩 나오게 하는 부분
            {
                vis++;
                text1.text = targetText.Substring(0, vis);
                runsThisFrame++;
            }

            runsThisFrame = 0; //한글자 출력하기 전 == 1; 한글자 출력 완료후 다시 0으로 초기화됨.
            yield return new WaitForSeconds(0.1f * speed); //글자 속도 조절 하는 부분
        }

        //terminate the architect and remove it from the active log of architects.
        Terminate();
    }

    void Initiate()
    {
        //check if an architect for this text object is already running. if it is, terminate it. Do not allow more than one architect to affect the same text object at once.
        TextArchitect existingArchitect = null;
        if (activeArchitects.TryGetValue(text1, out existingArchitect))
            existingArchitect.Terminate();

        buildProcess = DialogueSystem.instance.StartCoroutine(Construction());
        activeArchitects.Add(text1, this);
    }


    /// <summary>
    /// Terminate this architect. Stops the text generation process and removes it from the cache of all active architects.
    /// </summary>
    public void Terminate()
    {
        activeArchitects.Remove(text1);
        if (isConstructing)
            DialogueSystem.instance.StopCoroutine(buildProcess);
        buildProcess = null;
    }


    //Force the architext to finish the text right now.

    public void ForceFinish()
    {
        Terminate();
    }
}
