using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIResearchDetailWindow : MonoBehaviour
{
    public GameObject Window;
    
    
    public static UIResearchDetailWindow Instance { get; private set; }
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

        Window.GetComponent<CanvasGroup>().alpha = 0;
        Window.SetActive(false);
    }

    public void Hide()
    {
        StartCoroutine(HideBG());
    }

    IEnumerator HideBG()
    {
        Window.GetComponent<CanvasGroup>().DOFade(0, SettingsManager.Instance.ScreenInterval).SetEase(Ease.Linear);
        yield return new WaitForSeconds(SettingsManager.Instance.ScreenInterval);
        Window.SetActive(false);
    }

    public void DisplayDetail(UIResearchNavigate nav, string key)
    {
        
        Window.GetComponent<CanvasGroup>().alpha = 0;
        Window.SetActive(false);
        for (int p = 0; p < ResearchManager.Instance.ResearchProjects.Length; p++)
        {
            if (ResearchManager.Instance.ResearchProjects[p].Key == key )
            {
                Window.transform.Find("TextCaption").GetComponent<Text>().text =
                    LocalizationManager.Instance.Translate(ResearchManager.Instance.ResearchProjects[p].Title);
                Window.transform.Find("ContentHolder/TextDescription").GetComponent<Text>().text =
                    LocalizationManager.Instance.Translate(ResearchManager.Instance.ResearchProjects[p].Description);
                if (ResearchManager.Instance.ResearchProjects[p].Avatar != null)
                {
                    Window.transform.Find("ContentHolder/Avatar").GetComponent<Image>().sprite =
                        ResearchManager.Instance.ResearchProjects[p].Avatar;
                    Window.transform.Find("ContentHolder/Avatar").GetComponent<Image>().preserveAspect = true;
                } 
                Window.SetActive(true);
                Window.GetComponent<CanvasGroup>().DOFade(1,
                   SettingsManager.Instance.ScreenInterval).SetEase(Ease.Linear);

                var button = Window.transform.Find("ContentHolder/BtnResearch");
                var resourceHolder = Window.transform.Find("ContentHolder/ResourceDisplay/ResourceHolder");
                button.gameObject.SetActive(ResearchManager.Instance.ResearchProjects[p].TimeStarted == DateTime.MinValue);
                
                resourceHolder.GetChild(0).gameObject.SetActive(ResearchManager.Instance.ResearchProjects[p].Resource1 != 0);
                resourceHolder.GetChild(0).GetChild(1).GetComponent<Text>().text = Utility.AmountToString(ResearchManager.Instance.ResearchProjects[p].Resource1);
                
                resourceHolder.GetChild(1).gameObject.SetActive(ResearchManager.Instance.ResearchProjects[p].Resource2 != 0);
                resourceHolder.GetChild(1).GetChild(1).GetComponent<Text>().text = Utility.AmountToString(ResearchManager.Instance.ResearchProjects[p].Resource2);

                resourceHolder.GetChild(2).gameObject.SetActive(ResearchManager.Instance.ResearchProjects[p].Resource3 != 0);
                resourceHolder.GetChild(2).GetChild(1).GetComponent<Text>().text = Utility.AmountToString(ResearchManager.Instance.ResearchProjects[p].Resource3);

                resourceHolder.GetChild(3).gameObject.SetActive(ResearchManager.Instance.ResearchProjects[p].Resource4 != 0);
                resourceHolder.GetChild(3).GetChild(1).GetComponent<Text>().text = Utility.AmountToString(ResearchManager.Instance.ResearchProjects[p].Resource4);

                
                
                // Eğer araştırma bitmişse Research düğmesi ve kaynakları komple görünmez yapacak
                if (ResearchManager.Instance.ResearchProjects[p].Completed)
                {
                    button.GetComponent<CanvasGroup>().alpha = .85f;
                    button.GetComponent<Image>().color = new Color(.67f, .67f, .67f, 1);
                    button.GetComponent<Button>().interactable = false;
                    button.GetChild(0).GetComponent<Text>().text =
                        LocalizationManager.Instance.Translate("strCompleted");

                }
                else
                {
                    if (
                            !ResearchManager.Instance.IsApplicable(key) || 
                            SettingsManager.Instance.Resource1Amount < ResearchManager.Instance.ResearchProjects[p].Resource1 ||
                        SettingsManager.Instance.Resource2Amount < ResearchManager.Instance.ResearchProjects[p].Resource2 ||
                        SettingsManager.Instance.Resource3Amount < ResearchManager.Instance.ResearchProjects[p].Resource3 ||
                        SettingsManager.Instance.Resource4Amount < ResearchManager.Instance.ResearchProjects[p].Resource4 )
                        // TODO: Level kontrol
                    {
                        // Eğer araştırma bitmemişse fakat kaynaklar ya da level yetmiyorsa research düğmesi ve kaynakları komple GÖRÜNÜR olacak, ama düğmeye basılmayacak
                        button.GetComponent<CanvasGroup>().alpha = .85f;
                        button.GetComponent<Image>().color = new Color(.33f, .33f, .33f, 1);
                        button.GetComponent<Button>().interactable = false;
                        button.GetChild(0).GetComponent<Text>().text =
                            LocalizationManager.Instance.Translate("strResearch");
                    }
                    else
                    {
                        if (ResearchManager.Instance.ResearchProjects[p].TimeStarted != DateTime.MinValue)
                        {
                            // Eğer üstünde çalışılıyorsa kalan zaman düğmenin yerinde gösterilecek
                            
                        }
                        else if( ResearchManager.Instance.IsApplicable(key))
                        {
                            Button btn = button.GetComponent<Button>();
                            
                            button.GetComponent<CanvasGroup>().alpha = 1f;
                            button.GetComponent<Image>().color = Color.white;
                            button.GetChild(0).GetComponent<Text>().text =
                                LocalizationManager.Instance.Translate("strResearch");
                            
                            btn.interactable = true;
                            btn.onClick.RemoveAllListeners();
                            btn.onClick.AddListener(() =>
                            {
                                if (   
                                    SettingsManager.Instance.Resource1Amount < ResearchManager.Instance.ResearchProjects[p].Resource1 ||
                                    SettingsManager.Instance.Resource2Amount < ResearchManager.Instance.ResearchProjects[p].Resource2 ||
                                    SettingsManager.Instance.Resource3Amount < ResearchManager.Instance.ResearchProjects[p].Resource3 ||
                                    SettingsManager.Instance.Resource4Amount < ResearchManager.Instance.ResearchProjects[p].Resource4 )
                                {
                                    UIStandard ui = FindObjectOfType<UIStandard>();
                                    ui.Error(LocalizationManager.Instance.Translate("resNotEnoughResources"));
                                }
                                else
                                {
                                    btn.interactable = false;
                                    ResearchManager.Instance.StartProject(key);
                                    nav.HandleResearch();
                                }
                            });
                            
                        }
                    }
                        
                    
                }
                break;
            }
        }
    }

   
    
}
