using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDataCarrier : MonoBehaviour
{
    public static LevelDataCarrier Instance {  get; private set; }

    public int levelNum;
    // Start is called before the first frame update
    void Awake()
    {
      if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        levelNum = -1;
    }

    public string GetLevelFilePath()
    {
        string folderPath = Path.Combine(Application.streamingAssetsPath, "Levels");
        string filePath = Path.Combine(folderPath, "level" + levelNum.ToString() + ".json");
        return filePath;
    }
}
