using System;
using System.Collections;
using System.Collections.Generic;
using GlobalTypes;
using UnityEngine;

public class EnemyShip : FireableShip
{
    public const float EPSILON = 0.03f;
    
    public ShipClass Class;
    public ShipLookDirection LookDirection;
    public float MoveSpeed = 1;
    public EnemyWave Wave;

    public GameObject Explosion;
    
    Transform waypointList= null;
    int currentWaypoint = -1;
    PlayerShipController playerShipController;
    
    
  

    internal void TravelTo(Transform targetTransform)
    {
        waypointList = targetTransform;
        currentWaypoint = -1;
    }
    
    void Start()
    {
        lastFireTime = DateTime.Now;
        Init();
        playerShipController = FindObjectOfType<PlayerShipController>();

        OnDead = () =>
        {
            //TODO: Bir object pooldan çekilmeli
            var explosion = Instantiate(Explosion);
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
        };
    }

    
    void Update()
    {
        if (waypointList != null && currentWaypoint +1 < waypointList.childCount)
        {
            switch (LookDirection)
            {
                case ShipLookDirection.ToBottom:
                    transform.rotation = Quaternion.Euler(0,180,0);
                    break;
                case ShipLookDirection.ToPlayer:
                    transform.LookAt(playerShipController.transform);
                    break;
                case ShipLookDirection.ToNextWayPoint:
                    transform.LookAt(waypointList.GetChild(currentWaypoint + 1).transform.position);
                    break;
            }
            Vector3 direction = (waypointList.GetChild(currentWaypoint + 1).transform.position - transform.position);
            transform.position += direction.normalized * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(waypointList.GetChild(currentWaypoint + 1).transform.position, transform.position) < EPSILON)
            {
                currentWaypoint += 1;
                if (currentWaypoint +1 >= waypointList.childCount)
                {
                    currentWaypoint = -1;
                    waypointList = null;
                    RemoveFromScreen();
                }
            }
        }
        
        Fire();
    }

    private void RemoveFromScreen()
    {
        Wave.DestroyShip(gameObject);
    }
}
