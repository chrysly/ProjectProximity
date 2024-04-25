using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GridVisualizer : MonoBehaviour {
    
    private MouseManager _cursorManager;

    [SerializeField] private int materialIndex = 3;

    private Tile _activeTile;
    private Tile _targetTile;
    private List<Tile> _path;

    private void Awake() {
        _cursorManager = FindObjectOfType<MouseManager>();
        _cursorManager.OnUnitHovered += SelectTile;
        _cursorManager.OnMovedUnit += ClearTiles;
        _cursorManager.OnUnitSelected += DrawRange;
    }

    private void SelectTile(Tile source, Tile target) {
        if (source == _activeTile && target == _targetTile) return;
        Debug.Log("drawing");
        ClearTiles(source, target);
        _activeTile = source;
        _targetTile = target;
        DrawRange(source);
        Pathfinding pathfinder = new Pathfinding();
        _path = pathfinder.CalculatePath(source, target, GridManager.Instance.GetGrid());
        foreach (Tile tile in _path) {
            if (_targetTile == tile) continue;
            tile.GetComponent<MeshRenderer>().materials[materialIndex].SetColor("_Color", new Color(0.2f, 0.5f, 0.2f));
            tile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 1f);
        }

        _activeTile.GetComponent<MeshRenderer>().materials[materialIndex].SetColor("_Color", new Color(0.8f, 0.6f, 0.2f));
        _activeTile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 1f);
        
        _targetTile.GetComponent<MeshRenderer>().materials[materialIndex].SetColor("_Color", new Color(0.4f, 0.8f, 0.4f));
        _targetTile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 1f);
    }

    private void DrawRange(Tile source) {
        HashSet<Tile> attackRangeSet = source.occupiedActor._currAttackRange;
        HashSet<Tile> moveRangeSet = source.occupiedActor._currMoveRange;
        var moveDisplay = moveRangeSet.Except(attackRangeSet);

        foreach (Tile tile in attackRangeSet) {
            tile.GetComponent<MeshRenderer>().materials[materialIndex].SetColor("_Color", new Color(0.6f, 0.4f, 0.2f));
            tile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 1f);
        }

        foreach (Tile tile in moveDisplay) {
            tile.GetComponent<MeshRenderer>().materials[materialIndex].SetColor("_Color", Color.yellow);
            tile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 1f);
        }

    }

    private void ClearTiles(Tile start, Tile target) {
        // if (_activeTile != null)
        //     _activeTile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 0f);
        //
        // if (_targetTile != null)
        //     _targetTile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 0f);
        //
        // if (_path != null && _path.Count > 0) {
        //     foreach (Tile tile in _path) {
        //         tile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 0f);
        //     }
        // }

        foreach (Tile tile in GridManager.Instance.GetGrid()) {
            tile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 0f);
        }
    }

    private void DeselectTile(Tile source, Tile target) {
        _targetTile = source;
        _path = new List<Tile>();
    }
}
