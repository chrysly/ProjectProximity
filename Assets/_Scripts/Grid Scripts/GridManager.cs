using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    #region Singleton
    private static GridManager _instance;

    public static GridManager Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null) {  Destroy(this.gameObject); }
        else { _instance = this; }

        grid = Transpose(Inputgrid);
    }
    #endregion Singleton

    // tile prefabs
    public List<Tile> tileTypes;

    // 0 = Grass
    // 1 = Water
    private int[,] Inputgrid = new int[,] {
        {0, 0, 1},
        {0, 1, 1},
        {0, 1, 1},
    };

    private int[,] grid;

    //void Start() {
    //    Debug.Log("start grid manager");
    //    grid = Transpose(Inputgrid);
    //}

    // helper to transpose input grid
    private int[,] Transpose(int[,] array) {
        int rows = array.GetLength(0);
        int columns = array.GetLength(1);

        int[,] transposedArray = new int[columns, rows];

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                transposedArray[j, i] = array[i, j];
            }
        }

        return transposedArray;
    }

    // generates a grid of tiles populated by class Tile
    public void GenerateGrid() {
        for (int x = 0; x < grid.GetLength(0); x++) {
            for (int y = 0; y < grid.GetLength(1); y++) {
                switch (grid[x, y]) {
                    case 0:
                        var spawnedTile0 = Instantiate(tileTypes[0], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile0.name = tileTypes[0].name + " " + x + " " + y;
                        break;
                    case 1:
                        var spawnedTile1 = Instantiate(tileTypes[1], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile1.name = tileTypes[1].name + " " + x + " " + y;
                        break;
                    default:
                        Debug.Log("yeah something fucked up");
                        break;
                }
            }
        }
    }
}
