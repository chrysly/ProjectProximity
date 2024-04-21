using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
    [SerializeField] private ActorData data;
    private float _health;
    //private Vector2 _gridPosition;
    private Tile _currTile;

    private HashSet<Tile> _currMoveRange;
    private HashSet<Tile> _currAttackRange;

    private MouseManager _mouseManager;

    void Start () {
        _mouseManager = FindObjectOfType<MouseManager>();
    }

    public void onTurnStart() {
        _currMoveRange = _currTile.getTilesInRange(data.moveRange, _currTile);
        _currAttackRange = _currTile.getTilesInRange(data.attackRange, _currTile);
    }

    public void MoveActor(Vector2 position) {
        //Conditional to check if position is within valid range
    }

    // when the unit is hovered over show the available tiles it can go to
    void OnMouseOver() {
        _mouseManager.HighlightTiles(_currMoveRange, _currAttackRange);
    }

    void OnMouseExit() {
        _mouseManager.UnhighlightTiles();
    }
}
