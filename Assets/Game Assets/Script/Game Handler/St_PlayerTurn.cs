using UnityEngine;

public class St_PlayerTurn : IBattleState
{
    private readonly GH_BattleHandler ctx;
    public St_PlayerTurn(GH_BattleHandler context)
    {
        this.ctx = context;
    }
    public void onEnter()
    {
        Debug.Log("Entering Player Turn");
        ctx.showOption();
        ctx.Run_ReadingInput();
    }
    public void onUpdate()
    {
        // Debug.Log("Executing Player Turn");
        // ctx.ChangeState(ctx.EnemyTurn);
    }
    public void onExit()
    {
        Debug.Log("Exiting Player Turn");
        ctx.hideOption();
    }
}
