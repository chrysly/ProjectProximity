using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public new delegate void StateTransition(GameState state);
    public static event StateTransition OnStateTransition;

    public enum GameState {
        Menu,
        Options,
        Game,
        Lose,
        Win
    }

    public GameState State;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        UpdateGameState(GameState.Menu);
    }

    public void UpdateGameState(GameState newState) {
        State = newState;
        Debug.Log("State: " + newState);

        switch (newState) {
            case GameState.Menu:
                break;
            case GameState.Options:
                break;
            case GameState.Game:
                break;
            case GameState.Lose:
                break;
            case GameState.Win:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnStateTransition?.Invoke(newState);
    }

    public void StartGame() {
        UpdateGameState(GameState.Game);
    }

    public void ReturnToMenu() {
        UpdateGameState(GameState.Menu);
    }
}