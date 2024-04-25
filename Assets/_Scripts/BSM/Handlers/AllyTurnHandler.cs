using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handler for Ally (player) turn
/// </summary>
public class AllyTurnHandler : MonoBehaviour {
    private ActorHandler _actorHandler;
    private MouseManager mouseManager;

    void Start() {
        _actorHandler = FindObjectOfType<ActorHandler>();
        mouseManager = FindObjectOfType<MouseManager>();
        mouseManager.OnMovedUnit += AllyUnitActed;
        BattleStateMachine.Instance.CurrInput.aliveAllies = _actorHandler.allyActors;
    }

    /// <summary>
    /// on turn start reset troops
    /// </summary>
    public void AllyTurnStart() {
        mouseManager.ChangeState(MouseManager.mouseStates.Idle);
        foreach (AllyActor a in BattleStateMachine.Instance.CurrInput.aliveAllies) {
            a.hasMoved = false;
        }
    }

    /// <summary>
    /// runs in the ally turn state 
    /// </summary>
    public void AllyTurnUpdate() {  }

    /// <summary>
    /// listens for an event from mouse manager and tracks how many units have moved
    /// </summary>
    private void AllyUnitActed(Tile currTile, Tile targetTile) {
        int currMovedUnits = 0;
        currTile.occupiedActor.hasMoved = true;
        foreach (AllyActor a in BattleStateMachine.Instance.CurrInput.aliveAllies) {
            if (a.hasMoved) { currMovedUnits++; }
        }

        Debug.Log("Allies Acted: " + currMovedUnits);
        if (currMovedUnits >= BattleStateMachine.Instance.CurrInput.aliveAllies.Count) {
            Debug.Log("END OF PLAYER TURN");
            EndAllyTurn();
        }
    }

    /// <summary>
    /// send message to bsm to transition states
    /// </summary>
    private void EndAllyTurn() {
        BattleStateMachine.Instance.Transition<BattleStateMachine.EnemyTurn>();
    }
}
