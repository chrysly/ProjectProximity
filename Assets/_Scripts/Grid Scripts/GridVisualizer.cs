using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class GridVisualizer : MonoBehaviour {
    
    private static GridVisualizer _instance;
    public static GridVisualizer Instance { get { return _instance; } }
    
    [SerializeField] private MouseManager cursorManager;

    [SerializeField] private Material selectMaterial;
    [SerializeField] private Material scrollMaterial;

    private Tile _activeTile;
    private Tile _targetTile;
    private List<Tile> _path;

    private void Awake() {
        cursorManager.OnUnitHovered += SelectTile;
    }

    private void SelectTile(Tile source, Tile target) {
        if (source == _activeTile && target == _targetTile) return;
        ClearTiles();
        _activeTile = source;
        _targetTile = target;
        Pathfinding pathfinder = new Pathfinding();
        _path = pathfinder.CalculatePath(source, target, GridManager.Instance.GetGrid());
        foreach (Tile tile in _path) {
            tile.GetComponent<MeshRenderer>().materials[1].SetColor("_Color", Color.yellow);
            tile.GetComponent<MeshRenderer>().materials[1].SetFloat("_Alpha", 1f);
        }
        
        _targetTile.GetComponent<MeshRenderer>().materials[1].SetColor("_Color", Color.green);
        _targetTile.GetComponent<MeshRenderer>().materials[1].SetFloat("_Alpha", 1f);
    }

    private void ClearTiles() {
        _targetTile.GetComponent<MeshRenderer>().materials[1].SetFloat("_Alpha", 0f);
        foreach (Tile tile in _path) {
            tile.GetComponent<MeshRenderer>().materials[1].SetFloat("_Alpha", 0f);
        }
    }

    private void DeselectTile(Tile source, Tile target) {
        _targetTile = source;
    }
}
