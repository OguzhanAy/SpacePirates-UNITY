using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public EnemyShip[] Ships;
    public Color DebugColor = Color.yellow;
    public float DebugSize = .5f;
    public float MoveSpeed = 10f;
    public LevelManagers LevelMgr;
    
    // Gemilerin ne kadar zaman aralığıyla geleceği
    public float AppearInterval = 1;
    
    //Oyuncuya ne kadar mesafe kalınca gemileri uçmaya başlayacağı
    public float StartDistance = 7;

    GameObject Controller;
    private bool isStarted = false;
    
    
    void Start()
    {
        Controller = Camera.main.gameObject;
    }

    
    void Update()
    {
        if (!isStarted && Mathf.Abs(Controller.transform.position.z - transform.position.z) < StartDistance)
        {
            StartTheWave();
        }
    }

    private void StartTheWave()
    {
        isStarted = true;
        StartCoroutine(StartWaveInTheBackground());
    }

    IEnumerator StartWaveInTheBackground()
    {
        GameObject nextShip;
        EnemyShip enemyShip;
        for (int i = 0; i < Ships.Length; i++)
        {
            nextShip = LevelMgr.Get(Ships[i].name, () =>
            {
                return Instantiate(Ships[i].gameObject, null);
            });

            nextShip.name = Ships[i].name;
            nextShip.SetActive(true);
            nextShip.transform.position = transform.position;
            enemyShip = nextShip.GetComponent<EnemyShip>();
            enemyShip.MoveSpeed = MoveSpeed;
            enemyShip.LevelMgr = LevelMgr;
            enemyShip.Wave = this;
            enemyShip.TravelTo(transform);
            
            yield return new WaitForSeconds(AppearInterval);
        }
    }

    public void DestroyShip(GameObject go)
    {
        LevelMgr.DestroyObject(go.name, go);
    }

    private void OnDrawGizmos()
    {
        Transform prev = transform;
        Gizmos.color = DebugColor;
        Gizmos.DrawSphere(prev.position, DebugSize);
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(prev.position, transform.GetChild(i).position);
            
            prev = transform.GetChild(i);
            
            Gizmos.color = DebugColor;
            Gizmos.DrawSphere(prev.position, DebugSize);
        }
    }
}
