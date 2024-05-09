
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
    [SerializeField] private ActorData data;
    private float _health;
    private Tile _currTile;
    public bool hasMoved;

    public HashSet<Tile> _currMoveRange = new();
    public HashSet<Tile> _currAttackRange = new();

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

    /// <summary>
    /// Depletes health from actor. If target actor health is <= 0 returns FALSE.
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public bool DepleteHealth(float damage) {
        _health -= damage;
        if (_health <= 0) {
            return true;
        }
        return false;
    }

    public float GetHealth() {
        return _health;
    }

    public int[] GetSpawnCoordinates() {
        return new [] {data.x, data.y, data.z};
    }

    //Vu's method for ui stuff
    public string getName()
    {
        return data.nameTag;
    }

}
