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
    [SerializeField] private Canvas canvas;
    private Tile _activeTile;
    private Tile _targetTile;
    private List<Tile> _path;

    private void Awake() {
        _cursorManager = FindObjectOfType<MouseManager>();
        _cursorManager.OnUnitHovered += SelectTile;
        _cursorManager.OnMovedUnit += ClearTiles;
        _cursorManager.OnUnitSelected += DrawRange;
        _cursorManager.OnUnitDeselected += DeselectTiles;
    }

    private void SelectTile(Tile source, Tile target) {
        if (source == _activeTile && target == _targetTile) return;
        ClearTiles(source, target);
        _activeTile = source;
        _targetTile = target;
        RedrawRange(source);

        //Vu's Ui Shenanigans
        string name = _activeTile.occupiedActor.getName();
        switch (name) {
            case "Commander":
                canvas.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "Extender":
                canvas.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case "Fighter":
                canvas.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case "Healer":
                canvas.transform.GetChild(3).gameObject.SetActive(true);
                break;
            case "Mage":
                canvas.transform.GetChild(4).gameObject.SetActive(true);
                break;
        }
        
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
        if (source == _activeTile) return;
        HashSet<Tile> attackRangeSet = source.occupiedActor._currAttackRange;
        HashSet<Tile> moveRangeSet = source.occupiedActor._currMoveRange;
        var moveDisplay = moveRangeSet.Except(attackRangeSet);

        foreach (Tile tile in attackRangeSet) {
            if (tile.Data().isWalkable) {
                tile.GetComponent<MeshRenderer>().materials[materialIndex]
                    .SetColor("_Color", new Color(0.6f, 0.4f, 0.2f));
                tile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 1f);
            }
        }

        foreach (Tile tile in moveDisplay) {
            if (tile.Data().isWalkable) {
                tile.GetComponent<MeshRenderer>().materials[materialIndex].SetColor("_Color", Color.yellow);
                tile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 1f);
            }
        }
        
        source.GetComponent<MeshRenderer>().materials[materialIndex].SetColor("_Color", Color.green);
        source.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 1f);
    }
    
    private void RedrawRange(Tile source) {
        HashSet<Tile> attackRangeSet = source.occupiedActor._currAttackRange;
        HashSet<Tile> moveRangeSet = source.occupiedActor._currMoveRange;
        var moveDisplay = moveRangeSet.Except(attackRangeSet);

        foreach (Tile tile in attackRangeSet) {
            if (tile.Data().isWalkable) {
                tile.GetComponent<MeshRenderer>().materials[materialIndex]
                    .SetColor("_Color", new Color(0.6f, 0.4f, 0.2f));
                tile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 1f);
            }
        }

        foreach (Tile tile in moveDisplay) {
            if (tile.Data().isWalkable) {
                tile.GetComponent<MeshRenderer>().materials[materialIndex].SetColor("_Color", Color.yellow);
                tile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 1f);
            }
        }
        
        source.GetComponent<MeshRenderer>().materials[materialIndex].SetColor("_Color", Color.green);
        source.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 1f);
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

        //Vu's Stuff
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        canvas.transform.GetChild(3).gameObject.SetActive(false);
        canvas.transform.GetChild(4).gameObject.SetActive(false);

        foreach (Tile tile in GridManager.Instance.GetGrid()) {
            if (tile.Data().isWalkable) tile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 0f);
        }
    }
    
    private void DeselectTiles(Tile source) {
        // Vu's Stuff
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        canvas.transform.GetChild(3).gameObject.SetActive(false);
        canvas.transform.GetChild(4).gameObject.SetActive(false);

        foreach (Tile tile in GridManager.Instance.GetGrid()) {
            if (tile.Data().isWalkable) tile.GetComponent<MeshRenderer>().materials[materialIndex].SetFloat("_Alpha", 0f);
        }
    }

    private void DeselectTile(Tile source, Tile target) {
        _targetTile = source;
        _path = new List<Tile>();
    }
}
