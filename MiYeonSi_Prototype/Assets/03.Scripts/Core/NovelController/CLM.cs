using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLM : MonoBehaviour
{
    public static LINE Interpret(string rawLine)
    {
        return new LINE(rawLine);
    }

    public class LINE
    {
        /// <summary>
        /// Who is speaking on this line.
        /// </summary>
        public string speaker = "";
        public static Character curCharacter;

        /// <summary>
        /// The segments on this line that make up each piece of dialogue.
        /// </summary>
        public List<SEGMENT> segments = new List<SEGMENT>();
        /// <summary>
        /// All the actions to ve called during or at the end of this line.
        /// </summary>
        public List<string> actions = new List<string>();

        public LINE(string rawLine)
        {
            string[] dialogueAndActions = rawLine.Split('"');
            char actionSplitter = ' ';
            string[] actionsArr = dialogueAndActions.Length == 3 ? dialogueAndActions[2].Split(actionSplitter) : dialogueAndActions[0].Split(actionSplitter); //액션 저장

            if(dialogueAndActions.Length == 3) //contains dialogue
            {                
                speaker = dialogueAndActions[0] == "" ? NovelController.instance.cachedLastSpeaker : dialogueAndActions[0]; //화자 이름이 안써있을 시는 cachedLastSpeaker(전에 말하던 화자)를 speaker에 대입.
                if (speaker[speaker.Length - 1] == ' ') //화자 이름 뒤에 공백 없애주기. 
                    speaker = speaker.Remove(speaker.Length - 1); 

                //cache the speaker
                NovelController.instance.cachedLastSpeaker = speaker; //NovelController에 새로운 화자 이름을 업데이트 해줌.

                //segment the dialogue.
                SegmentDialogue(dialogueAndActions[1]); 
            }
            //now handle actions
            for(int i = 0; i< actionsArr.Length; i++)
            {
                actions.Add(actionsArr[i]);
            }
            //the line is now segmented, the actions are loaded, and it is ready to be used.
        }

        /// <summary>
        /// Segments a line of dialogue into a list of individual parts separated by input delays.
        /// </summary>
        void SegmentDialogue(string dialogue)
        {
            segments.Clear();
            string[] parts = dialogue.Split('{', '}');

            for(int i = 0; i < parts.Length; i++)
            {
                SEGMENT segment = new SEGMENT();
                bool isOdd = i % 2 != 0;

                //commands are odd indexed. Dialogue is always even.
                if(isOdd)
                {
                    //commands and data are split by spaces
                    string[] commandData = parts[i].Split(' ');
                    switch (commandData[0])
                    {
                        case "c": //wait for input and clear.
                            segment.trigger = SEGMENT.TRIGGER.waitClickClear;
                            break;
                        case "a": //wait for input and append.
                            segment.trigger = SEGMENT.TRIGGER.waitClick;
                            //appending requires fetching the text of the previous segment to be the pre text
                            segment.pretext = segments.Count > 0 ? segments[segments.Count - 1].dialogue : "";
                            break;
                        case "w": //wait for set time and clear.
                            segment.trigger = SEGMENT.TRIGGER.autoDelay;
                            segment.autoDelay = float.Parse(commandData[1]);
                            break;
                        case "wa": //wait for set time and append.
                            segment.trigger = SEGMENT.TRIGGER.autoDelay;
                            segment.autoDelay = float.Parse(commandData[1]);
                            //appending requires fetching the text of the previous segment to be the pre text
                            segment.pretext = segments.Count > 0 ? segments[segments.Count - 1].dialogue : "";
                            break;
                    }
                    i++; //invrement so we move past the command and to the next bit of dialogue.
                }

                segment.dialogue = parts[i];
                segment.line = this;

                segments.Add(segment);
            }
        }

        public class SEGMENT
        {
            public LINE line;
            public string dialogue = "";
            public string pretext = "";
            public enum TRIGGER { waitClick, waitClickClear, autoDelay}
            public TRIGGER trigger = TRIGGER.waitClickClear;

            public float autoDelay = 0;

            public void Run()
            {
                if (running != null)
                    NovelController.instance.StopCoroutine(running);
                running = NovelController.instance.StartCoroutine(Running());
            }

            public bool isRunning { get { return running != null; } }
            Coroutine running = null;
            public TextArchitect architect = null;
            IEnumerator Running()
            {
                if(line.speaker != "narrator")
                {
                    if(curCharacter != null && line.speaker != curCharacter.characterName)
                    {
                        curCharacter.FadeOut(100, false); //방금까지 말하던 사람 지워주기.
                    }
                    Character character = CharacterManager.instance.GetCharacter(line.speaker);
                    curCharacter = character;
                    character.FadeIn(100, false); //새로운 사람 FadeIn.
                    character.Say(dialogue, pretext != "");
                }
                else
                {
                    curCharacter.FadeOut(100, false);
                    DialogueSystem.instance.Say(dialogue, line.speaker, pretext != "");
                }

                //yield while the dialogue system's architect is constructing the dialogue.
                architect = DialogueSystem.instance.currentArchitect;

                while (architect.isConstructing)
                    yield return new WaitForEndOfFrame();

                running = null;
            }

            private void ChangeDialogueColor()
            {
                if(line.speaker  == "마민석")
                {

                }
                else if (line.speaker == "사수진")
                {

                }
                else if (line.speaker == "안하경")
                {

                }
                else if (line.speaker == "연애세포")
                {

                }
                else if (line.speaker == "지찬우")
                {

                }
                else if (line.speaker == "최은지")
                {

                }
                else //엑스트라나 나레이션 일 때
                {

                }
            }

            public void ForceFinish()
            {
                if (running != null)
                    NovelController.instance.StopCoroutine(running);
                running = null;

                if (architect != null)
                    architect.ForceFinish();
            }
        }
    }
}
