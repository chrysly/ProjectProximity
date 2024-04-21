using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    #region Singleton
    private static GridManager _instance;

    public static GridManager Instance { get { return _instance; } }

    private void Awake() {
        if (_instance == null) {  Destroy(this.gameObject); }
        else { _instance = this; }
    }
    #endregion Singleton
    
    // 2D array of tiles

    void Start() {

    }

    // generates a grid of tiles populated by class Tile
    private void GenerateGrid() {

    }
}
