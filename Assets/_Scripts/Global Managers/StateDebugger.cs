using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDebugger : MonoBehaviour {
    [SerializeField] private KeyCode restartKeyCode = KeyCode.R;
    [SerializeField] private KeyCode winKeyCode = KeyCode.W;
    [SerializeField] private KeyCode loseKeyCode = KeyCode.L;

    private void Update() {
        if (Input.GetKeyDown(restartKeyCode)) {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Menu);
        } else if (Input.GetKeyDown(winKeyCode)) {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Win);
        } else if (Input.GetKeyDown(loseKeyCode)) {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
    }
}
