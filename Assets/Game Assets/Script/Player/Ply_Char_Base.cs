using System;
using UnityEngine;

public class Ply_Char_Base : MonoBehaviour, IDamageable
{
    [SerializeField]private int baseDamage; 
    [SerializeField]private int MaxHP;
    private int currentHP;
    public Action OnDefeated;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual int getTotalDamage()
    {
        return baseDamage;//Can be modified later on
    }
    public virtual void TakeDamage(int DamageAmount)
    {
        currentHP-=DamageAmount;
        if (Mathf.Clamp(currentHP, 0, MaxHP) == 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        OnDefeated.Invoke();
        Destroy(gameObject);
    }
}
