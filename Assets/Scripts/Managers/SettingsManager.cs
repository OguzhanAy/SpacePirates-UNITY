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

    
    private ObscuredFloat resource1Amount;
    private ObscuredFloat resource2Amount;
    private ObscuredFloat resource3Amount;
    private ObscuredFloat resource4Amount; 



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
        
    }

#if UNITY_EDITOR
    [MenuItem("Moon Games/CN Space Pirates/Delete Settings")] 
    static void DeleteSaveData()
    {
        ObscuredPrefs.DeleteAll();
        Debug.Log("All keys have been deleted ");
    }
    
#endif    
    


    private void Start()
    {
        Load();
    }

    public void Save()
    {
        ObscuredPrefs.Set("Resource1PerHour", Resource1PerHour);
        ObscuredPrefs.Set("Resource2PerHour", Resource2PerHour);
        ObscuredPrefs.Set("Resource3PerHour", Resource3PerHour);
        ObscuredPrefs.Set("Resource4PerHour", Resource4PerHour);
        ObscuredPrefs.Set("LastResourceCollectionTime", LastResourceCollectionTime);
        ObscuredPrefs.Set("Resource1Amount", Resource1Amount);
        ObscuredPrefs.Set("Resource2Amount", Resource2Amount);
        ObscuredPrefs.Set("Resource3Amount", Resource3Amount);
        ObscuredPrefs.Set("Resource4Amount", Resource4Amount);
        
        ObscuredPrefs.Save();
        
    }

    public void Load()
    {
        Resource1PerHour = ObscuredPrefs.Get("Resource1PerHour", Resource1PerHour);
        Resource2PerHour = ObscuredPrefs.Get("Resource2PerHour", Resource2PerHour);
        Resource3PerHour = ObscuredPrefs.Get("Resource3PerHour", Resource3PerHour);
        Resource4PerHour = ObscuredPrefs.Get("Resource4PerHour", Resource4PerHour);
        LastResourceCollectionTime = ObscuredPrefs.Get("LastResourceCollectionTime", LastResourceCollectionTime);
        resource1Amount = ObscuredPrefs.Get("Resource1Amount", 0);
        resource2Amount = ObscuredPrefs.Get("Resource2Amount", 0);
        resource3Amount = ObscuredPrefs.Get("Resource3Amount", 0);
        resource4Amount = ObscuredPrefs.Get("Resource4Amount", 0);
    }
}
