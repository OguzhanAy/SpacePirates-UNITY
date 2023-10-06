using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStandard : MonoBehaviour
{
    public GameObject UIBackground;
    public GameObject UIInfo;
    public GameObject UIError;
    public GameObject UIConfirm;
    public GameObject UIPrompt;

    private float fadeDuration = .3f;
    
    void Start()
    {
        // TODO: fadeDuration bir settings yapısından çekilmeli
        
        UIBackground.SetActive(false);
        
    }

    public void Info(string message, string title = "INFORMATİON", string buttonCaption = "OK", Action onOK = null)
    {
        DisplayBackground();
        
        UIInfo.SetActive(true);
        UIInfo.transform.Find("Title/Text").GetComponent<Text>().text = title;
        UIInfo.transform.Find("Text").GetComponent<Text>().text = message;
        Transform button = UIInfo.transform.Find("BtnOK");
        
        button.transform.Find("Text").GetComponent<Text>().text = buttonCaption;
        button.GetComponent<Button>().onClick.RemoveAllListeners();;
        button.GetComponent<Button>().onClick.AddListener(() =>
        {
            CloseElement(onOK);
        });


    }   

    public void Error(string message, string title = "ERROR", string buttonCaption = "OK", Action onOK = null)
    {
        DisplayBackground();
        
        UIError.SetActive(true);
        UIError.transform.Find("Title/Text").GetComponent<Text>().text = title;
        UIError.transform.Find("Text").GetComponent<Text>().text = message;
        Transform button = UIError.transform.Find("BtnOK");
        
        button.transform.Find("Text").GetComponent<Text>().text = buttonCaption;
        button.GetComponent<Button>().onClick.RemoveAllListeners();;
        button.GetComponent<Button>().onClick.AddListener(() =>
        {
            CloseElement(onOK);
        });
    }
    
    public void Confirm(string message, string title = "CONFIRM", string buttonCaptionYes = "YES", string buttonCaptionNo = "NO", Action onYes = null, Action onNo = null)
    {
        DisplayBackground();
        
        UIConfirm.SetActive(true);
        UIConfirm.transform.Find("Title/Text").GetComponent<Text>().text = title;
        UIConfirm.transform.Find("Text").GetComponent<Text>().text = message;
        Transform buttonYes = UIConfirm.transform.Find("BtnYes");
        Transform buttonNo = UIConfirm.transform.Find("BtnNo");
        
        buttonYes.transform.Find("Text").GetComponent<Text>().text = buttonCaptionYes;
        buttonYes.GetComponent<Button>().onClick.RemoveAllListeners();;
        buttonYes.GetComponent<Button>().onClick.AddListener(() =>
        {
            CloseElement(onYes);
        });
        buttonNo.transform.Find("Text").GetComponent<Text>().text = buttonCaptionNo;
        buttonNo.GetComponent<Button>().onClick.RemoveAllListeners();;
        buttonNo.GetComponent<Button>().onClick.AddListener(() =>
        {
            CloseElement(onNo);
        });
        
    }
    
    public void Prompt(string title = "ENTER INFORMATION", string description = "", string placeHolder = "Enter Value", string buttonCaption = "OK", string value = "", Action<string> onValueSet = null)
    {
        DisplayBackground();
        
        UIPrompt.SetActive(true);
        UIPrompt.transform.Find("Title/Text").GetComponent<Text>().text = title;
        UIPrompt.transform.Find("Text").GetComponent<Text>().text = description;
        UIPrompt.transform.Find("InputField").GetComponent<InputField>().text = value;
        UIPrompt.transform.Find("InputField/Placeholder").GetComponent<Text>().text = placeHolder;
        
        Transform buttonOK = UIPrompt.transform.Find("BtnOK");
        Transform buttonClose = UIPrompt.transform.Find("BtnClose");
        
        buttonOK.transform.Find("Text").GetComponent<Text>().text = buttonCaption;
        buttonOK.GetComponent<Button>().onClick.RemoveAllListeners();;
        buttonOK.GetComponent<Button>().onClick.AddListener(() =>
        {
            CloseElement(() =>
            {
                onValueSet(UIPrompt.transform.Find("InputField/Text").GetComponent<Text>().text);
            });
        });
        buttonClose.GetComponent<Button>().onClick.RemoveAllListeners();;
        buttonClose.GetComponent<Button>().onClick.AddListener(() =>
        {
            CloseElement(() =>
            {
                onValueSet("");
            });
        });
        
    }
    
    // Utility function
    
    private void DisplayBackground()
    {
        UIInfo.SetActive(false);
        UIError.SetActive(false);
        UIConfirm.SetActive(false);
        UIPrompt.SetActive(false);
        
        UIBackground.SetActive(true);
        UIBackground.transform.SetAsLastSibling();//Her şeyin üstünde çalışması için
        
        CanvasGroup canvasGroup = UIBackground.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;
        canvasGroup.DOFade(1, fadeDuration).SetEase(Ease.Linear);
    }

    private void CloseElement(Action onOk)
    {
        StartCoroutine(CloseElementBg(onOk));
    }

    IEnumerator CloseElementBg(Action onOk)
    {
        CanvasGroup canvasGroup = UIBackground.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0, fadeDuration).SetEase(Ease.Linear);

        yield return new WaitForSeconds(fadeDuration);

        onOk?.Invoke();
        UIBackground.SetActive(false);
    }
}
