using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    [SerializeField] private Transform menuCanvas;
    [SerializeField] private Transform winCanvas;
    [SerializeField] private Transform loseCanvas;
    private bool _isActive;
    
    private void Awake() {
        GameManager.OnStateTransition += SetMenuState;
        winCanvas.gameObject.SetActive(false);
        loseCanvas.gameObject.SetActive(false);
    }

    private void OnDisable() {
        GameManager.OnStateTransition -= SetMenuState;
    }

    public void SetMenuState(GameManager.GameState state) {
        if (state is GameManager.GameState.Menu) {
            RestartOperation();
        } else if (state is GameManager.GameState.Game) {
            StartOperation();
        } else if (state is GameManager.GameState.Win) {
            WinOperation();
        } else if (state is GameManager.GameState.Lose) {
            LoseOperation();
        }
    }

    private void StartOperation() {
        if (_isActive) {
            _isActive = false;
            menuCanvas.gameObject.SetActive(false);
        }
        winCanvas.gameObject.SetActive(false);
        loseCanvas.gameObject.SetActive(false);
    }

    private void RestartOperation() {
        if (!_isActive) {
            _isActive = true;
            menuCanvas.gameObject.SetActive(true);
        }
        winCanvas.gameObject.SetActive(false);
        loseCanvas.gameObject.SetActive(false);
    }

    private void WinOperation() {
        winCanvas.gameObject.SetActive(true);
    }

    private void LoseOperation() {
        loseCanvas.gameObject.SetActive(true);
    }
}
