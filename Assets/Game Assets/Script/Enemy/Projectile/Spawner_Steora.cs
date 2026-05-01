using System.Collections;
// using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public enum SpawnerType {
    Circle, 
    Line
    }
public class Spawner_Steora: Spawner_Base
{
    //Projectile Handler, ngikutin tutorial Alexander Zotov
    [Header("General Settings")]
    [SerializeField] private SpawnerType currentType;
    [SerializeField] private int bulletsAmount;
    [SerializeField] private float fireRate;
    [SerializeField] private float startAngle =0, endAngle=360;
    private Vector2 bulletMoveDirection;
    [Header("Type Settings")]
    [SerializeField] private GameObject HomingBullet;
    [SerializeField]private float homingFireRate;
    [SerializeField]private int homingAmount;
    [SerializeField] private GameObject RegularBullet;
    [SerializeField]private float regularFireRate;
    [SerializeField]private int regularAmount;
    [SerializeField]private float rotationSpeed;
    private bool shouldRotate;
    protected override void Start()
    {
        base.Start();
        SpawnerSprite = GetComponent<SpriteRenderer>();
    }
    protected override void Update()
    {
        if (shouldRotate)  transform.Rotate(0,0,rotationSpeed*Time.fixedDeltaTime);
        // else if (transform.rotation.z!=0) transform.rotation = quaternion.identity;
    }
    public override void StartSpawning()
    {
        base.StartSpawning();
        int typeRandom = UnityEngine.Random.Range(0,2);
        SpawnerSprite.color = Color.white;
        if (typeRandom == 0) currentType = SpawnerType.Circle;
        else currentType = SpawnerType.Line;
        StartCoroutine(Blasting());
        
    }
    public override void StopSpawning()
    {
        base.StopSpawning();
        StopAllCoroutines();
        PH_ObjPooling.objPoolInstance?.RemovePooledBullet();
    }
    private IEnumerator Blasting()
    {
        if (!canAttack) yield return null;
        while (true){
            CheckBehaviour();
            float angleStep = (endAngle-startAngle)/bulletsAmount;
            float angle = startAngle;       
            for (int i = 0; i<bulletsAmount+1;i++)
            {
                float bulDirX = transform.position.x + Mathf.Sin((angle*Mathf.PI)/180f);
                float bulDirY = transform.position.y + Mathf.Cos((angle*Mathf.PI)/180f);
                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY,0f);
                Vector2 bulDir = (bulMoveVector-transform.position).normalized;
                GameObject bul = PH_ObjPooling.objPoolInstance.GetBulett();
                    bul.transform.position = transform.position;
                    bul.transform.rotation = transform.rotation;
                    bul.SetActive(true);
                    bul.GetComponent<Prj_BaseProjectile>().setMovingDir(bulDir);
                angle += angleStep;
            }
            yield return new WaitForSeconds(fireRate);
        }
    }
    private void CheckBehaviour()
    {
        switch (currentType)
        {
                case SpawnerType.Circle:
                    transform.rotation = quaternion.identity;
                     this.fireRate = homingFireRate;
                     this.bulletsAmount = homingAmount;
                     PH_ObjPooling.objPoolInstance.setPooledBullet(HomingBullet);
                     shouldRotate = false;
                     break;
                case SpawnerType.Line:
                    this.fireRate = regularFireRate;
                    this.bulletsAmount = regularAmount;
                     PH_ObjPooling.objPoolInstance.setPooledBullet(RegularBullet);
                     shouldRotate = true;
                    break;
        }
    }
}
