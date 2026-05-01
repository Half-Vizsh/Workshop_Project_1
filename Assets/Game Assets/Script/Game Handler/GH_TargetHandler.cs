using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class GH_TargetHandler : MonoBehaviour
{
    public static List<Emy_Base> SelectablesEmy = new List<Emy_Base>();
    public static int currentIdx = 0;
    public event Action<Emy_Base> AttackConfirmed;
    public event Action AttackCanceled;
    private bool isTargeting = false;
    public void addTarget(Emy_Base T) => SelectablesEmy.Add(T);
    public void remTarget(Emy_Base T) => SelectablesEmy.Remove(T);
    public void doTargetting()
    {
        if (isTargeting) return;
        StopAllCoroutines();
        currentIdx = 0; 
        StartCoroutine(ReadingPlayerTargetting());
    }
    //Written by Claude but catch the meaning, it's navigating by index, the modulo lines making sure that it won't go xout of bound 
    public IEnumerator ReadingPlayerTargetting()
    {
        if (getCurrent() == null) yield break;   
        getCurrent().onChoosen(); //Highlight the first target, ini jalan
        yield return null; //Nungguin si BH selesai dulu ceunah biar bisa make enter
        while (true){
            // Debug.Log("The Reading Player Targetting is Running");
            if (Keyboard.current.aKey.wasPressedThisFrame) moveBack();
            else if (Keyboard.current.dKey.wasPressedThisFrame) moveNext();
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                // Debug.Log("Attack Confirmed");
                 isTargeting = false;
                getCurrent().onLeave();
                AttackConfirmed?.Invoke(getCurrent());
                yield break;
            }
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                isTargeting = false;
                getCurrent().onLeave();
                AttackCanceled?.Invoke();
                yield break;
            }
            yield return null;
        } 
    }
    public void moveNext()
    {
        Debug.Log("Move next triggered");
        getCurrent().onLeave();
        currentIdx = (currentIdx+1) % SelectablesEmy.Count; 
        getCurrent().onChoosen();
    }
    public void moveBack()
    {
        Debug.Log("Move back triggered");
        getCurrent().onLeave();
        currentIdx = (currentIdx-1+SelectablesEmy.Count)% SelectablesEmy.Count;
        getCurrent().onChoosen();
    }
    public Emy_Base getCurrent() {
        if (SelectablesEmy.Count<=0) return null;
        return SelectablesEmy[currentIdx];
    }   
    public List<Emy_Base> GetAllEnemies()
    {
        return SelectablesEmy;
    }
}
