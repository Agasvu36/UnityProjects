using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;

[System.Serializable]
public class PlayerInfo 
{
    public int CurrentLevel;
}

public class Progress : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerInfo PlayerInfo;
    //[SerializeField] TextMeshProUGUI textMeshProUGUI;


    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]
    public static extern void LoadExtern();

    public static Progress Instance;


    private void Start()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            //LoadExtern();
            LoadExtern();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Save() 
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(jsonString);
    }

    public void SetPlayerInfo(string value) 
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
       // textMeshProUGUI.text = Progress.Instance.PlayerInfo.CurrentLevel.ToString();
    }


}
