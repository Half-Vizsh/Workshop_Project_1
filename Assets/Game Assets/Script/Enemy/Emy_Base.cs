using Unity.VisualScripting;
using UnityEngine;

public class Emy_Base : MonoBehaviour, IDamageable
{
    protected int currentHP;
    [SerializeField] protected int MaxHP = 100;
    [SerializeField] private  SpriteRenderer TargetCursor;
    private GH_TargetHandler GH_TH_Script;
    public virtual void Awake  ()
    {
        this.tag = "Selectable";
        currentHP = MaxHP;
        GH_TH_Script = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GH_TargetHandler>();
        GH_TH_Script.addTarget(this);
        hideCursor();
    }
    public virtual void Update()
    {
        
    }
    //#Damaging
    public virtual  void TakeDamage(int amount)
    {
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
        Destroy(gameObject);
    }
    //#Targetting
    public virtual void onChoosen()
    {
        //Nampilin kursor gitu lah harusnya
        Debug.Log(this.name+" is being choosen");
        showCursor();
    }
    public virtual void onLeave()
    {
        Debug.Log(this.name+"Leave the shit");
        hideCursor();
    }
    public virtual void OnConfirm()
    {
        //Disini trigger damaging
        Debug.Log("This shit is executed");
        showCursor();
    }
    public void showCursor() => TargetCursor.color = new Color(1f,1f,1f,1f);
    public void hideCursor() => TargetCursor.color = new Color(1f,1f,1f,0f);
}
