using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCheck : MonoBehaviour
{
   
    void Start()
    {
        if (SettingsManager.Instance == null)
        {
            SceneManager.LoadScene(0);
        }
    }

   
}
