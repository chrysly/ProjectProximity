using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class GridVisualizer : MonoBehaviour {

    [SerializeField] private MouseManager cursorManager;

    [SerializeField] private Material selectMaterial;
    [SerializeField] private Material scrollMaterial;

    private Tile _activeTile;
    private List<Tile> _path;

    private void SelectTile(Tile source, Tile target) {
        _activeTile = source;
        Pathfinding pathfinder = new Pathfinding();
        _path = pathfinder.CalculatePath(source, target, GridManager.Instance.GetGrid());
        foreach (Tile tile in _path) {
            tile.GetComponent<MeshRenderer>().materials[1].SetColor("_Color", Color.yellow);
            tile.GetComponent<MeshRenderer>().materials[1].SetFloat("_Alpha", 1f);
        }
        
        _activeTile.GetComponent<MeshRenderer>().materials[1].SetColor("_Color", Color.green);
        _activeTile.GetComponent<MeshRenderer>().materials[1].SetFloat("_Alpha", 1f);
    }

    private void ClearTiles() {
        _activeTile.GetComponent<MeshRenderer>().materials[1].SetFloat("_Alpha", 0f);
        foreach (Tile tile in _path) {
            tile.GetComponent<MeshRenderer>().materials[1].SetFloat("_Alpha", 0f);
        }
    }

    private void DeselectTile(Tile source, Tile target) {
        _activeTile = source;
    }
}
