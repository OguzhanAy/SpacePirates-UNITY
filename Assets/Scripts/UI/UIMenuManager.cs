using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuManager : MonoBehaviour
{
    public GameObject PnlSinglePlayer;

    public GameObject ScrSingleHQ;
    public GameObject ScrSingleHangar;
    public GameObject ScrSingleLab;
    public GameObject ScrSingleWorkshop;

    public const int SP_SCREEN_HQ = 0;
    public const int SP_SCREEN_Hangar = 1;
    public const int SP_SCREEN_Lab = 2;
    public const int SP_SCREEN_Workshop = 3;

    int currentScreen = SP_SCREEN_HQ;
    
    void Start()
    {
        PnlSinglePlayer.SetActive(false);
        
        ScrSingleHQ.SetActive(false);
        ScrSingleHangar.SetActive(false);
        ScrSingleLab.SetActive(false);
        ScrSingleWorkshop.SetActive(false);
    }

    public void DisplaySinglePlayer()
    {
        PnlSinglePlayer.SetActive(true);

        SwitchToScreen(SP_SCREEN_HQ);
    }

    public void SwitchToScreen(int screen)
    {
        ScrSingleHQ.SetActive(false);
        ScrSingleHangar.SetActive(false);
        ScrSingleLab.SetActive(false);
        ScrSingleWorkshop.SetActive(false);
        
        switch (screen)        
        {               
            case SP_SCREEN_HQ:
                ScrSingleHQ.SetActive(true);
                break;
            case SP_SCREEN_Hangar:
                ScrSingleHangar.SetActive(true);
                break;
            case SP_SCREEN_Lab:
                ScrSingleLab.SetActive(true);
                break;
            case SP_SCREEN_Workshop:
                ScrSingleWorkshop.SetActive(true);
                break;                
        }

        currentScreen = screen;

    }
    
    
    
}
