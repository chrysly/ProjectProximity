using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    /// <summary>
    /// Scuffed two-purpose event based & overridable class until we decide on one LMAOOOO
    /// </summary>
    public new delegate void Click();
    public event Click OnClick;

    public new delegate void Enter();
    public event Enter OnEnter;

    public new delegate void Exit();
    public event Exit OnExit;

    protected virtual void OnMouseDown() {
        Debug.Log("I was clicked");
        OnClick?.Invoke();
    }

    protected virtual void OnMouseEnter() {
        Debug.Log("I was entered");
        OnEnter?.Invoke();
    }

    protected void OnMouseExit() {
        Debug.Log("I was exited");
        OnExit?.Invoke();
    }
}
