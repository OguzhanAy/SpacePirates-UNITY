using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    [SerializeField]
    private SOResearchProject[] researchProjects;
    
    public SOResearchProject[] ResearchProjects
    {
        get
        {
            return researchProjects;
        }
    }
    
    public static ResearchManager Instance { get; private set; }
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

    private void PollResearchProjects()
    {
        bool saveRequired = false;
        for (int rp = 0; rp < researchProjects.Length; rp++)
        {
            if (!researchProjects[rp].Completed && //Bitmemiş
                researchProjects[rp].TimeStarted != DateTime.MinValue && // başlamışsa
                Utility.GetTime() > researchProjects[rp].TimeStarted.AddSeconds(researchProjects[rp].Duration) //süresi tamamlanmış
                )
            {
                researchProjects[rp].Completed = true;
                saveRequired = true;
                
            }
        }

        if (saveRequired)
        {
            SettingsManager.Instance.SaveResearchProjects();
        }
    }

    private void Start()
    {
        SettingsManager.Instance.LoadResearchProjects();
        InvokeRepeating("PollResearchProjects", 0, 1);
    }

    public SOResearchProject GetResearchProject(string key)
    {
        for (int i = 0; i < researchProjects.Length; i++)
        {
            if (researchProjects[i].Key == key)
            {
                return researchProjects[i];
            }
        }

        return null;
    }
    
    public bool IsApplicable(string key)
    {
        SOResearchProject project = GetResearchProject(key);
        if (project == null)
        {
            return false;
        }
        SOResearchProject dependency;
        for (int i = 0; i < project.Dependencies.Length; i++)
        {
            dependency = GetResearchProject(project.Dependencies[i]);
            if (dependency != null)
            {
                if (!dependency.Completed)
                {
                    return false;
                }                
            }
            else
            {
                Debug.LogWarning("null dependency research project: dep=" + project.Dependencies[i] + "project key="+ project.Key);
            }
        }

        return !project.Completed;
    }


    public void StartProject(string key)
    {
        SOResearchProject project = GetResearchProject(key);
        if (project == null)
        {
            
            return;
        }
        

        SettingsManager.Instance.Resource1Amount -= project.Resource1;
        SettingsManager.Instance.Resource2Amount -= project.Resource2;
        SettingsManager.Instance.Resource3Amount -= project.Resource3;
        SettingsManager.Instance.Resource4Amount -= project.Resource4;
        
        
        project.TimeStarted = Utility.GetTime();
        SettingsManager.Instance.Save();
        SettingsManager.Instance.SaveResearchProjects();
    }

   
}
