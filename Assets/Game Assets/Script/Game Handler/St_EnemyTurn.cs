using UnityEngine;

public class St_EnemyTurn : IBattleState
{
    private readonly GH_BattleHandler ctx; //Ini buat ngasih konteks ke scriptnya supaya bisa pake ctx.changestate() atau akses objek yang diinisialisasi di BattleHandler
    //private readonly  spesifikscript scriptnya, nanti masukin aja di konstruktor trs pas new tambahin dah tuh butuh apa, jadi kita ga perlu ctx.script
    public St_EnemyTurn(GH_BattleHandler context)
    {
        this.ctx = context;
    }
    public void onEnter()
    {
        Debug.Log("Entering Enemy Turn");
        ctx.doMoveCent();
        ctx.showArea();
        ctx.doAttackSequence();
    }
    public void onUpdate()
    {
        // Debug.Log("Executing Enemy Turn");
        // ctx.ChangeState(ctx.PlayerTurn);
    }
    public void onExit()
    {
        Debug.Log("Exiting Enemy Turn");
        // ctx.doMoveBack();
        ctx.hideArea();
    }
}
