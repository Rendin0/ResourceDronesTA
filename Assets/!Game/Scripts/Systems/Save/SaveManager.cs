using UnityEngine;

public class SaveManager
{
    private static SaveManager _instance;
    public static SaveManager Instance
    {
        get
        {
            _instance ??= new SaveManager();
            return _instance;
        }
    }

    public SaveData Data { get; private set; }

    private SaveManager()
    {
        Data = PlayerPrefs.HasKey("SaveData")
            ? JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("SaveData"))
            : new SaveData();
    }
    
    public void Save()
    {
        string json = JsonUtility.ToJson(Data);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
    }
}