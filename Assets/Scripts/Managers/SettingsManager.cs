using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using CodeStage.AntiCheat.Storage;
using UnityEditor;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    
    public ObscuredInt Resource1PerHour = 1;
    public ObscuredInt Resource2PerHour = 1;
    public ObscuredInt Resource3PerHour = 1;
    public ObscuredInt Resource4PerHour = 1;
    public ObscuredDouble LastResourceCollectionTime = double.MinValue;
    public ObscuredFloat Resource1Amount;
    public ObscuredFloat Resource2Amount;
    public ObscuredFloat Resource3Amount;
    public ObscuredFloat Resource4Amount;
    
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);                
        }
        Load();
    }

#if UNITY_EDITOR
    [MenuItem("Moon Games/CN SpacePirates/DeleteSettings")]
    static void DeleteSaveData()
    {
        ObscuredPrefs.DeleteAll();
        Debug.Log("All keys have been deleted");
    }
    
#endif

    private void Start()
    {
        
    }

    public void Save()
    {
        ObscuredPrefs.Set("Resource1PerHour", (int)Resource1PerHour);
        ObscuredPrefs.Set("Resource2PerHour", (int)Resource2PerHour);
        ObscuredPrefs.Set("Resource3PerHour", (int)Resource3PerHour);
        ObscuredPrefs.Set("Resource4PerHour", (int)Resource4PerHour);
        ObscuredPrefs.Set("LastResourceCollectionTime", (double)LastResourceCollectionTime);
        ObscuredPrefs.Set("Resource1Amount", (float)Resource1Amount);
        ObscuredPrefs.Set("Resource2Amount", (float)Resource2Amount);
        ObscuredPrefs.Set("Resource3Amount", (float)Resource3Amount);
        ObscuredPrefs.Set("Resource4Amount", (float)Resource4Amount);
        
        ObscuredPrefs.Save();
        
    }

    public void Load()
    {
        Resource1PerHour = ObscuredPrefs.Get("Resource1PerHour", (int)Resource1PerHour);
        Resource2PerHour = ObscuredPrefs.Get("Resource2PerHour", (int)Resource2PerHour);
        Resource3PerHour = ObscuredPrefs.Get("Resource3PerHour", (int)Resource3PerHour);
        Resource4PerHour = ObscuredPrefs.Get("Resource4PerHour", (int)Resource4PerHour);
        LastResourceCollectionTime = ObscuredPrefs.Get("LastResourceCollectionTime", (double)LastResourceCollectionTime);
        Resource1Amount = ObscuredPrefs.Get("Resource1Amount", (float)Resource1Amount);
        Resource2Amount = ObscuredPrefs.Get("Resource2Amount", (float)Resource2Amount);
        Resource3Amount = ObscuredPrefs.Get("Resource3Amount", (float)Resource3Amount);
        Resource4Amount = ObscuredPrefs.Get("Resource4Amount", (float)Resource4Amount);
    }
}
