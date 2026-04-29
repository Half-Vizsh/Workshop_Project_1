using System.Collections;
using UnityEngine;

public class Emy_Visual : MonoBehaviour
{
    [SerializeField] private  SpriteRenderer TargetCursor; //assign manually
    [SerializeField] private Material normalMat;
    [SerializeField] private Material flashMat;
    [SerializeField] private float FlashDur;
    [SerializeField] private int blinkTimes;
    private Animator anim; 
    private SpriteRenderer sr;
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        hideCursor();
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
    //Cursor
    public void showCursor() => TargetCursor.color = new Color(1f,1f,1f,1f);
    public void hideCursor() => TargetCursor.color = new Color(1f,1f,1f,0f);
}
