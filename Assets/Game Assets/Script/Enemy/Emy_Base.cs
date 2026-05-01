using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Emy_Base : MonoBehaviour, IDamageable
{
    protected int currentHP; public int getHP(){return currentHP;}
    [SerializeField] protected int MaxHP = 100;
    [SerializeField] protected Spawner_Base SpawnerChild;
    protected GH_TargetHandler GH_TH_Script;
    protected Emy_Visual VisualScript;
    public virtual void Awake  ()
    {
        this.tag = "Selectable";
        currentHP = MaxHP;
        GH_TH_Script = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GH_TargetHandler>();
        GH_TH_Script.addTarget(this);
        VisualScript = this.GetComponent<Emy_Visual>();
        this.SpawnerChild = GetComponentInChildren<Spawner_Base>();
    }
    public virtual void Start(){}
    public virtual void Update(){}
    public virtual void StartAttack()
    {
        SpawnerChild?.StartSpawning();
    }
    public virtual void StopAttack()
    {
        SpawnerChild?.StopSpawning();
    }
    //#Damaging
    public virtual  void TakeDamage(int amount)
    {
        VisualScript.doFlash();
        currentHP-=amount;
        if (Mathf.Clamp(currentHP, 0, MaxHP) == 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        Debug.Log ("Object Die");
        GH_TH_Script.remTarget(this);
        StopAttack();
        Destroy(gameObject, 0.5f);
    }
    //Targetting Visual Effect
    public virtual void onChoosen()
    {
        // Debug.Log(this.name+" is being choosen");
        VisualScript.showCursor();
    }
    public virtual void onLeave()
    {
        // Debug.Log(this.name+"Leave the shit");
        VisualScript.hideCursor();
    }
    public virtual void OnConfirm()
    {
        // Debug.Log("This shit is executed");
        VisualScript.hideCursor();
    }
}
