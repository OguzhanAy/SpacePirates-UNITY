using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    public Action OnInvisible = null;
    bool isVisible = false;
    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        Debug.Log("Çalışıyor mu");
        if (isVisible)
        {
            if (OnInvisible != null)
            {                
                OnInvisible.Invoke();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
}


