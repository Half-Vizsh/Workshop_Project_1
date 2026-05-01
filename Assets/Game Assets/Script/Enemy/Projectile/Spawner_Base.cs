using System;
using UnityEngine;

public class Spawner_Base : MonoBehaviour
{
    [SerializeField] protected bool canAttack = false;
    [SerializeField]protected SpriteRenderer SpawnerSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        StopSpawning();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    public virtual void StartSpawning()
    {
        canAttack = true;            
        if (SpawnerSprite!=null) SpawnerSprite.color = Color.white;
    }
    public virtual void StopSpawning()
    {
        canAttack = false;
        if (SpawnerSprite!=null)SpawnerSprite.color = new Color(1f,1f,1f,0f);
    }
}
