using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceScreen : MonoBehaviour
{
    public static ChoiceScreen instance;
    public GameObject root;
    public ChoiceButton choicePrefab;

    static List<ChoiceButton> choices = new List<ChoiceButton>();

    void Awake()
    {
        instance = this;
        Hide();
    }

    public static void Show( params string[] choices )
    {
        instance.root.SetActive(true);

        if (isShowingChoices)
            instance.StopCoroutine(showingChoices);

        ClearAllCurrentChoices();

        showingChoices = instance.StartCoroutine(ShowingChoices(choices));
    }

    public static void Hide()
    {
        if (isShowingChoices)
            instance.StopCoroutine(showingChoices);
        showingChoices = null;

        ClearAllCurrentChoices();

        instance.root.SetActive(false);
    }

    static void ClearAllCurrentChoices()
    {
        foreach(ChoiceButton b in choices)
        {
            DestroyImmediate(b.gameObject);
        }
        choices.Clear();
    }

    public static bool isWaitingForChoiceToBeMade { get { return isShowingChoices && !lastChoiceMade.hasBeenMade; } }
    public static bool isShowingChoices { get { return showingChoices != null; } } 

    static Coroutine showingChoices = null;
    public static IEnumerator ShowingChoices(string[] choices)
    {
        yield return new WaitForEndOfFrame(); 
        lastChoiceMade.Reset();

        for(int i = 0; i < choices.Length; i++)
        {
            CreateChoice(choices[i].Split('"')[1]);
        }

        while (isWaitingForChoiceToBeMade)
            yield return new WaitForEndOfFrame();

        Hide();
    }

    static void CreateChoice(string choice)
    {
        GameObject ob = Instantiate(instance.choicePrefab.gameObject, instance.choicePrefab.transform.parent);
        ob.SetActive(true);
        ChoiceButton b = ob.GetComponent<ChoiceButton>();

        b.text = choice; //텍스트 삽입구간
        b.choiceIndex = choices.Count;

        choices.Add(b);
    }

    [System.Serializable]
    public class CHOICE
    {
        public bool hasBeenMade { get { return title != ""&&index != -1; } }

        public string title = "";
        public int index = -1;

        public void Reset()
        {
            title = "";
            index = -1;
        }
    }
    public CHOICE choice = new CHOICE();
    public static CHOICE lastChoiceMade { get { return instance.choice; } }

    public void MakeChoice(ChoiceButton button)
    {
        choice.index = button.choiceIndex;
        choice.title = button.text;

        if (LovePoint.instance._goBranch)
        {
            switch (choice.index)
            {
                case 0:
                    LovePoint.instance._numBranch = 1; ;
                    break;
                case 1:
                    LovePoint.instance._numBranch = 2; ;
                    break;
                case 2:
                    LovePoint.instance._numBranch = 3; ;
                    break;
               
                default:
                    break;
            }
        }
        else
        {
            switch (choice.index)
            {
                case 0:
                    LovePoint.instance.Distr_LovePoint_Cal(-5);
                    break;
                case 1:
                    LovePoint.instance.Distr_LovePoint_Cal(0);
                    break;
                case 2:
                    LovePoint.instance.Distr_LovePoint_Cal(3);
                    break;
                case 3:
                    LovePoint.instance.Distr_LovePoint_Cal(5);
                    break;
                default:
                    break;
            }
        }
        
    }

    public void MakeChoice(string choiceTitle)
    {
        foreach(ChoiceButton b in choices)
        {
            if(b.text.ToLower() == choiceTitle.ToLower())
            {
                MakeChoice(b);
                return;
            }
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if (choices.Count > choiceIndex)
            MakeChoice(choices[choiceIndex]);
    }

}
