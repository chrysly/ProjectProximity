using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handler for Ally (player) turn
/// </summary>
public class AllyTurnHandler : MonoBehaviour
{
    private MouseManager mouseManager;

    void Start() {
        MouseManager.OnMovedUnit += AllyUnitActed;
        mouseManager = FindObjectOfType<MouseManager>();
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
        foreach (AllyActor a in BattleStateMachine.Instance.CurrInput.aliveAllies) {
            if (a.hasMoved) { currMovedUnits++; }
        }

        if (currMovedUnits >= BattleStateMachine.Instance.CurrInput.aliveAllies.Count) {
            EndAllyTurn();
        }
    }

    /// <summary>
    /// send message to bsm to transition states
    /// </summary>
    private void EndAllyTurn() {
        // send smthing to bsm which will swithc it to the enemy turn
        // make an event for this
    }
}
