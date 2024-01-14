using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float Speed;

    public bool CanMove = true;
  
    
    void Update()
    {
        if (CanMove)
        {
            transform.position += Vector3.forward * Time.deltaTime * Speed;
        }
    }
}
