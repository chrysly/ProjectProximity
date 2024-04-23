using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player mouse clicks. is a mini internal state machine
/// </summary>
public class MouseManager : MonoBehaviour {
    
    #region Events
    public delegate void UnitSelected(Tile currTile);
    public event UnitSelected OnUnitSelected;

    public delegate void UnitDeselected(Tile currTile);
    public event UnitDeselected OnUnitDeselected;

    public delegate void MovedUnit(Tile currTile, Tile targetTile);
    public event MovedUnit OnMovedUnit;

    public delegate void UnitHovered(Tile currTile, Tile targetTile);

    public event UnitHovered OnUnitHovered;
    #endregion

    public enum mouseStates {
        Disabled,
        Idle,
        UnitSelected,
        MoveUnit,
    }

    private mouseStates _currState = mouseStates.Disabled;

    private Tile _currTile;
    private Tile _targetTile;

    private void Start() {
        _currState = mouseStates.Disabled;
    }

    /// <summary>
    /// for other classes to call
    /// </summary>
    public void ChangeState(mouseStates nextState) {
        _currState = nextState;
    }

    /// <summary>
    /// state transitions
    /// </summary>
    void Update() {
        switch (_currState) {
            case (mouseStates.Disabled):
                DisabledState();
                break;
            case (mouseStates.Idle):
                IdleState();
                break;
            case (mouseStates.UnitSelected):
                UnitSelectedState();
                OnEnter();
                break;
            case (mouseStates.MoveUnit):
                MoveUnitState();
                break;
            default:
                Debug.Log("mousemanager: null state");
                break;
        }
    }

    /// <summary>
    /// when the mouse can't interact with the board
    /// </summary>
    private void DisabledState() {

    }

    /// <summary>
    /// during the players turn, but nothing is currently selected
    /// </summary>
    private void IdleState() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out hit)) {
                if (hit.collider != null) {
                    Tile tile = hit.collider.GetComponent<Tile>();
                    if (tile.occupiedActor != null && tile.occupiedActor.GetType() == typeof(AllyActor)) {
                        if (!tile.occupiedActor.hasMoved) {
                            Debug.Log("Unit: " + " selected");
                            _currTile = tile;
                            _currState = mouseStates.UnitSelected;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// if a unit has been selected
    /// </summary>
    private void UnitSelectedState() {
        OnUnitSelected?.Invoke(_currTile);

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out hit)) {
                if (hit.collider != null) {
                    Tile tile = hit.collider.GetComponent<Tile>();
                    if (tile != null && _currTile.occupiedActor._currMoveRange.Contains(tile)) {
                        _targetTile = tile;
                        _currState = mouseStates.MoveUnit;
                    } else if (tile != null && _currTile.occupiedActor._currAttackRange.Contains(tile) &&
                               tile.occupiedActor != null) {
                        _targetTile = tile;
                        _currState = mouseStates.MoveUnit;
                    }
                }
            }
            //This would probably have to happen during MoveUnit state, by the time the player clicks left click they've already moved on to the next state before
            //they can cancel - Chris
        } else if (Input.GetMouseButtonDown(1)) {   // if right click, then deselct unit
            _currTile = null;
            _currState = mouseStates.Idle;
            OnUnitDeselected?.Invoke(_currTile);
        }
    }

    private void OnEnter() {
        if (_currState == mouseStates.UnitSelected) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out hit)) {
                if (hit.collider != null) {
                    Tile tile = hit.collider.GetComponent<Tile>();
                    if (tile != null && tile.Data().isWalkable) {
                        if (_currTile.occupiedActor._currMoveRange.Contains(tile)) {
                            OnUnitHovered?.Invoke(_currTile, tile);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// if a unit was selected and then a valid tile was selected
    /// </summary>
    private void MoveUnitState() {
        OnMovedUnit?.Invoke(_currTile, _targetTile);
        Actor actor = _currTile.occupiedActor;
        actor.hasMoved = true;
        _currTile.occupiedActor = null;
        _targetTile.occupiedActor = actor;
        
        _currTile = null;
        _targetTile = null;
        _currState = mouseStates.Idle;

        // wait / do smthing (?)
        //StartCoroutine(ReturnToIdle());
    }

    //dumb coroutine stuff
    // private IEnumerator ReturnToIdle() {
    //     OnUnitDeselected?.Invoke(_currTile);
    //     yield return new WaitForSeconds(BattleStateMachine.Instance.CurrInput.AnimationDelay);
    //     
    // }
}
