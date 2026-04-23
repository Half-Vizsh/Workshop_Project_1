using UnityEngine;

public interface IBattleState
{
    public void onEnter();
    public void onUpdate(); //Fixed update kekya ga perlu soalnya ga pake physics
    public void onExit();
}
