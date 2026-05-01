using TMPro;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GH_UIManager : MonoBehaviour
{
    [SerializeField]private GameObject MenuUI;
    [SerializeField]private Image BackgroundPanel;
    [SerializeField] private TextMeshProUGUI UIText;
    [SerializeField] private Button AttackButton;
    [SerializeField]private Button ReturnToMainMenu;
    private bool gameEnded;  
    private bool gamePaused;
    private GameObject lastButtonPressed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ply_Char_Base.OnDefeated += OnLose;
        GH_BattleHandler.OnWin += OnWin;
        MenuUI.SetActive(false);
        Time.timeScale = 1f;   
        EventSystem.current.SetSelectedGameObject(AttackButton.gameObject); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (gamePaused) onResume();
            else onPause();
        }
    }
    void onPause()
    {
        if(gameEnded) return;
        lastButtonPressed = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(ReturnToMainMenu.gameObject); 
        gamePaused = true;
        MenuUI.SetActive(true);
        BackgroundPanel.color = new Color (0,0,0,20);
        Time.timeScale = 0f;   
    }
    void onResume()
    {
        MenuUI.SetActive(false);
        Time.timeScale = 1f;   
        gamePaused = false;
        if (lastButtonPressed!=null) EventSystem.current.SetSelectedGameObject(lastButtonPressed.gameObject);
        else EventSystem.current.SetSelectedGameObject(AttackButton.gameObject);
    }
    void OnLose()
    {
        if (gamePaused) onResume();
        gameEnded = true;
        MenuUI.SetActive(true);
        UIText.text = "Game Over";
        BackgroundPanel.color = new Color (255, 64, 86, 159);
    }
    void OnWin()
    {
        if (gamePaused) onResume();
        gameEnded = true;
        MenuUI.SetActive(true);
        UIText.text = "Game Ended";
        BackgroundPanel.color = new Color (255,193,64,159);
    }    
}
