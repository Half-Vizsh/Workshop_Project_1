using System;
using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Prj_BaseProjectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb2d;
    [SerializeField] protected Collider2D Col;
    [SerializeField] protected float  Speed; public void setSpeed(float newSpeed) => this.Speed = newSpeed; public float getSpeed(){return this.Speed;}
    [SerializeField] protected float lifeTime; public void setLifeTime(float newLifetime) => this.lifeTime = newLifetime; public float getLifeTime() {return this.lifeTime;}
    [SerializeField] protected int baseDamage;
    protected Vector2 moveDirection;
    public virtual void OnEnable()
    {
        Invoke("Destroy",getLifeTime());
    }
    public virtual void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        Col = this.GetComponent<Collider2D>();
    } 
    public virtual void Update()
    {
        Moving();
    }
    public virtual void Moving ()
    {
        this.transform.Translate(moveDirection * Speed * Time.deltaTime);
    }
    public void setMovingDir(Vector2 newDir)
    {
        moveDirection = newDir;
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerSoul"))
        {
            other.GetComponent<Ply_Char_Base>().TakeDamage(baseDamage);
        }
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
    public virtual void OnDisable()
    {
         CancelInvoke();
    }
}
