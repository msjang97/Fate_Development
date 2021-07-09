using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    private static SaveData instance = null;
    public static SaveData P_instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private string _chapterName;
    public string ChapterName
    {
        get { return _chapterName; }
        set { _chapterName = value; }
    }

    private int _savedChapterProgress;
    public int SavedChapterProgress
    {
        get { return _savedChapterProgress; }
        set { _savedChapterProgress = value; }
    }

    private int _savedBackgroundLine;
    public int SavedBackgroundLine
    {
        get { return _savedBackgroundLine; }
        set { _savedBackgroundLine = value; }
    }

    private string _savedPlaySong;
    public string SavedPlaySong
    {
        get { return _savedPlaySong; }
        set { _savedPlaySong = value; }
    }

    private bool _isLoadData;
    public bool isLoadData
    {
        get { return _isLoadData; }
        set { _isLoadData = value; }
    }

    private Dictionary<string, bool> _endingCollection;
    public Dictionary<string, bool> EndingCollection
    {
        get { return _endingCollection; }
    }

    string dataPath;
    string endingDataPath;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        dataPath = Application.dataPath + "/05.SaveData/SaveData.txt";

        FileInfo dataFile = new FileInfo(dataPath);
        if (!dataFile.Exists)
        {
            SaveGame("Chapter0_start", 0, 0, "Title");
        }

        _chapterName = "";
        _savedChapterProgress = 0;
        _isLoadData = false;

        endingDataPath = Application.dataPath + "/05.SaveData/EndingData.txt";

        SaveAndLoadEndingData("");
        
    }


    public void SaveGame(string chapterName, int chapterProgress, int backgroundLine, string playSong)
    {
        _chapterName = chapterName;
        _savedChapterProgress = chapterProgress;
        _savedBackgroundLine = backgroundLine;
        _savedPlaySong = playSong;

        FileInfo dataFile = new FileInfo(dataPath);

        FileStream fs = dataFile.Create();
        TextWriter tw = new StreamWriter(fs);
        tw.Write("ChapterName : " + _chapterName + "\n");
        tw.Write("ChapterProgress : " + _savedChapterProgress + "\n");
        tw.Write("SavedBackgroundLine : " + _savedBackgroundLine + "\n");
        tw.Write("SavedPlaySong : " + _savedPlaySong + "\n");

        tw.Write("ch_count : " + LovePoint.instance.ch_count + "\n");
        tw.Write("eunji_LovePoint : " + LovePoint.instance.eunji_LovePoint + "\n");
        tw.Write("arin_LovePoint : " + LovePoint.instance.arin_LovePoint + "\n");
        tw.Write("minseok_LovePoint : " + LovePoint.instance.minseok_LovePoint + "\n");
        tw.Write("junbyeong_LovePoint : " + LovePoint.instance.junbyeong_LovePoint + "\n");

        tw.Close();
        fs.Close();

        Debug.Log(_chapterName + ", line : " + _savedChapterProgress + " lastBackground : " + _savedBackgroundLine + ", " + _savedPlaySong);

        // 추가 작성
        // File.AppendAllText(Application.dataPath + "/SaveData.txt", "  !@#추가 내용");
    }

    public void LoadGame()
    {
        if (File.Exists(dataPath))
        {
            string[] loadData = File.ReadAllLines(dataPath);
            for (int i = 0; i < loadData.Length; i++)
            {
                string[] data = null;
                if (loadData[i].StartsWith("ChapterName : "))
                {
                    // 배열의 3번째에 정보가 들어감.
                    data = loadData[i].Split(' ', '\n');
                    _chapterName = data[2];
                }
                else if (loadData[i].StartsWith("ChapterProgress : "))
                {
                    data = loadData[i].Split(' ', '\n');
                    _savedChapterProgress = int.Parse(data[2]);
                }
                else if (loadData[i].StartsWith("SavedBackgroundLine : "))
                {
                    data = loadData[i].Split(' ', '\n');
                    _savedBackgroundLine = int.Parse(data[2]);
                }
                else if (loadData[i].StartsWith("SavedPlaySong : "))
                {
                    data = loadData[i].Split(' ', '\n');
                    _savedPlaySong = data[2];
                }
                else if (loadData[i].StartsWith("ch_count : "))
                {
                    data = loadData[i].Split(' ', '\n');
                    LovePoint.instance.ch_count = int.Parse(data[2]);
                }
                else if (loadData[i].StartsWith("eunji_LovePoint : "))
                {
                    data = loadData[i].Split(' ', '\n');
                    LovePoint.instance.eunji_LovePoint = int.Parse(data[2]);
                }
                else if (loadData[i].StartsWith("arin_LovePoint : "))
                {
                    data = loadData[i].Split(' ', '\n');
                    LovePoint.instance.arin_LovePoint = int.Parse(data[2]);
                }
                else if (loadData[i].StartsWith("minseok_LovePoint : "))
                {
                    data = loadData[i].Split(' ', '\n');
                    LovePoint.instance.minseok_LovePoint = int.Parse(data[2]);
                }
                else if (loadData[i].StartsWith("junbyeong_LovePoint : "))
                {
                    data = loadData[i].Split(' ', '\n');
                    LovePoint.instance.junbyeong_LovePoint = int.Parse(data[2]);
                }
            }

            Debug.Log("Load!" + _chapterName + ", line : " + _savedChapterProgress + " lastBackground : " + _savedBackgroundLine + ", " + _savedPlaySong);
        }
        else
        {
            string errorMessage = "ERR! File " + dataPath + " does not exist!";
            Debug.LogError(errorMessage);
        }
    }

    public void SaveAndLoadEndingData(string endingName)
    {
        _endingCollection = new Dictionary<string, bool>();

        if (File.Exists(endingDataPath))
        {
            string[] loadData = File.ReadAllLines(endingDataPath);
            for (int i = 0; i < loadData.Length; i++)
            {
                string[] data = null;
                // 딕셔너리에 데이터 추가.
                data = loadData[i].Split('\n');
                _endingCollection.Add(data[0], true);
            }
        }
        else
        {
            string message = "File " + dataPath + " does not exist!";
            Debug.Log(message);
        }

        if (!_endingCollection.ContainsKey(endingName))
            _endingCollection.Add(endingName, true);

        FileInfo dataFile = new FileInfo(endingDataPath);

        FileStream fs = dataFile.Create();
        TextWriter tw = new StreamWriter(fs);

        foreach (var ending in _endingCollection)
            if (ending.Key != "")
                tw.Write(ending.Key + "\n");

        tw.Close();
        fs.Close();

    }
}
