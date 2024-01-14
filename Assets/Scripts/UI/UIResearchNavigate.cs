using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIResearchNavigate : MonoBehaviour
{
    public string Key;
    public GameObject SourceResearchTopics;
    public GameObject TargetResearchTopics;

    public GameObject ResearchProgress;

    UIMenuManager menuManager;
    GameObject instantiatedResearchProgress = null;

    private void Start()
    {
        AdjustImageColor();
        

        menuManager = FindObjectOfType<UIMenuManager>();
        HandleResearch();
    }

    public void AdjustImageColor()
    {
        if (TargetResearchTopics == null)
        {
            Image img = GetComponent<Image>();
            
            //adjust the color
            if (string.IsNullOrEmpty(Key))
            {
                Color c = new Color(0.125f, .125f, .125f, 1f);
                img.color = c;
            }
            else
            {
                SOResearchProject project = ResearchManager.Instance.GetResearchProject(Key);
                Color c = new Color(.125f, .125f, .125f, 1f);
                if (project !=null && project.Completed )
                {
                    //bitmişse
                    c = Color.white;
                }

                if (project != null && !project.Completed && (project.TimeStarted != DateTime.MinValue || ResearchManager.Instance.IsApplicable(Key)))
                {
                    //başlamışsa
                    c = new Color(.45f, .45f, .9f, 1f);
                }
                img.color = c;
                //img.sprite project avatar;
            }
        }
    }

    public void HandleResearch()
    {
        if (ResearchProgress !=null && !string.IsNullOrEmpty(Key))
        {
            var project = ResearchManager.Instance.GetResearchProject(Key);
            if (project != null && !project.Completed && project.TimeStarted != DateTime.MinValue )
            {
                instantiatedResearchProgress = Instantiate(ResearchProgress, transform);
                StartCoroutine(CountDownForResearch(instantiatedResearchProgress, project));

            }
        }
    }

    IEnumerator CountDownForResearch(GameObject instantiatedResearchProgress, SOResearchProject project)
    {
        Slider progress = instantiatedResearchProgress.transform.Find("SldProgress").GetComponent<Slider>();
        Text timerText = instantiatedResearchProgress.transform.Find("Timer/Text").GetComponent<Text>();
        progress.maxValue = project.Duration;

        TimeSpan remainingTime;
        while (Utility.GetTime() < project.TimeStarted.AddSeconds(project.Duration))
        {
            progress.value = (float)(Utility.GetTime() - project.TimeStarted).TotalSeconds;
            Debug.Log((float)(DateTime.Now - project.TimeStarted).TotalSeconds);
            yield return new WaitForSeconds(1);

            remainingTime = TimeSpan.FromSeconds(project.Duration -
                                                 Mathf.RoundToInt((float)(Utility.GetTime() - project.TimeStarted).TotalSeconds));
            if (remainingTime.TotalDays >1)
            {
                timerText.text = remainingTime.Days + "days" + remainingTime.Hours.ToString().PadLeft(2, '0')+":"+remainingTime.Minutes.ToString().PadLeft(2, '0')+":" + remainingTime.Seconds.ToString().PadLeft(2, '0');
                
            }
            else
            {
                timerText.text = remainingTime.Hours.ToString().PadLeft(2, '0')+":"+remainingTime.Minutes.ToString().PadLeft(2, '0')+":" + remainingTime.Seconds.ToString().PadLeft(2, '0');
                
            }
        }

        StartCoroutine(RefreshAllResearchProjects());
        Destroy(instantiatedResearchProgress);
    }

    IEnumerator RefreshAllResearchProjects()
    {
        yield return new WaitForSeconds(1.1f);
        
        UIResearchNavigate[] scripts = FindObjectsOfType<UIResearchNavigate>();
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].AdjustImageColor();
        }
    }

    public void Navigate()
    {
        StartCoroutine(NavigateNow());
        UIResearchDetailWindow.Instance.Hide();
    }

    IEnumerator NavigateNow()
    {
        SourceResearchTopics.GetComponent<CanvasGroup>().alpha = 1;
        SourceResearchTopics.GetComponent<CanvasGroup>().DOFade(0, SettingsManager.Instance.ScreenInterval);
        
        yield return new WaitForSeconds(SettingsManager.Instance.ScreenInterval);
        SourceResearchTopics.SetActive(false);
        TargetResearchTopics.SetActive(true);
        
        TargetResearchTopics.GetComponent<CanvasGroup>().alpha = 0;
        TargetResearchTopics.GetComponent<CanvasGroup>().DOFade(1, SettingsManager.Instance.ScreenInterval);

    }

    public void DisplayDetails()
    {
        UIResearchDetailWindow.Instance.DisplayDetail(this, Key);
    }
    
     
}
