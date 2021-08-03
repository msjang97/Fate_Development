using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class ForJsonGameData
{
    public string _chapterName;
    public int _savedChapterProgress;
    public int _savedBackgroundLine;
    public string _savedPlaySong;

    public int ch_count;
    public int eunji_LovePoint;
    public int arin_LovePoint;
    public int minseok_LovePoint;
    public int junbyeong_LovePoint;
}

[Serializable]
public class EndingCollectionData
{
    public List<string> _endingCollection;

}

[Serializable]
public class SettingData
{
    public float _BGM_volume;
    public float _SFX_volume;

}

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

    public ForJsonGameData _gameData;
    public EndingCollectionData _endingCollectionData;
    public SettingData _settingData;

    private bool _isLoadData;
    public bool isLoadData
    {
        get { return _isLoadData; }
        set { _isLoadData = value; }
    }

    string dataPath;
    string endingDataPath;
    string settingDataPath;

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

        _isLoadData = false;

        _gameData = new ForJsonGameData();
        _endingCollectionData = new EndingCollectionData();
        _settingData = new SettingData();

        dataPath = Application.persistentDataPath + "GameData.json";
        endingDataPath = Application.persistentDataPath + "EndingData.json";
        settingDataPath = Application.persistentDataPath + "settingData.json";

        FileInfo dataFile = new FileInfo(dataPath);
        if (!dataFile.Exists)
        {
            SaveGame("Chapter0_start", 0, 0, "Title");
        }

        LoadSettingData();
        SaveAndLoadEndingData("");
    }


    public void SaveGame(string chapterName, int chapterProgress, int backgroundLine, string playSong)
    {
        _gameData._chapterName = chapterName;
        _gameData._savedChapterProgress = chapterProgress;
        _gameData._savedBackgroundLine = backgroundLine;
        _gameData._savedPlaySong = playSong;

        string ToJsonData = JsonUtility.ToJson(_gameData);
        File.WriteAllText(dataPath, ToJsonData);

        Debug.Log(_gameData._chapterName + ", line : " + _gameData._savedChapterProgress + " lastBackground : " + _gameData._savedBackgroundLine + ", " + _gameData._savedPlaySong);

    }

    public void LoadGame()
    {
        if (File.Exists(dataPath))
        {
            string loadData = File.ReadAllText(dataPath);
            _gameData = JsonUtility.FromJson<ForJsonGameData>(loadData);

            Debug.Log("Load!" + _gameData._chapterName + ", line : " + _gameData._savedChapterProgress + " lastBackground : " + _gameData._savedBackgroundLine + ", " + _gameData._savedPlaySong);
        }
        else
        {
            string errorMessage = "ERR! File " + dataPath + " does not exist!";
            Debug.LogError(errorMessage);
        }
    }

    public void SaveAndLoadEndingData(string endingName)
    {
        if (File.Exists(endingDataPath))
        {
            string loadData = File.ReadAllText(endingDataPath);
            _endingCollectionData = JsonUtility.FromJson<EndingCollectionData>(loadData);
        }
        else
        {
            _endingCollectionData._endingCollection = new List<string>();

            string message = "File " + endingDataPath + " does not exist!";
            Debug.Log(message);
        }

        if (!_endingCollectionData._endingCollection.Contains(endingName))
            _endingCollectionData._endingCollection.Add(endingName);

        string ToJsonData = JsonUtility.ToJson(_endingCollectionData);
        File.WriteAllText(endingDataPath, ToJsonData);
    }

    public void SaveSettingData(float BGM_volume, float SFX_volume)
    {
        _settingData._BGM_volume = BGM_volume;
        _settingData._SFX_volume = SFX_volume;

        string ToJsonData = JsonUtility.ToJson(_settingData);
        File.WriteAllText(settingDataPath, ToJsonData);

        Debug.Log("BGM : " + _settingData._BGM_volume + " SFX : " + _settingData._SFX_volume);
    }

    public void LoadSettingData()
    {
        if (File.Exists(settingDataPath))
        {
            string loadData = File.ReadAllText(settingDataPath);
            _settingData = JsonUtility.FromJson<SettingData>(loadData);

            Debug.Log("Load BGM : " + _settingData._BGM_volume + " SFX : " + _settingData._SFX_volume);
        }
        else
        {
            SaveSettingData(0.5f, 0.5f);

            string errorMessage = "ERR! setting data File " + settingDataPath + " does not exist!";
            Debug.LogError(errorMessage);
        }
    }
}
