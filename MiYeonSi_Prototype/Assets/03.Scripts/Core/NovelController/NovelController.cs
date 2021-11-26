using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class NovelController : MonoBehaviour
{
    public static NovelController instance;
    //private string txtFileName = null;
    public bool isAfterMiniGame = false;
    [HideInInspector]
    public int lastBackground;
    string _chapterName;
    string playSongName;
    float untouchableTime;
    bool isAutoNext;
    public bool next_box;
    public bool choiceNext;


    public GameObject touch_box;
    /// <summary> The lines of data loaded directly from a chapter file. /// </summary>
    public List<string> data = new List<string>();
    /// <summary> The progress in the current data list. /// </summary>

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        next_box = false;
        isAutoNext = false;
        if (SaveData.P_instance.isLoadData == true)
        {
            SaveData.P_instance.LoadGame();
            _chapterName = SaveData.P_instance._gameData._chapterName;

            LoadChapterFile(_chapterName);
            return;
        }

        if (ChoiceManager.P_instance.savedChapterName == "") //처음 시작할때만 
        {
            _chapterName = "Chapter0_start"; //Chapter0_start

            LovePoint.instance.ResetData();
        }
        else
        {
            if (LovePoint.instance.goEnd)// 엔딩이 열릴때
            {
                switch (LovePoint.instance.end_num)
                {
                    case 0:
                        _chapterName = "EE"; // 은지엔딩 EE
                        break;
                    case 1:
                        _chapterName = "JE"; //준병엔딩 JE
                        break;
                    case 2:
                        _chapterName = "AE"; //아린엔딩 AE
                        break;
                    case 3:
                        _chapterName = "ME"; // 민석엔딩 ME
                        break;
                    case 4:
                        _chapterName = "SE"; //솔로엔딩 SE
                        break;
                    default:
                        break;
                }

                LovePoint.instance.goEnd = false;
            }
            else// 엔딩이 안나올때
            {
                if (LovePoint.instance._isBranch) // 챕터4 일때 분기점
                {
                    switch (LovePoint.instance._numBranch)
                    {
                        case 1:
                            _chapterName = "Chapter4_1_start"; //4_1
                            break;
                        case 2:
                            _chapterName = "Chapter4_2_start"; // 4_2
                            break;
                        case 3:
                            _chapterName = "Chapter4_3_start";  // 4_3
                            break;
                        default:
                            break;
                    }
                }

                _chapterName = ChoiceManager.P_instance.savedChapterName;
                isAfterMiniGame = true;
            }

        }

        LoadChapterFile(_chapterName);
    }

    void Update()
    {
        if (isTouchable())
            Next();

        CoolDownManager();
    }

    void CoolDownManager()
    {
        if (untouchableTime > 0.0f)
            untouchableTime -= Time.deltaTime;
    }

    bool isTouchable()
    {
        if (untouchableTime > 0.0f)
        {
            next_box = false;
            return false;
        }

        if (next_box || isAutoNext || Input.GetKey(KeyCode.Space))
            return true;

        return false;
    }

    public bool _next = false;
    public void Next()
    {
        _next = true;
        next_box = false;
        isAutoNext = false;
    }

    static List<string> ArrayToList(string[] array, bool removeBlankLines = true)
    {
        List<string> list = new List<string>();
        for (int i = 0; i < array.Length; i++)
        {
            string s = array[i];
            if (s.Length > 0 || !removeBlankLines)
            {
                list.Add(s);
            }
        }
        return list;
    }

    public void LoadChapterFile(string fileName)
    {
        TextAsset asset = Resources.Load("story/" + fileName) as TextAsset;
        string str = asset.text;
        bool removeBlankLines = true;
        string[] str2 = str.Split('\n');
        for (int i = 0; i < str2.Length; i++)
            str2[i] = str2[i].Trim();

        List<string> lines = ArrayToList(str2, removeBlankLines);
        data = lines;

        //data = FileManager.LoadFile(FileManager.savPath + "Resources/story/" + fileName); 

        cachedLastSpeaker = "";

        if (handlingChapterFile != null)
            StopCoroutine(handlingChapterFile);
        handlingChapterFile = StartCoroutine(HandlingChapterFile());

        _chapterName = fileName;
        SaveData.P_instance._gameData._chapterName = _chapterName;

        Next();
    }

    public bool isHandlingChapterFile { get { return handlingChapterFile != null; } }

    Coroutine handlingChapterFile = null;
    [HideInInspector] public int chapterProgress = 0; //
    IEnumerator HandlingChapterFile()
    {
        chapterProgress = 0; //번호가 여기서 초기화됌.

        if (SaveData.P_instance.isLoadData == true)
        {
            SaveData.P_instance.isLoadData = false;
            chapterProgress = SaveData.P_instance._gameData._savedChapterProgress;
            lastBackground = SaveData.P_instance._gameData._savedBackgroundLine;
            playSongName = SaveData.P_instance._gameData._savedPlaySong;

            LovePoint.instance.ch_count = SaveData.P_instance._gameData.ch_count;
            LovePoint.instance.eunji_LovePoint = SaveData.P_instance._gameData.eunji_LovePoint;
            LovePoint.instance.junbyeong_LovePoint = SaveData.P_instance._gameData.junbyeong_LovePoint;
            LovePoint.instance.arin_LovePoint = SaveData.P_instance._gameData.arin_LovePoint;
            LovePoint.instance.minseok_LovePoint = SaveData.P_instance._gameData.minseok_LovePoint;

            HandleLine(data[lastBackground]);
            Command_PlayMusic(playSongName);
            if (chapterProgress != 0)
                _next = true;

            Debug.Log(chapterProgress + "" + lastBackground + playSongName);
        }

        while (chapterProgress < data.Count)
        {
            if (_next)
            {
                if (isAfterMiniGame)
                {
                    if (ChoiceManager.P_instance.actions[ChoiceManager.P_instance.selectedNum - 1].StartsWith("Load")) // 미니게임을 통한 Load 키워드 이용 시
                    {
                        isAfterMiniGame = false;
                        HandleLine(ChoiceManager.P_instance.actions[ChoiceManager.P_instance.selectedNum - 1]);

                        continue;
                    }

                    if (!LovePoint.instance._choiceNext) //미니게임 이후일 경우, 저장된 진행상황만큼 이동.
                    {
                        HandleLine(ChoiceManager.P_instance.choices[ChoiceManager.P_instance.selectedNum - 1]); //선택지 출력.

                        choiceNext = true;
                    }
                    else if (LovePoint.instance._choiceNext)
                    {
                        HandleLine(ChoiceManager.P_instance.actions[ChoiceManager.P_instance.selectedNum - 1]); //선택지에 대한 대답 출력.

                        choiceNext = false;
                        isAfterMiniGame = false;
                        LovePoint.instance._choiceNext = false;
                    }

                    while (isHandlingLine)
                        yield return new WaitForEndOfFrame();

                    LovePoint.instance._isAfterMiniGame = isAfterMiniGame; // 이게 필요한 부분인가?
                    chapterProgress = ChoiceManager.P_instance.savedChapterProgress;

                    continue;
                }

                string line = data[chapterProgress];

                //this is a choice
                if (line.StartsWith("choice"))
                {
                    yield return HandlingChoiceLine(line, null);
                    chapterProgress++;
                }

                //this is a Choice MiniGame
                else if (line.StartsWith("miniGame"))
                {
                    string[] before = data[chapterProgress - 2].Split('"');
                    ChoiceManager.P_instance.beforeMinigame = before.Length > 1 ? before[1] : before[0];
                    //ChoiceManager.P_instance.beforeMinigame = data[chapterProgress - 2].Split('"')[1];
                    ChoiceManager.P_instance.savedChapterName = _chapterName;
                    ChoiceManager.P_instance.selectedNum = 0;
                    ChoiceManager.P_instance.isMainSceneLoaded = false;
                    string[] miniGameName = null;
                    miniGameName = line.Split('(', ')');
                    yield return HandlingChoiceLine(line, miniGameName[1]); //여기서 미니게임 이름 넘겨주기. 
                    chapterProgress++;
                }

                //this is a normal line of dialogue and actions.
                else
                {
                    HandleLine(data[chapterProgress]);
                    Debug.Log(data[chapterProgress]);
                    chapterProgress++;
                    while (isHandlingLine)
                        yield return new WaitForEndOfFrame();
                }

            }
            yield return new WaitForEndOfFrame();
        }
        handlingChapterFile = null;
    }


    IEnumerator HandlingChoiceLine(string line, string miniGameName)
    {
        //string title = line.Split('"')[1];
        List<string> choices = new List<string>();
        List<string> actions = new List<string>();

        bool gatheringChoices = true;
        while (gatheringChoices)
        {
            chapterProgress++;
            line = data[chapterProgress];

            if (line.Equals("{"))
            {
                //Debug.Log("일치함");
                continue;
            }

            line = line.Replace("        ", "");

            if (line != "}")
            {
                choices.Add(line);
                actions.Add(data[chapterProgress + 1].Replace("        ", ""));
                chapterProgress++;
            }
            else
            {
                gatheringChoices = false;
            }
        }

        ChoiceManager.P_instance.savedChapterProgress = chapterProgress + 1; //싱글턴에 진행상황 저장하고 다른씬으로 넘어가기

        //display choices
        if (choices.Count > 0 && miniGameName == null)
        {
            ChoiceScreen.Show(choices.ToArray()); yield return new WaitForEndOfFrame(); //선택지 만드는중.
            while (ChoiceScreen.isWaitingForChoiceToBeMade)
                yield return new WaitForEndOfFrame();

            //choice is made. execute the paired action.
            string action = actions[ChoiceScreen.lastChoiceMade.index]; //선택된 선택지에 대한 대답 출력.
            HandleLine(action);

            while (isHandlingLine)
                yield return new WaitForEndOfFrame();
        }
        else //일반 선택지가 아니라면
        {
            ChoiceManager.P_instance.choices = choices; //선택지 텍스트 저장.
            ChoiceManager.P_instance.actions = actions; //선택지 대답 저장.

            SceneManager.LoadScene(miniGameName); // 각 미니게임 호출해주기.
        }

        //chapterProgress++;
    }

    void HandleLine(string rawLine)
    {
        //Debug.Log(rawLine);
        CLM.LINE line = CLM.Interpret(rawLine);
        StopHandlingLine();
        handlingLine = StartCoroutine(HandlingLine(line));

    }

    void StopHandlingLine()
    {
        if (isHandlingLine)
            StopCoroutine(handlingLine);
        handlingLine = null;
    }

    [HideInInspector]
    //Used as a fallback when no speaker is given.
    public string cachedLastSpeaker = "";

    public bool isHandlingLine { get { return handlingLine != null; } }
    Coroutine handlingLine = null;
    IEnumerator HandlingLine(CLM.LINE line)
    {
        _next = false;

        //Before Line is Started. Handle all the actions set at the end of the line.
        for (int i = 0; i < line.actions.Count; i++)
        {
            HandleAction(line.actions[i]);
        }

        int lineProgress = 0; //progress through the segments of a line.

        while (lineProgress < line.segments.Count)
        {
            _next = false; //reset at the start of each loop.
            CLM.LINE.SEGMENT segment = line.segments[lineProgress];

            //always run the first segment automatically. But wait for the trigger on all proceding segments.
            if (lineProgress > 0)
            {
                if (segment.trigger == CLM.LINE.SEGMENT.TRIGGER.autoDelay)
                {
                    for (float timer = segment.autoDelay; timer >= 0; timer -= Time.deltaTime)
                    {
                        yield return new WaitForEndOfFrame();
                        if (_next)
                            break; //allow the termination of a delay when "next" is triggered. Prevents unskippable wait timers.
                    }
                }
                else
                {
                    while (!_next)
                        yield return new WaitForEndOfFrame(); //wait untill the player says move to the next segment.
                }
            }
            _next = false; // next could have been triggered during an event above.

            //the segment now needs to build and run.
            segment.Run();

            while (segment.isRunning) //만약 말하는 중인데 인풋이 또 들어왔다면
            {
                yield return new WaitForEndOfFrame();
                //allow for auto completion of the current segment for skipping purposes.
                if (_next)
                {
                    //rapidly complete the text on first advance, force it to finish on the second.
                    if (!segment.architect.skip) //skip이 false일때 
                    {
                        segment.architect.skip = true; //skip을 true로 바꿔줌.
                    }
                    else //skip이 true일때
                    {
                        //segment.ForceFinish(); //대화 강제 종료
                    }

                }
            }

            lineProgress++;

            yield return new WaitForEndOfFrame();
        }

        //Line is finished. Handle all the actions set at the end of the line.
        for (int i = 0; i < line.actions.Count; i++)
        {
            HandleAfterAction(line.actions[i]);
        }

        handlingLine = null;
    }

    //ACTIONS
    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void HandleAction(string action)
    {
        print("Handle action [" + action + "]");
        string[] data = action.Split('(', ')');

        switch (data[0])
        {
            case "enter":
                Command_Enter(data[1]);
                break;
            case "exit":
                Command_Exit(data[1]);
                break;
            case "setBackground":
                Command_SetLayerImage(data[1], BCFC.instance.background);
                Next(); // 배경 전환되면서 같이 전환.
                break;
            case "setMiniGameAfterBG":
                Command_SetLayerImage(data[1], BCFC.instance.background);
                lastBackground = chapterProgress;
                SaveData.P_instance.SaveGame(_chapterName, chapterProgress, lastBackground, playSongName);
                Next(); // 배경 전환되면서 같이 전환.
                break;
            case "setCinematic":
                Command_SetLayerImage(data[1], BCFC.instance.cinematic);
                break;
            case "setForeground":
                Command_SetLayerImage(data[1], BCFC.instance.foreground);
                untouchableTime = 2.0f;
                break;
            case "playSound":
                Command_PlaySound(data[1]);
                break;
            case "playMusic":
                Command_PlayMusic(data[1]);
                Next(); // 배경음 전환되면서 같이 전환.
                break;
            case "move":
                Command_MoveCharacter(data[1]);
                break;
            case "setPosition":
                Command_SetPosition(data[1]);
                break;
            case "changeExpression":
                Command_ChangeExpression(data[1]);
                break;
            case "Load":
                Command_Load(data[1]);
                break;
            case "removeForeground":
                Command_removeForeground(data[1]);
                break;
            case "countEnd":
                Choice_EndingScene_R();
                break;
            case "goEnding":
                Go_SelectEnding();
                break;
            case "goEndingCredit":
                Go_EndingCredit();
                break;
            case "distr":
                Command_Distr(data[1]);
                break;
            case "goBranch":// 챕터 4 분기점 묻는 초이스전에 대본추가
                Go_Branch();
                break;
            case "nextBranch"://챕터 4 대본 마지막 추가
                Next_Branch();
                break;

        }
    }
    public void HandleAfterAction(string action)
    {
        print("Handle After action [" + action + "]");
        string[] data = action.Split('(', ')');

        switch (data[0])
        {
            case "autoNextPassage":
                Command_AutoNextPassage(data[1]);
                break;
            case "setGotoStartScene":
                Command_goToStartScene();
                break;
            case "setGotoStartScene2":
                Command_goToStartScene2();
                break;
        }
    }

    void Command_AutoNextPassage(string data)
    {
        float delayTime = (data != "" ? float.Parse(data) : 0);

        isAutoNext = true;
        untouchableTime = delayTime;
    }

    void Go_start()
    {
        SceneManager.LoadScene("StartScene");
    }

    void Go_EndingCredit()
    {
        SceneManager.LoadScene("EndingCredit");
    }

    void Go_SelectEnding()
    {
        LovePoint.instance.goEnd = true;
        SceneManager.LoadScene("MainSystem");
    }

    void Command_Distr(string distr)
    {
        switch (distr)
        {
            case "문준병":
                LovePoint.instance.ch_count = 1;
                break;
            case "고아린":
                LovePoint.instance.ch_count = 2;
                break;
            case "마민석":
                LovePoint.instance.ch_count = 3;
                break;
            default:
                break;
        }

    }

    void Go_Branch()
    {
        LovePoint.instance._goBranch = true;
    }
    void Next_Branch()
    {
        LovePoint.instance._isBranch = true;
        SceneManager.LoadScene("MainSystem");
    }
    void Command_goToStartScene2()
    {
        touch_box.SetActive(false);
        AudioManager.instance.StopSong();
        Invoke("Go_start", 1.5f);
    }

    void Command_goToStartScene()
    {
        AudioManager.instance.StopSong();
        Go_start();
    }

    void Command_Load(string chapterName)
    {
        //LovePoint.instance.ch_count++;
        NovelController.instance.LoadChapterFile(chapterName);
        HandleLine(data[0]);
        SaveData.P_instance.SaveGame(_chapterName, 0, 0, playSongName);
    }
    public void Choice_EndingScene_R()
    {
        string EndingName = "";


        if (LovePoint.instance.eunji_LovePoint >= 85) // 은지호감도 85이상 일때 은지 엔딩
        {
            LovePoint.instance.end_num = 0; //eunji
            EndingName = "Enji_Ending";
        }
        else
        {
            int higherPoint = LovePoint.instance.junbyeong_LovePoint; // 준병호감도를 하이포인트로 임의지정

            if (higherPoint < LovePoint.instance.arin_LovePoint) // 
            {
                higherPoint = LovePoint.instance.arin_LovePoint;

                if (higherPoint < LovePoint.instance.minseok_LovePoint)
                {
                    higherPoint = LovePoint.instance.minseok_LovePoint;
                    LovePoint.instance.end_num = 3; // minseok
                    EndingName = "Minseok_Ending";
                }
                else if (higherPoint > LovePoint.instance.minseok_LovePoint)
                {
                    higherPoint = LovePoint.instance.arin_LovePoint;
                    LovePoint.instance.end_num = 2; // arin
                    EndingName = "Hakyung_Ending";
                }

                else if (higherPoint == LovePoint.instance.minseok_LovePoint)
                {
                    LovePoint.instance.end_num = 4; // solo
                    EndingName = "Solo_Ending";
                }
            }

            else if (higherPoint > LovePoint.instance.arin_LovePoint)
            {
                if (higherPoint > LovePoint.instance.minseok_LovePoint)
                {
                    higherPoint = LovePoint.instance.junbyeong_LovePoint;
                    LovePoint.instance.end_num = 1; // junbyeong
                    EndingName = "Sujin_Ending";
                }
                else if (higherPoint < LovePoint.instance.minseok_LovePoint)
                {
                    higherPoint = LovePoint.instance.minseok_LovePoint;
                    LovePoint.instance.end_num = 3; // minseok
                    EndingName = "Minseok_Ending";
                }

                else if (higherPoint == LovePoint.instance.minseok_LovePoint)
                {
                    LovePoint.instance.end_num = 4; // solo
                    EndingName = "Solo_Ending";
                }
            }

            else if (higherPoint == LovePoint.instance.arin_LovePoint)
            {
                if (higherPoint >= LovePoint.instance.minseok_LovePoint)
                {
                    LovePoint.instance.end_num = 4; // solo
                    EndingName = "Solo_Ending";
                }
                else
                {
                    higherPoint = LovePoint.instance.minseok_LovePoint;
                    LovePoint.instance.end_num = 3; // minseok
                    EndingName = "Minseok_Ending";
                }
            }
        }

        SaveData.P_instance.SaveAndLoadEndingData(EndingName);
        Next();
    }

    void ILLuCon(string s)
    {
        SaveData.P_instance.SaveAndLoadEndingData(s);
    }

    void Command_removeForeground(string data)
    {

        string texName = data;
        Texture2D tex = texName == "null" ? null : Resources.Load("Images/UI/Backdrops/" + texName) as Texture2D;
        BCFC.instance.foreground.RemoveActiveImage(tex);
        Next();
    }

    void Command_SetLayerImage(string data, BCFC.LAYER layer)
    {
        string texName = data.Contains(",") ? data.Split(',')[0] : data;
        //Debug.Log(texName);
        ILLuCon(texName);
        Texture2D tex = texName == "null" ? null : Resources.Load("Images/UI/Backdrops/" + texName) as Texture2D;
        float spd = 2f;
        bool smooth = false;

        if (data.Contains(","))
        {
            string[] parameters = data.Split(',');
            foreach (string p in parameters)
            {
                float fVal = 0;
                bool bVal = false;
                if (float.TryParse(p, out fVal))
                {
                    spd = fVal; continue;
                }
                if (bool.TryParse(p, out bVal))
                {
                    smooth = bVal; continue;
                }
            }
        }
        layer.TransitionToTexture(tex, spd, smooth);
    }

    void Command_PlaySound(string data)
    {
        AudioClip clip = Resources.Load("Audio/SFX/" + data) as AudioClip;

        if (clip != null)
            AudioManager.instance.PlaySFX(clip);
        else
            Debug.LogError("Clip does not exist - " + data);
    }

    void Command_PlayMusic(string data)
    {
        AudioClip clip = Resources.Load("Audio/Music/" + data) as AudioClip;

        if (clip != null)
        {
            AudioManager.instance.PlaySong(clip);
        }
        else
            Debug.LogError("Clip does not exist - " + data);

        playSongName = data;
    }

    void Command_MoveCharacter(string data)
    {
        string[] parameters = data.Split(',');
        string character = parameters[0];
        float locationX = float.Parse(parameters[1]);
        float locationY = float.Parse(parameters[2]);
        float speed = parameters.Length >= 4 ? float.Parse(parameters[3]) : 1f;
        bool smooth = parameters.Length == 5 ? bool.Parse(parameters[4]) : true;

        Character c = CharacterManager.instance.GetCharacter(character);
        c.MoveTo(new Vector2(locationX, locationY), speed, smooth);
    }
    void Command_SetPosition(string data)
    {
        string[] parameters = data.Split(',');
        string character = parameters[0];
        float locationX = float.Parse(parameters[1]);
        float locationY = float.Parse(parameters[2]);

        Character c = CharacterManager.instance.GetCharacter(character);
        c.SetPosition(new Vector2(locationX, locationY));
    }
    void Command_ChangeExpression(string data) //매개변수 ( 캐릭터이름, 캐릭터파일이름, 스피드) 로 넘겨주기.
    {
        string[] parameters = data.Split(',');
        string character = parameters[0];
        string expression = parameters[1];
        float speed = parameters.Length == 3 ? float.Parse(parameters[2]) : 1f;

        Character c = CharacterManager.instance.GetCharacter(character);
        Debug.Log(expression);
        Sprite sprite = c.GetSprite(expression);
        c.TransitionBody(sprite, speed, false);
    }

    void Command_Enter(string data)
    {
        string[] parameters = data.Split(',');
        string character = parameters[0];
        float speed = 100;
        bool smooth = false;

        Character c = CharacterManager.instance.GetCharacter(character, true, false);
        c.renderers.bodyRenderer.color = new Color(1, 1, 1, 1); //알파값0은 완전한 투명
        c.enabled = true;

        c.FadeIn(speed, smooth);
    }

    void Command_Exit(string data)
    {
        string[] parameters = data.Split(',');
        string[] characters = parameters[0].Split(';');
        float speed = 100;
        bool smooth = false;

        foreach (string s in characters)
        {
            Character c = CharacterManager.instance.GetCharacter(s);
            c.FadeOut(speed, smooth);
        }
    }

}
