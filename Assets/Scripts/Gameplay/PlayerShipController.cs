using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerShipController : FireableShip
{
    //Hareketle ilgili değişkenler
    public float MoveSpeed = 1;
    public float RotationAmount = 10;
    public GameObject Ship;
    public FixedJoystick Joystick;
    public Color HitColor = new Color(1, .33f, .33f, 1);
    
    public Image UIArrowLeft;
    public Image UIArrowRight;
    public Image UIArrowTop;
    public Image UIArrowBottom;

    public UIGameScreen UI;

    bool actionStarted = false;
    
    public void StartAction()
    {
        Init();
        UIArrowRight.color = new Color(255, 255, 255, 0);
        UIArrowLeft.color = new Color(255, 255, 255, 0);
        UIArrowTop.color = new Color(255, 255, 255, 0);
        UIArrowBottom.color = new Color(255, 255, 255, 0);

        OnDead = () =>
        {
            UI.DisplayGameOverText();
            gameObject.SetActive(false);
        };
        
        OnLifeChanged = (life) =>
        {
            if (life == 0)
            {
                return;
            }
            
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = HitColor;
                renderers[i].material.DOColor(Color.white, .25f).SetEase(Ease.Linear);
            }
        };

        actionStarted = true;

    }

    

   

   
    void Update()
    {
        if (!actionStarted)
        {
            return;
        }
        
         Vector3 targetPosition = (Ship.transform.localPosition) + Vector3.forward * Joystick.Vertical * Time.deltaTime * MoveSpeed + Vector3.right *Joystick.Horizontal *Time.deltaTime * MoveSpeed;

         if (targetPosition.x < -5)
         {
             targetPosition.x = -5;
         }
         if (targetPosition.z < -2)
         {
             targetPosition.z = -2;
         }
         if (targetPosition.x >5)
         {
             targetPosition.x = 5;

         }
         if (targetPosition.z >2)
         {
             targetPosition.z = 2;

         }
         Ship.transform.localPosition = targetPosition;

         if (Joystick.Horizontal <0)
         {
             Ship.transform.DOLocalRotate(new Vector3(0, 0, RotationAmount), 0.1f).SetEase(Ease.Linear);
         }else if (Joystick.Horizontal > 0)
         {
             Ship.transform.DOLocalRotate(new Vector3(0, 0, -RotationAmount), 0.1f).SetEase(Ease.Linear);

         }else
         {
             Ship.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f).SetEase(Ease.Linear);

         }
         
         //UI Effect
         UIArrowRight.color = new Color(255, 255, 255, Joystick.Horizontal <= 0 ? 0: Joystick.Horizontal);
         UIArrowLeft.color = new Color(255, 255, 255, Joystick.Horizontal >= 0 ? 0: -Joystick.Horizontal);
         UIArrowTop.color = new Color(255, 255, 255, Joystick.Vertical <= 0 ? 0: Joystick.Vertical);
         UIArrowBottom.color = new Color(255, 255, 255, Joystick.Vertical >= 0 ? 0: -Joystick.Vertical);
         
    }
}
