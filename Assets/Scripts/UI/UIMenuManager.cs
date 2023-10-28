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

    public LocalizedStringTable LocalizedTable;

    int currentScreen = SP_SCREEN_HOME;
    StringTable localizedTable;

    private const float SCREEN_INTERVAL = 0.3f;
    
    void Start()
    {
        ScreenBase.SetActive(false);
        PnlSinglePlayer.SetActive(false);

        localizedTable = LocalizedTable.GetTable();

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
            rect.DOAnchorPosX(Screen.width, SCREEN_INTERVAL);
            yield return new WaitForSeconds(SCREEN_INTERVAL);
            ScreenBase.SetActive(false);

        }
        else
        {
            ScreenBase.SetActive(true);
            rect.anchoredPosition = new Vector2(Screen.width, 0);
            rect.DOAnchorPosX(0, SCREEN_INTERVAL);

            switch (screen)        
            {               
                case SP_SCREEN_HQ:
                    ScreenTitle.text = localizedTable.GetEntry("btnHQ").Value;
                    break;
                case SP_SCREEN_Hangar:
                    ScreenTitle.text = localizedTable.GetEntry("btnHangar").Value;
                    break;
                case SP_SCREEN_Lab:
                    ScreenTitle.text = localizedTable.GetEntry("btnLab").Value;
                    break;
                case SP_SCREEN_Workshop:
                    ScreenTitle.text = localizedTable.GetEntry("btnWorkshop").Value;
                    break;                
            }
        }
        

        currentScreen = screen;
    }
    
    IEnumerator DisplayResources()
    {
        while (true)
        {
            Resource1.text = Mathf.FloorToInt(SettingsManager.Instance.Resource1Amount).ToString();
            Resource2.text = Mathf.FloorToInt(SettingsManager.Instance.Resource2Amount).ToString();
            Resource3.text = Mathf.FloorToInt(SettingsManager.Instance.Resource3Amount).ToString();
            Resource4.text = Mathf.FloorToInt(SettingsManager.Instance.Resource4Amount).ToString();
            yield return new WaitForSeconds(60);
        }
    }
    
}
