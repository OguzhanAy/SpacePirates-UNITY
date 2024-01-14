using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LevelManagers : MonoBehaviour
{
    public SOLevelData LevelData;
    public PlayerShipController ShipController;

    Dictionary<string, ObjectPooler> levelPooler = new Dictionary<string, ObjectPooler>();
    GameObject levelPrefab = null;
   
    void Start()
    {
        LoadPrefab();
    }

    public void AddToPooler(string name, GameObject newObject)
    {
        if (!levelPooler.ContainsKey(name))
        {
            levelPooler.Add(name, new ObjectPooler());
        }
        
        levelPooler[name].Add(newObject);
        
    }

    public GameObject Get(string name, ObjectPooler.CreateNewObjectDelegate onNotPresent)
    {
        if (!levelPooler.ContainsKey(name))
        {
            levelPooler.Add(name, new ObjectPooler());
        }

        return levelPooler[name].Get(onNotPresent);
    }

    public void DestroyObject(string name, GameObject obj)
    {
        if (!levelPooler.ContainsKey(name))
        {
            return;
        }
        
        levelPooler[name].DestroyObject(obj);
        
    }

    private void LoadPrefab()
    {
        //prefabı yükle
        var prefab = Resources.Load<GameObject>("LevelBackgrounds/" + LevelData.LevelPrefab);

        levelPrefab = Instantiate(prefab);
        levelPrefab.transform.position = LevelData.StartPosition;

        for (int i = 0; i < levelPrefab.transform.childCount; i++)
        {
            if (levelPrefab.transform.transform.GetChild(i).GetComponent<ParticleDestroyer>() == null)
            {
                levelPrefab.transform.transform.GetChild(i).AddComponent<ParticleDestroyer>();
                
            }
        }
        
        // Skybox materyalini yükle
        RenderSettings.skybox = LevelData.SkyBoxMaterial;
        
        ShipController.StartAction();
    }
}
