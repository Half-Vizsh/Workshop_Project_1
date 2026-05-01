using UnityEngine;
using UnityEngine.SceneManagement;

public class GH_ButtonUIManager : MonoBehaviour
{
        public void onMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
    }
    public void onPlayButtonClicked()
    {
        SceneManager.LoadScene("MainBattleScene",LoadSceneMode.Single);
    } 
    public void onExitButtonClicked()
    {
           #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
