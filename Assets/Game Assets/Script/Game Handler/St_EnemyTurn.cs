using UnityEngine;

public class St_EnemyTurn : IBattleState
{
    private readonly GH_BattleHandler ctx;
    public St_EnemyTurn(GH_BattleHandler context)
    {
        this.ctx = context;
    }
    public void onEnter()
    {
        Debug.Log("Entering Enemy Turn");
    }
    public void onUpdate()
    {
        Debug.Log("Executing Enemy Turn");
    }
    public void onExit()
    {
        Debug.Log("Exiting Enemy Turn");
    }
}
