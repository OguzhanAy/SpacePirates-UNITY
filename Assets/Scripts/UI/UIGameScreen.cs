using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScreen : MonoBehaviour
{
   public GameObject TxtGameOver;
   public Slider SldXP;
   public Slider SldHP;

   int playerMaxHP;
   int playerMaxXP;
   int playerCurrHP;
   int playerCurrXP;

   private void Start()
   {
      TxtGameOver.SetActive(false);
   }

   public void DisplayGameOverText()
   {
      TxtGameOver.SetActive(true);
   }

   public void SetPlayerMaxHP(int maxHP)
   {
      SldHP.maxValue = maxHP;
      playerMaxHP = maxHP;
      DisplayPlayerHP();
      
   }
   public void SetPlayerMaxXP(int maxXP)
   {
      SldXP.maxValue = maxXP;
      playerMaxXP = maxXP;
      DisplayPlayerXP();
   }

   private void DisplayPlayerHP()
   {
      SldHP.transform.Find("Text").GetComponent<Text>().text = playerCurrHP + " / " + playerMaxHP;
   }
   
   private void DisplayPlayerXP()
   {
      SldXP.transform.Find("Text").GetComponent<Text>().text = playerCurrXP + " / " + playerMaxXP;
   }

  
}
