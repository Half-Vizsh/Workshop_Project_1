using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GH_BattleHandler : MonoBehaviour
{
    //This script Handle the turn base mechanic and player's input
    public static GH_BattleHandler Instance {get; private set;}
    public static Action OnWin;
    private IBattleState currentState;
    public St_PlayerTurn PlayerTurn {get; private set;}
    public St_EnemyTurn EnemyTurn {get; private set;}
    public St_ChooseTarget ChooseTargetTurn {get; private set;}
    
    [Header("Player Turn Handler")]
    [SerializeField] private GameObject options;    public void showOption(){this.options.SetActive(true);} public void hideOption(){this.options.SetActive(false);}
    public Button Ply_AttackButton; public Button Ply_HealButton;
    private GH_TargetHandler TargetHandler;
    private Ply_Char_Base playerScript;

    [Header("Enemy Turn Handler")]
    [SerializeField] private Ply_Soul_Move SoulScript; 
    [SerializeField] private GameObject MoveableArea;   public void showArea(){this.MoveableArea.SetActive(true);} public void hideArea(){this.MoveableArea.SetActive(false);}
    [SerializeField]private float SequenceTime;
    [SerializeField] private float AttackDelay;
    private void Awake()
    {
        if (Instance != null) {Destroy(gameObject); return;}
        Instance = this;
        PlayerTurn = new St_PlayerTurn(this);
        EnemyTurn = new St_EnemyTurn(this);
        ChooseTargetTurn = new St_ChooseTarget(this);        
        Ply_AttackButton.onClick.AddListener(onAttackButtonClick); //Event system
        Ply_HealButton.onClick.AddListener(onHealButtonClick);
    }
    private void Start()
    {
        TargetHandler = this.GetComponent<GH_TargetHandler>();
        TargetHandler.AttackConfirmed += onAttackButtonConfirmed; 
        TargetHandler.AttackCanceled += onAttackButtonCanceled;
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("PlayerSoul");
        playerScript = PlayerObj.GetComponent<Ply_Char_Base>();
        SoulScript = PlayerObj.GetComponent<Ply_Soul_Move>();
        
        StopAllAttacks();
        hideArea();

        ChangeState(PlayerTurn);
    }
    private void Update()
    {
        // //For debugging, changing state with space, delete this later
        // if (Keyboard.current.spaceKey.wasPressedThisFrame)
        // {
        //     if (currentState == PlayerTurn)ChangeState(EnemyTurn);
        //     else ChangeState(PlayerTurn);
        // }
        currentState?.onUpdate();
    }
    public void ChangeState(IBattleState nextState)
    {
        currentState?.onExit();
        currentState = nextState;
        currentState?.onEnter();
    }

    //# PLAYER PHASE
    //Where the player's option logic is handled
    public void onAttackButtonClick()
    {
        //
        Debug.Log("Attack Button has been pressed");
        TargetHandler.doTargetting();
    }
    public void onHealButtonClick()
    {
        Debug.Log("Heal Button has been pressed");
        playerScript.ReceiveHeal(20);   
    }
    //Where the targeting logic is handled
    public void onAttackButtonConfirmed(Emy_Base Target)
    {
        if (playerScript==null) return;
        Target.TakeDamage(playerScript.getTotalDamage());
        Debug.Log(Target.name+" diserang, darahnya berkurang menjadi: "+Target.getHP());
        //Tambahin delay neh disini
        ChangeState(EnemyTurn);//harusnya enemy, player buat test
    }
    public void onAttackButtonCanceled()
    {
        ChangeState(PlayerTurn);
    }
    public void Run_ReadingInput() => StartCoroutine(ReadingPlayerChoosing()); //Cuma buat run
    public IEnumerator ReadingPlayerChoosing()
    {
        Debug.Log("Waiting input");
        GameObject lastButtonSelected = EventSystem.current.currentSelectedGameObject;
        GameObject buttonSelected;
        while (true){ 
            buttonSelected =  EventSystem.current.currentSelectedGameObject;
            if (buttonSelected == null) EventSystem.current.SetSelectedGameObject(lastButtonSelected);
            else{lastButtonSelected = buttonSelected;} 

            if (Keyboard.current.enterKey.wasPressedThisFrame){ 
                if (buttonSelected == Ply_AttackButton.gameObject){
                    Ply_AttackButton.onClick.Invoke(); 
                    ChangeState(ChooseTargetTurn);
                    yield break;
                } 
                if (buttonSelected == Ply_HealButton.gameObject){
                    Ply_HealButton.onClick.Invoke();
                    Debug.Log("Heal has been pressed"); 
                    ChangeState(EnemyTurn);
                    yield break;   
                }
            }
            yield return null;
        }
    }

    //#ENEMY PHASE
    //Where the Hearth movement handled
    public void doMoveCent() => StartCoroutine(MovingHeartCen());
    private IEnumerator MovingHeartCen()
    {
        // SoulScript.enabled = true;
        SoulScript.StopAllCoroutines();
        SoulScript.showheart();
        SoulScript.doMoveToCenter();
        yield return new WaitForSecondsRealtime(0.75f);
  }
    public void doMoveBack() => StartCoroutine(MovingHeartOri());
    private IEnumerator MovingHeartOri()
    {
        SoulScript.StopAllCoroutines();
        SoulScript.doMoveToChar();
        yield return new WaitForSecondsRealtime(0.75f);
        SoulScript.hideHeart();
        // SoulScript.enabled =false;
    }
    //Enable Spawner
    public void StartAllAttacks(){
        foreach (Emy_Base enemy in TargetHandler.GetAllEnemies())
        {
            enemy.StartAttack();
        }
    }
    public void StopAllAttacks(){
        foreach (Emy_Base enemy in TargetHandler.GetAllEnemies())
        {
            enemy.StopAttack();
        }
    }
    //Attack Timing
    public void doAttackSequence() => StartCoroutine(AttackSequence());
    public IEnumerator AttackSequence()
    {
        yield return new WaitForSecondsRealtime(AttackDelay);
        StartAllAttacks();
        yield return new WaitForSecondsRealtime (SequenceTime);
        StopAllAttacks();
        doMoveBack();
        yield return new WaitForSecondsRealtime(AttackDelay);
        ChangeState(PlayerTurn);
    }
}
