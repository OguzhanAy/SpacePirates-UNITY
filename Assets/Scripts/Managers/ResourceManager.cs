using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    
    void Start()
    {

        if (SettingsManager.Instance.LastResourceCollectionTime == 0)
        {
            //ilk defa oyun açılıyor demektir, hiçbir şey yapma            
        }
        else
        {
            int minutesPassedSinceLastShutdown = 
                Mathf.FloorToInt((float)(Utility.GetTime() - DateTime.FromOADate(SettingsManager.Instance.LastResourceCollectionTime)).TotalMinutes);

            if (minutesPassedSinceLastShutdown>0)
            {
            SettingsManager.Instance.Resource1Amount += (SettingsManager.Instance.Resource1PerHour / 60f)* minutesPassedSinceLastShutdown ;
            SettingsManager.Instance.Resource2Amount += (SettingsManager.Instance.Resource2PerHour / 60f)* minutesPassedSinceLastShutdown;
            SettingsManager.Instance.Resource3Amount += (SettingsManager.Instance.Resource3PerHour / 60f)* minutesPassedSinceLastShutdown;
            SettingsManager.Instance.Resource4Amount += (SettingsManager.Instance.Resource4PerHour / 60f)* minutesPassedSinceLastShutdown;
                SettingsManager.Instance.Save();
            }

        }
        
        StartCoroutine(CollectResources());
    }

    IEnumerator CollectResources()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);

            SettingsManager.Instance.Resource1Amount += SettingsManager.Instance.Resource1PerHour / 60f;
            SettingsManager.Instance.Resource2Amount += SettingsManager.Instance.Resource2PerHour / 60f;
            SettingsManager.Instance.Resource3Amount += SettingsManager.Instance.Resource3PerHour / 60f;
            SettingsManager.Instance.Resource4Amount += SettingsManager.Instance.Resource4PerHour / 60f;

            SettingsManager.Instance.LastResourceCollectionTime = Utility.GetTime().ToOADate();
            SettingsManager.Instance.Save();
            
        }
    }
    
}
