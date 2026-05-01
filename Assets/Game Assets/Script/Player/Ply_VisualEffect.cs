using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ply_FlashEffect : MonoBehaviour
{
    //Jadi visual handler aja
    [SerializeField] private Material normalMat;
    [SerializeField] private Material flashMat;
    [SerializeField] private float FlashDur;
    [SerializeField] private int blinkTimes;
    public Image greenBar;
    private GameObject PlayerCharacter; //Bukan soul yah
    private Animator anim; 
    private SpriteRenderer sr;
    void Start()
    {
        PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        anim = PlayerCharacter.GetComponent<Animator>();
        sr = PlayerCharacter.GetComponent<SpriteRenderer>();
    }
    //Damage Flash
    public void doFlash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashEffect());
    }
    public IEnumerator FlashEffect()
    {
        if (flashMat == null || normalMat == null)
        {
            Debug.Log ("Material not detected");
            yield break;
        }
        for (int i = 0; i<blinkTimes;i++){
            sr.material = flashMat;
            yield return new WaitForSecondsRealtime(FlashDur);
            sr.material = normalMat;
            yield return new WaitForSecondsRealtime(FlashDur);
        }
    }
    public void doHealEffect()
    {
        StopAllCoroutines();
        StartCoroutine(HealEffect());
    }
    public IEnumerator HealEffect()
    {
         for (int i = 0; i<blinkTimes;i++){
            sr.color = new Color(0f,255f,0f);
            yield return new WaitForSecondsRealtime(FlashDur);
            sr.color = Color.white;
            yield return new WaitForSecondsRealtime(FlashDur);
        }
    }
    public void UpdateBar(int curHealth, int maxHealth)
    {
        greenBar.fillAmount = curHealth/100f;
    }
    public void DeleteChar() => Destroy(PlayerCharacter);
}
