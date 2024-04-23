using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField] private ActorHandler actorHandler;
    
    #region Singleton
    private static GridManager _instance;

    public static GridManager Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null) {  Destroy(this.gameObject); }
        else { _instance = this; }

        intGrid = Transpose(inputGrid);
    }
    #endregion Singleton

    [SerializeField] private GameLogic gameLogic;

    // tile prefabs
    public List<Tile> tileTypes;

    // 0 = Grass
    // 1 = Water
    private int[,] inputGrid = new int[,] {
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 1},
        {0, 0, 0, 1, 1},
        {0, 0, 0, 1, 1},
    };

    private int[,] intGrid;
    private Tile[,] tileGrid;
    public Tile[,] GetGrid() { return tileGrid; }

    void Start() {
        intGrid = Transpose(inputGrid);
        tileGrid = new Tile[inputGrid.GetLength(0), inputGrid.GetLength(1)];
    }

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
        for (int x = 0; x < intGrid.GetLength(0); x++) {
            for (int y = 0; y < intGrid.GetLength(1); y++) {
                switch (intGrid[x, y]) {
                    case 0:
                        var spawnedTile0 = Instantiate(tileTypes[0], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile0.name = tileTypes[0].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile0;
                        break;
                    case 1:
                        var spawnedTile1 = Instantiate(tileTypes[1], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile1.name = tileTypes[1].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile1;
                        break;
                    default:
                        Debug.Log("yeah something fucked up");
                        break;
                }
            }
        }
        PlaceUnits();   // spawn units on field
    }

    /// <summary>
    /// places units on the board (hard coded bc fml)
    /// </summary>
    private void PlaceUnits() {
        List<AllyActor> allies = actorHandler.allyActors;
        List<EnemyActor> enemies = actorHandler.enemyActors;

        // hard coded bc fml
        var newUnit = Instantiate(allies[0], new Vector3(0,0), Quaternion.identity);
        newUnit.OnTurnStart(tileGrid[0, 0]);
        tileGrid[0, 0].occupiedActor = newUnit;
    }
}
