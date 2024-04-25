using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField] private ActorHandler actorHandler;
    
    #region Singleton
    private static GridManager _instance;

    public static GridManager Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null) {  Destroy(this.gameObject); }
        else { _instance = this; }

        //intGrid = Transpose(inputGrid);
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

        {0,0,0,0,0,0,0,9,9,9,9,9,0,0,0,0,0},
        {0,0,0,9,9,9,9,9,9,9,9,9,9,0,0,0,0},
        {0,9,9,9,9,9,9,1,5,1,9,9,9,0,0,0,0},
        {9,9,9,9,1,10,7,1,5,1,1,1,9,9,0,0,9},
        {9,9,1,7,1,5,1,1,1,8,5,2,9,9,9,9,9},
        {0,0,1,5,1,6,1,5,5,1,5,4,9,9,9,9,0},
        {0,4,4,1,5,1,4,2,1,4,9,11,9,9,0,0,0},
        {0,5,1,1,1,1,1,9,11,11,9,1,1,0,0,0,0},
        {1,1,1,5,1,5,9,9,5,5,10,2,5,0,0,0,0},
        {0,1,2,2,9,11,9,2,5,4,1,8,1,0,0,0,0},
        {0,1,1,9,9,5,1,1,1,1,5,2,1,1,0,0,0},
        {0,0,9,9,9,4,4,2,1,1,2,1,4,4,0,0,0},
        {0,0,9,9,4,5,6,7,5,4,10,4,5,0,0,0,0},
        {0,0,0,9,5,1,1,8,1,1,1,1,1,0,0,0,0},
        {0,0,0,9,9,1,4,1,5,5,1,2,5,0,0,0,0},
        {0,0,0,0,9,9,9,2,4,1,1,1,6,0,0,0,0},
        {0,0,0,0,9,9,9,1,1,1,8,0,0,0,0,0,0},
        {0,0,0,0,9,9,9,1,1,1,0,0,0,0,0,0,0},
        {0,0,0,0,9,9,9,1,1,1,0,0,0,0,0,0,0},
        {0,0,0,0,0,9,9,1,0,0,0,0,0,0,0,0,0},

        //{1,1,1 },
        //{1,1,2 },
        //{1,2,2 },

    };

    private Tile[,] tileGrid;
    public Tile[,] GetGrid() { return tileGrid; }

    void Start() {
        tileGrid = new Tile[inputGrid.GetLength(0), inputGrid.GetLength(1)];
    }

    // generates a grid of tiles populated by class Tile
    public void GenerateGrid() {
        int placementZ = inputGrid.GetLength(1) - 1;
        for (int z = 0; z < inputGrid.GetLength(0); z++) {
            for (int x = 0; x < inputGrid.GetLength(1); x++) {
                Tile tileToSpawn = tileTypes.ElementAt(inputGrid[z, x]);
                SpawnTile(tileToSpawn, z, x, placementZ, x);
            }
            placementZ--;
        }
        PlaceUnits();   // spawn units on field
    }

    // why didn't i make a fcking method for this before
    private void SpawnTile(Tile tileToSpawn, int z, int x, int pz, int px) {
        var spawnedTile = Instantiate(tileToSpawn, new Vector3(px, 0f, pz), Quaternion.identity);
        spawnedTile.name = tileToSpawn.name + " " + z + " " + x;
        tileGrid[z, x] = spawnedTile;
        spawnedTile.x = x;
        spawnedTile.z = z;
    }

    /// <summary>
    /// places units on the board (hard coded bc fml)
    /// </summary>
    private void PlaceUnits() {
        List<AllyActor> allies = actorHandler.allyActors;
        List<EnemyActor> enemies = actorHandler.enemyActors;
        Tile[,] grid = GridManager.Instance.GetGrid();

        foreach (Actor unit in allies) {
            // hard coded bc fml
            int[] coords = unit.GetSpawnCoordinates();
            Vector3 pos = grid[coords[0], coords[1]].transform.position;
            pos = new Vector3(pos.x, pos.y + 1f, pos.z);
            var newUnit = Instantiate(unit, pos, Quaternion.identity);
            newUnit.OnTurnStart(tileGrid[coords[0], coords[1]]);
            tileGrid[coords[0], coords[1]].occupiedActor = newUnit;
            Debug.Log("SPAWNED ALLY");
        }
        
        foreach (Actor unit in enemies) {
            // hard coded bc fml
            int[] coords = unit.GetSpawnCoordinates();
            var newUnit = Instantiate(unit, new Vector3(coords[0], coords[2], coords[1]), Quaternion.identity);
            newUnit.OnTurnStart(tileGrid[coords[0], coords[1]]);
            tileGrid[coords[0], coords[1]].occupiedActor = newUnit;
        }
    }
}
