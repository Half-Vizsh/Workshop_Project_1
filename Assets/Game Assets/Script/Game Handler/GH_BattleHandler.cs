using UnityEngine;
public class GH_BattleHandler : MonoBehaviour
{
    public static GH_BattleHandler Instance {get; private set;}
    private IBattleState currentState;
    public St_PlayerTurn PlayerTurn {get; private set;}
    public St_EnemyTurn EnemyTurn {get; private set;}

    private void Awake()
    {
        if (Instance != null) {Destroy(gameObject); return;}
        Instance = this;

        PlayerTurn = new St_PlayerTurn(this);
        EnemyTurn = new St_EnemyTurn(this);
    }
    private void Start()
    {
        ChangeState(PlayerTurn);
    }
    private void Update()
    {
        currentState?.onUpdate();
    }
    private void ChangeState(IBattleState nextState)
    {
        currentState?.onExit();
        currentState = nextState;
        currentState?.onEnter();
    }
}
