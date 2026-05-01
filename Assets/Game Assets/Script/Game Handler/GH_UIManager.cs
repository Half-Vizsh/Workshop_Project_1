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
    private GH_TargetHandler ThScript; //Awalnya event diakses static, tpi bikin error pas pindah-pindah scene, makanya jadi reference
    private Ply_Char_Base PlyScript;
    private bool gameEnded;  
    private bool gamePaused;
    private GameObject lastButtonPressed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlyScript = GameObject.FindGameObjectWithTag("PlayerSoul").GetComponent<Ply_Char_Base>();
        PlyScript.OnDefeated += OnLose;
        ThScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GH_TargetHandler>();
        ThScript.OnWin += OnWin;
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
        UIText.text = "Pause";
        BackgroundPanel.color = new Color32 (0,0,0,150);
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
        EventSystem.current.SetSelectedGameObject(ReturnToMainMenu.gameObject); 
        Time.timeScale = 0f;   
        gameEnded = true;
        MenuUI.SetActive(true);
        UIText.text = "Game Over";
        BackgroundPanel.color = new Color32 (255, 64, 86, 159);
        ThScript.OnWin -= OnWin;
        PlyScript.OnDefeated -=OnLose;
    }
    void OnWin()
    {
        if (gamePaused) onResume();
        EventSystem.current.SetSelectedGameObject(ReturnToMainMenu.gameObject); 
        Time.timeScale = 0f;   
        gameEnded = true;
        MenuUI.SetActive(true);
        UIText.text = "Game Ended";
        BackgroundPanel.color = new Color32 (255,193,64,159);
        ThScript.OnWin -= OnWin;
        PlyScript.OnDefeated -=OnLose;    }    
}
