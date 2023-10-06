using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITestStd : MonoBehaviour
{
    public UIStandard UI;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UI.Info("Hayırlı İşler!", "BİLGİ", "TAMAM", () =>
            {
                Debug.Log("Bilgi ekranı kapatıldı.");
            });
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UI.Error("Off çok kötü...", "HATA", "KAPAT", () =>
            {
                Debug.Log("Hata ekranı kapatıldı.");
            });
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UI.Confirm("Emin misin?", "SORU", "EVET", "HAYIR", () =>
            {
                Debug.Log("Evet seçildi.");
            },() =>
            {
                Debug.Log("Hayır seçildi.");
            });
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UI.Prompt("ADINIZI GİRİN", "Adınızı yukarıdaki kutucuğa yazınız.", "Adınızı girin.","GİR", "Mahmut", (param) =>
            {
                if (!string.IsNullOrEmpty(param))
                {
                    Debug.Log("Girilen isim: " + (string)param);
                }
                else
                {
                    Debug.Log("İsim girilmeden kapatıldı.");
                }
                
            });
        }
    }
}
