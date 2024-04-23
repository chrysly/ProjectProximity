using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager for handling interactions between actors
/// </summary>
public class ActorHandler : MonoBehaviour
{
    #region Events
    public delegate void UnitDefeated(Actor actor);
    public static event UnitDefeated OnUnitDefeated;
    #endregion

    private void Start() {
        MouseManager.OnMovedUnit += HandleInteraction;  // might be wrong
    }

    /// <summary>
    /// handles the interaction logic from currTile to target tile (checks if it should move or attack)
    /// </summary>
    private void HandleInteraction(Tile currTile, Tile targetTile) {
        // set the curr actor to hasMoved
        if (targetTile.occupiedActor != null) { // this is kinda a redundant check i think?
            UnitAttacks(currTile.occupiedActor, targetTile.occupiedActor);
        }
    }

    /// <summary>
    /// logic for a unit moving to another tile
    /// </summary>
    private void UnitMoves() {

    }

    /// <summary>
    /// logic for unit attacking another unit
    /// </summary>
    private void UnitAttacks(Actor curr, Actor target) {
        // logic for units attacking

        // if the target unit after the attack has zero health left, then send the unit death event to the bsm
        OnUnitDefeated?.Invoke(curr);
    }

    // if an actor dies then send an event to the BSM
}
