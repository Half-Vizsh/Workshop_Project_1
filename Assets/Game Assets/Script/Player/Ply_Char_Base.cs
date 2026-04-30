using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Ply_Char_Base : MonoBehaviour, IDamageable
{
    [SerializeField]private int baseDamage; 
    [SerializeField]private int MaxHP;
    [SerializeField] private float IFrame;
    private Ply_FlashEffect visualScript;
    private float currentFrame;
    private bool IsDead = false;
    private bool DamageAble = true;
    private int currentHP;
    public Action OnDefeated;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = MaxHP;
        visualScript = GetComponent<Ply_FlashEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFrame>0.01f) DamageAble = false;
        else DamageAble = true;
        currentFrame-=Time.deltaTime;
    }
    public virtual int getTotalDamage()
    {
        return baseDamage;//Can be modified later on
    }
    public virtual void TakeDamage(int DamageAmount)
    {
        if (!DamageAble) return;
        currentHP = Mathf.Clamp(currentHP-DamageAmount, 0, MaxHP); 
        Debug.Log("Character Taking damage: "+DamageAmount+" HP left: "+this.currentHP);
        currentFrame = IFrame;
        visualScript.doFlash();
        if (Mathf.Clamp(currentHP, 0, MaxHP) == 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        if(IsDead) return;
        Debug.Log(this.name+" is destoryed");
        OnDefeated?.Invoke();
        Destroy(gameObject);
        IsDead = true;
    }
}
