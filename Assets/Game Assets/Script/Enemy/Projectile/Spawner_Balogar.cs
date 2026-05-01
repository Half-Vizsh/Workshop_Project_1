using UnityEngine;

public class Spawner_Balogar : Spawner_Base
{
    [SerializeField] private float speed;
    [SerializeField]private float Cooldown;
    [SerializeField] private GameObject SwordObject;
    [SerializeField] private GameObject SwordPoint;
    [SerializeField] private float leftBound  = 2f;
    [SerializeField] private float rightBound = 7f;
    private int _direction = 1;
    private float currentCD;
    //Ni spawner simpel aja yah, otak gw dah ngebul
    protected override void Start()
    {
        SpawnerSprite = GetComponentInChildren<SpriteRenderer>();
        base.Start();
    }
    protected override void Update()
    {
        Patroling();
        if (currentCD<=0)
        {
            Shoot();
            currentCD = Cooldown;
        } 
        currentCD-=Time.deltaTime;
    }
    public void Patroling()
    {
     transform.position += Vector3.right * _direction * speed * Time.deltaTime;
        if (transform.position.x >= rightBound) _direction = -1;
        if (transform.position.x <= leftBound)  _direction =  1;
    }
    public void Shoot()
    {
        if (!canAttack) return;
        Vector3 SpawnPoint = SwordPoint.transform.position;
        Instantiate(SwordObject, SpawnPoint, Quaternion.identity);
    }
}
