using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
    [SerializeField] private ActorData data;
    private float _health;
    private Tile _currTile;
    public bool hasMoved;

    private HashSet<Tile> _currMoveRange = new();
    private HashSet<Tile> _currAttackRange = new();

    private MouseManager _mouseManager;

    void Start () {
        _mouseManager = FindObjectOfType<MouseManager>();
    }

    // change logic later
    public void OnTurnStart(Tile tile) {
        UpdateCurrTile(tile);
        _currMoveRange = _currTile.getTilesInRange(data.moveRange, GridManager.Instance.GetGrid());
        _currAttackRange = _currTile.getTilesInRange(data.attackRange, GridManager.Instance.GetGrid());
    }

    public void UpdateCurrTile(Tile tile) {
        _currTile = tile;
    }

    public void MoveActor(Vector2 position) {
        //Conditional to check if position is within valid range
    }

    // when the unit is hovered over show the available tiles it can go to
    void OnMouseOver() {
        this.GetComponent<SpriteRenderer>().color = Color.gray;
        Debug.Log(_currMoveRange.Count);
        _mouseManager.HighlightTiles(_currMoveRange, _currAttackRange);
    }

    void OnMouseExit() {
        this.GetComponent<SpriteRenderer>().color = Color.white;
        _mouseManager.UnhighlightTiles();
    }
}
