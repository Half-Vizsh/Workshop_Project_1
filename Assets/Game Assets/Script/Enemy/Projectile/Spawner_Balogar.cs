using UnityEngine;

public class Spawner_Balogar : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField]private float Cooldown;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private GameObject SwordObject;
    [SerializeField] private GameObject SwordPoint;
    private Transform target;
    private float currentCD;
    //Ni spawner simpel aja yah, otak gw dah ngebul
    void Start()
    {
        target = pointB;
    }
    void Update()
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
        transform.position = Vector2.MoveTowards(transform.position,target.position,speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target.position) < 0.05f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }
    public void Shoot()
    {
        Vector3 SpawnPoint = SwordPoint.transform.position;
        Instantiate(SwordObject, SpawnPoint, Quaternion.identity);
    }
}
