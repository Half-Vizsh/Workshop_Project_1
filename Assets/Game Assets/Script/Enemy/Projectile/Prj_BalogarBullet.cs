using UnityEngine;

public class Prj_BalogarBullet : Prj_BaseProjectile
{
    [SerializeField] private float MaxSpeed; 
    [SerializeField] private float Acceleration;
    private float currentSpeed = 0;
    public override void OnEnable(){}
    public override void Start()
    {
        base.Start();
        setMovingDir(new Vector2(this.moveDirection.x,-1f));
        Destroy(gameObject, lifeTime);
    }
    public override void Update()
    {
        currentSpeed += getSpeed()*Acceleration*Time.deltaTime;
        Speed = Mathf.Min(currentSpeed, MaxSpeed);
        Moving();
    }
    public override void Moving()
    {
        base.Moving();
    }
}
