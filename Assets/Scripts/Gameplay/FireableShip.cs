using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireableShip : MonoBehaviour
{
    //Ateşle ilgili değişkenler
    public float ParticleSpeed = 10;
    public float FireFrequency = 0.5f;
    public GameObject FireParticle;
    public int HP;
    public int Damage = 1;
    int remainingHP;
    bool isDead = false;
    public LevelManagers LevelMgr;
    
    public Action OnDead = null;
    public Action<int> OnLifeChanged = null;

    protected DateTime lastFireTime = DateTime.MinValue;

    public bool IsDead
    {
        get { return isDead; }
    }
    public int RemainingHP
    {
        get
        {
            return remainingHP;
        }
    }

    public void Init()
    {
        remainingHP = HP;
        isDead = false;
    }

    public void Fire()
    {
        Debug.Log("ss");
        if (!isDead && (DateTime.Now - lastFireTime).TotalSeconds >= FireFrequency)
        {
            var particle = LevelMgr.Get(FireParticle.name,() =>
            {
                return Instantiate(FireParticle);
            });
            
            particle.transform.position = transform.position;
            particle.transform.rotation = Quaternion.identity;
            var particleFlyer = particle.GetComponent<ParticleFlyer>();
            
            particle.SetActive(true);
            particleFlyer.ParticleSpeed = ParticleSpeed;
            particleFlyer.LevelMgr = LevelMgr;
            particleFlyer.ParticleName = FireParticle.name;
            particleFlyer.Damage = Damage;
            particleFlyer.TargetTag = gameObject.tag == "Enemy" ? "Player" : "Enemy";
            
            lastFireTime = DateTime.Now;  
        }
    }
    
    // gemi vurulduğunda
    public void GotDamage(int amount)
    {
        remainingHP -= amount;
        if (remainingHP <= 0)
        {
            isDead = true;
            OnDead?.Invoke();
        }
        else
        {
            OnLifeChanged?.Invoke(amount);
        }
    }
    
    
    
}
