using UnityEditor;
using UnityEngine;

public interface IDamageable
{
    public abstract void TakeDamage(int amount);
    public abstract void Die();
}   
