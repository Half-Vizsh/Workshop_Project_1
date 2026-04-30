using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PH_ObjPooling : MonoBehaviour
{
    //This object pooling and the Spawner belong to Steora
    public static PH_ObjPooling objPoolInstance;
    [SerializeField] private GameObject pooledBullet;
    private bool needMoreBullet = true;
    private List<GameObject> bullets; //ini buat poolingnya
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        objPoolInstance = this;
    }
    private void Start()
    {
        bullets = new List<GameObject>();
    }
    public void setPooledBullet(GameObject NewBullet)
    {
        if (NewBullet == pooledBullet) return;
        for (int i = 0; i < bullets.Count; i++)
            {
                    Destroy(bullets[i]);
            }
        bullets.Clear();
        this.pooledBullet = NewBullet;
    }
    public GameObject GetBulett()
    {
        if (bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }
        if (needMoreBullet)
        {
            GameObject bul = Instantiate (pooledBullet, this.transform.position, quaternion.identity);
            bul.SetActive(false);
            bullets.Add(bul);
            return bul;
        }
        return null;
    } 
}
