using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Manager for handling interactions between actors
/// </summary>
public class ActorHandler : MonoBehaviour
{
    private MouseManager mouseManager;
    private ActorAnimationHandler _animator;

    public List<AllyActor> allyActors;
    public List<EnemyActor> enemyActors;
    
    #region Events
    public delegate void UnitDefeated(Actor actor);
    public event UnitDefeated OnUnitDefeated;

    public delegate void ToAnimateState();
    public event ToAnimateState OnToAnimateState;
    #endregion

    private void Awake() {
        mouseManager = FindObjectOfType<MouseManager>();
        _animator = FindObjectOfType<ActorAnimationHandler>();
        mouseManager.OnMovedUnit += HandleInteraction;  // might be wrong
    }

    /// <summary>
    /// handles the interaction logic from currTile to target tile (checks if it should move or attack)
    /// </summary>
    private void HandleInteraction(Tile currTile, Tile targetTile) {
        OnToAnimateState?.Invoke();

        //make seur you differentiate between attack range vs move range bc edge case
        // set the curr actor to hasMoved
        if (targetTile.occupiedActor != null && targetTile.occupiedActor is EnemyActor) { // this is kinda a redundant check i think?
            UnitAttacks(currTile.occupiedActor, targetTile.occupiedActor);
        }
        else {
            UnitMoves(currTile, targetTile);
        }
    }

    /// <summary>
    /// logic for a unit moving to another tile
    /// </summary>
    private void UnitMoves(Tile currTile, Tile targetTile) {
        //naur we do this in an animation handler lol
        currTile.occupiedActor.hasMoved = true;
        Debug.Log(currTile.occupiedActor.hasMoved);
        targetTile.occupiedActor = currTile.occupiedActor;
        currTile.occupiedActor = null;
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
