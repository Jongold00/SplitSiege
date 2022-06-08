using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using System;

public class UserProfileManager : MonoBehaviour
{
    #region Singleton

    public static UserProfileManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            instance = this;
        }

    }

    private void Start()
    {
        activeProfile = LoadData(Application.persistentDataPath + "/saves/" + "userprofiledata.save");

        listenGameStateChange += OnGameStateChange;
        EventsManager.instance.SubscribeGameStateChange(listenGameStateChange);
    }

    private void OnDestroy()
    {
        EventsManager.instance.UnSubscribeGameStateChange(listenGameStateChange);
    }

    #endregion Singleton

    Action<GameStateManager.GameState> listenGameStateChange;


    public UserData activeProfile;



    public void OnGameStateChange(GameStateManager.GameState newState)
    {
        if (newState == GameStateManager.GameState.Won)
        {
            activeProfile.UpdateLevelData(GameStateManager.instance.currentLevelName, GameStateManager.instance.GetNumStarsOnWin());
            Save("userprofiledata", activeProfile);
        }

    }

    public UserData LoadData(string filePath)
    {
        if (!File.Exists(filePath))
        {
            UserData newData = new UserData();
            Save("userprofiledata", newData);
            return newData;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(filePath, FileMode.Open);

        try
        {
            UserData saveFile = (UserData)formatter.Deserialize(file);
            file.Close();
            return saveFile;
        }

        catch
        {
            Debug.Log("failed to load from save file");
            file.Close();
            return null;    
        }
    }

    public bool Save(string fileName, UserData data)
    {
        Debug.Log("saving...");
        BinaryFormatter formatter = new BinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string filePath = Application.persistentDataPath + "/saves/" + fileName + ".save";

        FileStream file = File.Create(filePath);
        formatter.Serialize(file, data);

        return true;
    }


}
