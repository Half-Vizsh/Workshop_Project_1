using UnityEngine;
using System.Collections;

public class Ply_FlashEffect : MonoBehaviour
{
    [SerializeField] private Material normalMat;
    [SerializeField] private Material flashMat;
    [SerializeField] private float FlashDur;
    [SerializeField] private int blinkTimes;
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
}
