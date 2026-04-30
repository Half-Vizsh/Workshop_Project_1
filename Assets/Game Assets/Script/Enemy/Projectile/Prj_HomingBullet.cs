using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviourScript : Prj_BaseProjectile
{
    private Transform PlayerPos;
    [SerializeField]private float homingSpeed; //Speed during homing
    [SerializeField]private float secToHoming;
    private float realSpeed = 2f;
    public override void OnEnable()
    {
        base.OnEnable();
        setSpeed(realSpeed);
        Invoke("Homing", secToHoming);
    }
    override public void Start()
    {
        base.Start();
        realSpeed = this.getSpeed();
        PlayerPos = GameObject.FindGameObjectWithTag("PlayerSoul").GetComponent<Transform>();
    }

    // Update is called once per frame
    override public void Update()
    {
        base.Update();
    }
    public void Homing()
    {
        Vector2 homingDir = (PlayerPos.position - transform.position).normalized; //Dapetin dir baru
        setSpeed(homingSpeed);
        setMovingDir(homingDir);   
    }
}
