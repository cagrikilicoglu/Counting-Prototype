using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance;
    public string nameString;
    public string highScoreUser;
    public int highScore;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadHighScore();
    }

    

    class HighScore
    {
        public int highScorePoint;
        public string highScoreUsername;
    }

    // load the latest high score at the beginning of the game from json
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            HighScore highScoreData = JsonUtility.FromJson<HighScore>(json);
            highScore = highScoreData.highScorePoint;
            highScoreUser = highScoreData.highScoreUsername;
        }
    }

    // save the score it is higher than the latest high score

    public void SaveHighScore()
    {
        HighScore highScoreData = new HighScore();
        highScoreData.highScorePoint = highScore;
        highScoreData.highScoreUsername = highScoreUser;
        string json = JsonUtility.ToJson(highScoreData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
}
