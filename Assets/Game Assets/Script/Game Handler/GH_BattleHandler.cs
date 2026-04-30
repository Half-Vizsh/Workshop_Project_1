using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GH_BattleHandler : MonoBehaviour
{
    public static GH_BattleHandler Instance {get; private set;}
    private IBattleState currentState;
    public St_PlayerTurn PlayerTurn {get; private set;}
    public St_EnemyTurn EnemyTurn {get; private set;}
    public St_ChooseTarget ChooseTargetTurn {get; private set;}
    
    [Header("Player Turn Handler")]
    [SerializeField] private GameObject options;    public void showOption(){this.options.SetActive(true);} public void hideOption(){this.options.SetActive(false);}
    public Button Ply_AttackButton; public Button Ply_ItemButton;
    private GH_TargetHandler TargetHandler;
    private Ply_Char_Base playerScript;

    [Header("Enemy Turn Handler")]
    [SerializeField] private Ply_Soul_Move SoulScript; 
    [SerializeField] private GameObject MoveableArea;   public void showArea(){this.MoveableArea.SetActive(true);} public void hideArea(){this.MoveableArea.SetActive(false);}
    private void Awake()
    {
        if (Instance != null) {Destroy(gameObject); return;}
        Instance = this;
        PlayerTurn = new St_PlayerTurn(this);
        EnemyTurn = new St_EnemyTurn(this);
        ChooseTargetTurn = new St_ChooseTarget(this);
    }
    private void Start()
    {
        TargetHandler = this.GetComponent<GH_TargetHandler>();
        TargetHandler.AttackConfirmed += onAttackButtonConfirmed; 
        TargetHandler.AttackCanceled += onAttackButtonCanceled;
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("PlayerSoul");
        playerScript = PlayerObj.GetComponent<Ply_Char_Base>();
        SoulScript = PlayerObj.GetComponent<Ply_Soul_Move>();
        ChangeState(PlayerTurn);
    }
    private void Update()
    {
        //For debugging, changing state with space, delete this later
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (currentState == PlayerTurn)ChangeState(EnemyTurn);
            else ChangeState(PlayerTurn);
        }
        currentState?.onUpdate();
    }
    public void ChangeState(IBattleState nextState)
    {
        currentState?.onExit();
        currentState = nextState;
        currentState?.onEnter();
    }
    //Where the player's option logic is handled
    public void onAttackButtonClick()
    {
        //
        Debug.Log("Attack Button has been pressed");
        TargetHandler.doTargetting();
    }
    public void onItemButtonClick()
    {
        Debug.Log("Item Button has been pressed");
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
        Ply_AttackButton.onClick.AddListener(onAttackButtonClick); //Event system
        Ply_ItemButton.onClick.AddListener(onItemButtonClick);
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
                if (buttonSelected == Ply_ItemButton.gameObject){
                    Ply_ItemButton.onClick.Invoke();
                    Debug.Log("Item has been pressed"); 
                    ChangeState(EnemyTurn);
                    yield break;   
                }
            }
            yield return null;
        }
    }
    //Where the Hearth movement handled
    public void doMoveCent() => StartCoroutine(MovingHeartCen());
    private IEnumerator MovingHeartCen()
    {
        SoulScript.showheart();
        yield return StartCoroutine(SoulScript.MoveToCenter());
    }
    public void doMoveBack() => StartCoroutine(MovingHeartOri());
    private IEnumerator MovingHeartOri()
    {
        yield return StartCoroutine(SoulScript.MoveToChar());
        SoulScript.hideHeart();
    }
}
