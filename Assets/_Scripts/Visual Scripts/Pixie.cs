using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pixie : MonoBehaviour {
    [SerializeField] private Pixie connectionPoint;
    [SerializeField] private Color connectionColor = Color.white;

    private Transform _actorDisplay;
    private Transform _vfxDisplay;
    private LineRenderer _lineDisplay;

    private bool _connected = false;

    private void Awake() {
        _actorDisplay = transform.GetChild(0);
        _vfxDisplay = transform.GetChild(1);
        _lineDisplay = transform.GetComponentInChildren<LineRenderer>();
        _vfxDisplay.gameObject.SetActive(false);
        _lineDisplay.materials[0].SetFloat("_Scroll" , 0f);
        _lineDisplay.positionCount = 2;
    }

    public Color GetColor() {
        return connectionColor;
    }

    public void ConnectPixie(Pixie target) {
        _lineDisplay.gameObject.SetActive(true);
    }

    public void DisconnectPixie() {
        
    }
}
