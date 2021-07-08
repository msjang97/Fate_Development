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

        text1.text = "";
        text1.text += preText;

        //text1.ForceMeshUpdate();
        //TMP_Text inf = text1.textInfo;
        //int vis = inf.characterCount;

        text1.text += targetText;

        //text1.ForceMeshUpdate();
        //inf = text1.textInfo;
        //int max = inf.characterCount;

        //text1.maxVisibleCharacters = vis;

       /* while (vis < max)
        {
            //allow skipping by increasing the characters per frame and the speed of occurance.
            if (skip)
            {
                speed = 1;
                charactersPerFrame = charactersPerFrame < 5 ? 5 : charactersPerFrame + 3;
            }

            //reveal a certain number of characters per frame.
            while (runsThisFrame < charactersPerFrame)
            {
                //vis++;
                //text1.maxVisibleCharacters = vis;
                runsThisFrame++;
            }

            //wait for the next available revelation time.
            runsThisFrame = 0;
            yield return new WaitForSeconds(0.01f * speed);
        }*/



        if (skip)
        {
            speed = 1;
            charactersPerFrame = charactersPerFrame < 5 ? 5 : charactersPerFrame + 3;
        }

        //reveal a certain number of characters per frame.
        while (runsThisFrame < charactersPerFrame)
        {
            //vis++;
            //text1.maxVisibleCharacters = vis;
            runsThisFrame++;
        }

        //wait for the next available revelation time.
        runsThisFrame = 0;
        yield return new WaitForSeconds(0.01f * speed);




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
        //text1.maxVisibleCharacters = text1.text.Length;
        Terminate();
    }
}
