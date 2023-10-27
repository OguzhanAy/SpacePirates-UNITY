using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using CodeStage.AntiCheat.Storage;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    
    public ObscuredInt Resource1PerHour = 1;
    public ObscuredInt Resource2PerHour = 1;
    public ObscuredInt Resource3PerHour = 1;
    public ObscuredInt Resource4PerHour = 1;
    public ObscuredDouble LastResourceCollectionTime = double.MinValue;
<<<<<<< Updated upstream
    
    public ObscuredInt Resource1Amount { get {return resource1Amount;} }
    public ObscuredInt Resource2Amount { get {return resource2Amount;} }
    public ObscuredInt Resource3Amount { get {return resource3Amount;} }
    public ObscuredInt Resource4Amount { get {return resource4Amount;} }
    
    private ObscuredInt resource1Amount;
    private ObscuredInt resource2Amount;
    private ObscuredInt resource3Amount;
    private ObscuredInt resource4Amount;
=======


    public ObscuredInt Resource1Amount;
    public ObscuredInt Resource2Amount;
    public ObscuredInt Resource3Amount;
    public ObscuredInt Resource4Amount;
    

    public ObscuredFloat Resource1Amount;
    public ObscuredFloat Resource2Amount;
    public ObscuredFloat Resource3Amount;
    public ObscuredFloat Resource4Amount;

>>>>>>> Stashed changes

    void Awake()
    {
        DontDestroyOnLoad(Instance);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);                
        }
        
    }

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
        ObscuredPrefs.Set("Resource1Amount", resource1Amount);
        ObscuredPrefs.Set("Resource2Amount", resource2Amount);
        ObscuredPrefs.Set("Resource3Amount", resource3Amount);
        ObscuredPrefs.Set("Resource4Amount", resource4Amount);
        
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
