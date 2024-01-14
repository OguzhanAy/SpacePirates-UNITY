using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

public class LocalizationManager : MonoBehaviour
{
    public LocalizedStringTable LocalizedTable;
    
    StringTable localizedTable;
    
    public static LocalizationManager Instance { get; private set; }
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
    
    private void Start()
    {
        localizedTable = LocalizedTable.GetTable();
    }

    public string Translate(string text)
    {
        return localizedTable.GetEntry(text).Value;
    }
    
}
