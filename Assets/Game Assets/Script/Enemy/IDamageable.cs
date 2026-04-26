using UnityEditor;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int amount);
    public void Die();
}   
