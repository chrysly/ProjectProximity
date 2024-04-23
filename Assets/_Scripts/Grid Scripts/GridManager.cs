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
    /***************************
     * 0 = empty tile
     * 1 = plain grass
     * 2 = grass with two tufts
     * 3 = grass with one tuft one rock
     * 4 = grass with two rocks
     * 5 = grass with one tuft
     * 6 = dirt with tree stump
     * 7 = dirt with moss
     * 8 = dirt with green rock
     * 9 = water
     * 10 = dirt with light rock
     * 11 = bridge
     **************************/
    private int[,] inputGrid = new int[,] {
        //   {1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14,15,16,17,18,19,20}

        //1  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 9, 9, 0, 0, 0},
        //2  {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 3, 4, 0, 9, 9, 9, 0, 0},
        //3  {0, 0, 0, 0, 0, 0, 0, 9, 9, 1, 2, 1, 1, 4, 1, 1, 9, 9, 0, 0},
        //4  {0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 2, 3, 1, 1, 3, 7, 9, 9, 9, 0},
        //5  {0, 9, 9, 9, 9, 9, 3, 4, 9, 9, 9, 1, 1, 3, 1, 1, 1, 9, 9, 0},
        //6  {9, 9, 9, 9, 9, 1, 1, 3, 4, 3,11, 3, 1, 1, 6, 3,10, 9, 9, 0},
        //7  {9, 9, 9, 9, 9, 3, 1, 6, 4, 1, 9, 9, 1, 4, 1, 1, 7, 9, 9, 0},
        //8  {1, 1, 1, 1, 2, 1, 8, 7, 2, 1, 2, 9, 9, 2, 3, 1, 1, 1, 9, 9},
        //9  {0, 1, 1, 1, 4, 3, 1, 3, 1, 1, 3, 3,11, 1, 3, 1, 3, 3, 9, 9},
        //10 {0, 1, 1, 1, 1, 3, 1, 4, 1, 1, 4, 5,11, 4, 1, 8, 1, 4, 9, 9},
        //11 {0, 0, 0, 8, 1, 1, 1,10, 2, 3, 1,10, 9, 9, 3, 5, 1, 0, 9, 9},
        //12 {0, 0, 0, 0, 1, 2, 1, 4, 1, 2, 8, 2, 1,11, 4, 2, 1, 0, 9, 9},
        //13 {0, 0, 0, 0, 6, 3, 1, 5, 4, 1, 1, 4, 1, 9, 9, 9, 9, 9, 9, 0},
        //14 {0, 0, 0, 0, 0, 0, 0, 0, 4, 1, 0, 0, 0, 9, 9, 9, 9, 0, 0, 0},
        //15 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 0, 0, 0, 0},
        //16 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 0, 0, 0, 0},
        //17 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 0, 0, 0},

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
                        spawnedTile0.x = x;
                        spawnedTile0.y = y;
                        break;
                    case 1:
                        var spawnedTile1 = Instantiate(tileTypes[1], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile1.name = tileTypes[1].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile1;
                        spawnedTile1.x = x;
                        spawnedTile1.y = y;
                        break;
                    case 2:
                        var spawnedTile2 = Instantiate(tileTypes[2], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile2.name = tileTypes[2].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile2;
                        spawnedTile2.x = x;
                        spawnedTile2.y = y;
                        break;
                    case 3:
                        var spawnedTile3 = Instantiate(tileTypes[3], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile3.name = tileTypes[3].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile3;
                        spawnedTile3.x = x;
                        spawnedTile3.y = y;
                        break;
                    case 4:
                        var spawnedTile4 = Instantiate(tileTypes[4], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile4.name = tileTypes[4].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile4;
                        spawnedTile4.x = x;
                        spawnedTile4.y = y;
                        break;
                    case 5:
                        var spawnedTile5 = Instantiate(tileTypes[5], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile5.name = tileTypes[5].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile5;
                        spawnedTile5.x = x;
                        spawnedTile5.y = y;
                        break;
                    case 6:
                        var spawnedTile6 = Instantiate(tileTypes[6], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile6.name = tileTypes[6].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile6;
                        spawnedTile6.x = x;
                        spawnedTile6.y = y;
                        break;
                    case 7:
                        var spawnedTile7 = Instantiate(tileTypes[7], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile7.name = tileTypes[7].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile7;
                        spawnedTile7.x = x;
                        spawnedTile7.y = y;
                        break;
                    case 8:
                        var spawnedTile8 = Instantiate(tileTypes[8], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile8.name = tileTypes[8].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile8;
                        spawnedTile8.x = x;
                        spawnedTile8.y = y;
                        break;
                    case 9:
                        var spawnedTile9 = Instantiate(tileTypes[9], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile9.name = tileTypes[9].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile9;
                        spawnedTile9.x = x;
                        spawnedTile9.y = y;
                        break;
                    case 10:
                        var spawnedTile10 = Instantiate(tileTypes[10], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile10.name = tileTypes[10].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile10;
                        spawnedTile10.x = x;
                        spawnedTile10.y = y;
                        break;
                    case 11:
                        var spawnedTile11 = Instantiate(tileTypes[11], new Vector3(x, -y), Quaternion.identity);
                        spawnedTile11.name = tileTypes[11].name + " " + x + " " + y;
                        tileGrid[x, y] = spawnedTile11;
                        spawnedTile11.x = x;
                        spawnedTile11.y = y;
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


        foreach (Actor unit in allies) {
            // hard coded bc fml
            int[] coords = unit.GetSpawnCoordinates();
            var newUnit = Instantiate(unit, new Vector3(coords[0], coords[1], coords[2]), Quaternion.identity);
            newUnit.OnTurnStart(tileGrid[coords[0], coords[1]]);
            tileGrid[coords[0], coords[1]].occupiedActor = newUnit;
        }
    }
}
