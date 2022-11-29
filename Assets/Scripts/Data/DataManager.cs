using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private string path;

    public Data data { get; private set; }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        path = Application.persistentDataPath + "/Data.dat";
        LoadData();
    }

    public void SetName(string name)
    {
        data.DisplayName = name;
        Events.Name.Invoke(name);
    }
    public void SetScore(int score)
    {
        if (score > data.Highscore)
        {
            data.Highscore = score;
            LeadPlayfab.SubmitScore(score);
        }
    }

    private void LoadData()
    {
        if (!File.Exists(path))
        {
            data = new Data();
            return;
        }

        data = JsonConvert.DeserializeObject<Data>(File.ReadAllText(path));
        Events.Name.Invoke(data.DisplayName);
    }

    private void SaveData()
    {
        File.WriteAllText(path, JsonConvert.SerializeObject(data));
    }

    public class Data
    {
        public string DisplayName;
        public int Highscore;

        public Data()
        {
            DisplayName = null;
            Highscore = 0;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
