using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    public GameObject PnlSinglePlayer;
    
    public GameObject ScreenBase;
    public GameObject ScreenResearch;
    public Text ScreenTitle;
    public Text Resource1;
    public Text Resource2;
    public Text Resource3;
    public Text Resource4;
    
        
    public const int SP_SCREEN_HOME = 0;
    public const int SP_SCREEN_HQ = 1;
    public const int SP_SCREEN_Hangar = 2;
    public const int SP_SCREEN_Lab = 3;
    public const int SP_SCREEN_Workshop = 4;

    

    int currentScreen = SP_SCREEN_HOME;
   

    
    
    void Start()
    {
        ScreenBase.SetActive(false);
        PnlSinglePlayer.SetActive(false);
        StartCoroutine(DisplayResources());
    }

    

    public void DisplaySinglePlayer()
    {
        PnlSinglePlayer.SetActive(true);

        SwitchToScreen(SP_SCREEN_HOME);
    }

    public void SwitchToScreen(int screen)
    {

        StartCoroutine(SwitchToScreenBG(screen));

    }

    IEnumerator SwitchToScreenBG(int screen)
    {
        if (screen == currentScreen) yield break;
        RectTransform rect = ScreenBase.GetComponent<RectTransform>();
        if (screen == SP_SCREEN_HOME)
        {
            rect.DOAnchorPosX(Screen.width, SettingsManager.Instance.ScreenInterval);
            yield return new WaitForSeconds(SettingsManager.Instance.ScreenInterval);
            ScreenBase.SetActive(false);

        }
        else
        {
            ScreenBase.SetActive(true);
            rect.anchoredPosition = new Vector2(Screen.width, 0);
            rect.DOAnchorPosX(0, SettingsManager.Instance.ScreenInterval);

            ScreenResearch.SetActive(screen == SP_SCREEN_Lab);
            
            switch (screen)        
            {               
                case SP_SCREEN_HQ:
                    ScreenTitle.text = LocalizationManager.Instance.Translate("btnHQ");
                    break;
                case SP_SCREEN_Hangar:
                    ScreenTitle.text = LocalizationManager.Instance.Translate("btnHangar");
                    break;
                case SP_SCREEN_Lab:
                    ScreenTitle.text = LocalizationManager.Instance.Translate("btnLab");
                    break;
                case SP_SCREEN_Workshop:
                    ScreenTitle.text = LocalizationManager.Instance.Translate("btnWorkshop");
                    break;                
            }
        }
        

        currentScreen = screen;
    }
    
    IEnumerator DisplayResources()
    {
        while (true)
        {
            Resource1.text = Utility.AmountToString(Mathf.FloorToInt(SettingsManager.Instance.Resource1Amount));
            Resource2.text = Utility.AmountToString(Mathf.FloorToInt(SettingsManager.Instance.Resource2Amount));
            Resource3.text = Utility.AmountToString(Mathf.FloorToInt(SettingsManager.Instance.Resource3Amount));
            Resource4.text = Utility.AmountToString(Mathf.FloorToInt(SettingsManager.Instance.Resource4Amount));
            yield return new WaitForSeconds(1);
        }
    }
    
}
