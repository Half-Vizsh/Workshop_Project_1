using UnityEngine;

public class St_ChooseTarget : IBattleState
{
    private readonly GH_BattleHandler ctx;
    public St_ChooseTarget(GH_BattleHandler context)
    {
        this.ctx = context;
    }
    public void onEnter()
    {
        Debug.Log("Entering Targeting Phase");
        ctx.hideOption();
    }
    public void onUpdate()
    {
        // Debug.Log("Executing Player Turn");
        // ctx.ChangeState(ctx.EnemyTurn);
    }
    public void onExit()
    {
        Debug.Log("Exiting Targeting Phase Turn");
    }
}
