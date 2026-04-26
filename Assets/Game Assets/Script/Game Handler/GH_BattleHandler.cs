using System.Collections;
using Unity.VisualScripting;
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
    [Header("Player Turn Handler")]
    [SerializeField] private GameObject options;    public void showOption(){this.options.SetActive(true);} public void hideOption(){this.options.SetActive(false);}
    public Button Ply_AttackButton;
    public Button Ply_ItemButton;
    [Header("Enemy Turn Handler")]
    [SerializeField] private GameObject Playerheart;    public void showHeart(){this.Playerheart.SetActive(true);} public void hideHeart(){this.Playerheart.SetActive(false);}
    [SerializeField] private GameObject MoveableArea;   public void showArea(){this.MoveableArea.SetActive(true);} public void hideArea(){this.MoveableArea.SetActive(false);}
    private void Awake()
    {
        if (Instance != null) {Destroy(gameObject); return;}
        Instance = this;

        PlayerTurn = new St_PlayerTurn(this);
        EnemyTurn = new St_EnemyTurn(this);
    }
    private void Start()
    {
        ChangeState(EnemyTurn);
    }
    private void Update()
    {
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
    //Where the player's turn logic is being handled
    public void onAttackButtonClick()
    {
        Debug.Log("Attack Button has been pressed");
    }
    public void onItemButtonClick()
    {
        Debug.Log("Item Button has been pressed");
    }
    public void Run_ReadingInput()
    {
        StartCoroutine(_ReadingPlyInput()); //Cuma buat run
    }
    public IEnumerator _ReadingPlyInput()
    {
        Debug.Log("Waiting input");
        Ply_AttackButton.onClick.AddListener(onAttackButtonClick);
        Ply_ItemButton.onClick.AddListener(onItemButtonClick);
        while (true){ 
            GameObject buttonSelected = EventSystem.current.currentSelectedGameObject;
            if (Keyboard.current.enterKey.wasPressedThisFrame){ 
                if (buttonSelected == Ply_AttackButton.gameObject){
                    Ply_AttackButton.onClick.Invoke(); 
                    Debug.Log("Attack has been pressed");
                    ChangeState(EnemyTurn);
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
}
