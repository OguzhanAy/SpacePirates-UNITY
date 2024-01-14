using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{
    public delegate GameObject CreateNewObjectDelegate();
    private List<GameObject> objects = new List<GameObject>();
    

    public void Add(GameObject newObject)
    {
        objects.Add(newObject);
    }

    public GameObject Get(CreateNewObjectDelegate onNotPresent)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].activeSelf)
            {
               //objects[i].SetActive(true);
               return objects[i];
            }
        }
        //Eğer uygun durumda yoksa

        GameObject newObject = onNotPresent?.Invoke();
        objects.Add(newObject);

        return newObject;
    }

    public void DestroyObject(GameObject obj)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == obj)
            {
                objects[i].SetActive(false);
                return;
            }
        }
        Debug.LogWarning("Given item is not present in the object pool");
    }
}
