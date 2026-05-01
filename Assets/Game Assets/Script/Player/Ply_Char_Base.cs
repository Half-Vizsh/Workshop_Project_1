using System;
using System.Collections;
using System.Security.Cryptography;
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
    private bool canHeal = true;
    private int currentHP;
    public static Action OnDefeated;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = MaxHP;
        visualScript = GetComponent<Ply_FlashEffect>();
        visualScript.UpdateBar(currentHP, MaxHP);
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
        visualScript.UpdateBar(currentHP, MaxHP);
        if (Mathf.Clamp(currentHP, 0, MaxHP) == 0)
        {
            visualScript.DeleteChar();
            Die();
        }
    }
    public IEnumerator HealCooldown()
    {
        //Waktu di enter, heal ke trigger berkali-kali jadinya dia heal langsung penuh, makanya dibikiin cd gini
        canHeal = false;
        yield return new WaitForSecondsRealtime(2f);
        canHeal = true;
    }
    public virtual void ReceiveHeal(int HealAmount)
    {
        if (!canHeal) return;
        currentHP = Mathf.Clamp(currentHP+HealAmount, 0, MaxHP);
        visualScript.doHealEffect();         
        visualScript.UpdateBar(currentHP, MaxHP);
        Debug.Log("Character Receive Heal: "+HealAmount+" HP left: "+this.currentHP);
        StartCoroutine(HealCooldown());
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
