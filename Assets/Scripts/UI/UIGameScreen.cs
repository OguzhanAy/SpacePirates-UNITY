using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameScreen : MonoBehaviour
{
   public GameObject TxtGameOver;

   private void Start()
   {
      TxtGameOver.SetActive(false);
   }

   public void DisplayGameOverText()
   {
      TxtGameOver.SetActive(true);
   }
}
