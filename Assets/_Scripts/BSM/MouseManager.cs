using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player mouse clicks. is a mini internal state machine
/// </summary>
public class MouseManager : MonoBehaviour
{
    #region Events
    public delegate void UnitSelected(Tile currTile);
    public static event UnitSelected OnUnitSelected;

    public delegate void UnitDeselected(Tile currTile);
    public static event UnitDeselected OnUnitDeselected;

    public delegate void MovedUnit(Tile currTile, Tile targetTile);
    public static event MovedUnit OnMovedUnit;
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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null) {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile.occupiedActor != null && tile.occupiedActor.GetType() == typeof(AllyActor)) {
                    Debug.Log("Unit: " + " selected");
                    _currTile = tile;
                    _currState = mouseStates.UnitSelected;
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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null) {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null) { // and it's either a valid attack or move tile
                    _targetTile = tile;
                    _currState = mouseStates.MoveUnit;
                }
            }
        } else if (Input.GetMouseButtonDown(1)) {   // if right click, then deselct unit
            _currTile = null;
            _currState = mouseStates.Idle;
            OnUnitDeselected?.Invoke(_currTile);
        }
    }

    /// <summary>
    /// if a unit was selected and then a valid tile was selected
    /// </summary>
    private void MoveUnitState() {
        OnMovedUnit?.Invoke(_currTile, _targetTile);

        // wait / do smthing (?)
        OnUnitDeselected?.Invoke(_currTile);
        _currTile = null;
        _targetTile = null;
        _currState = mouseStates.Idle;
    }
}
