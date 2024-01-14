using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFlyer : ParticleDestroyer
{
    public int Damage = 1;
    public float ParticleSpeed = 10;
    public LevelManagers LevelMgr = null;
    public string TargetTag = "Enemy";
    public string ParticleName = String.Empty;

    private void Start()
    {
        OnInvisible = () =>
        {
            DestroyFromThePool();
        };
    }

    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * ParticleSpeed;
    }

    public void DestroyFromThePool()
    {
        LevelMgr?.DestroyObject(ParticleName,gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TargetTag)
        {
            other.GetComponent<FireableShip>()?.GotDamage(Damage);
        }
    }

   
}
