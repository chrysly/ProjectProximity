using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// fuck imma come back to this AAAA

public class Tile : MonoBehaviour
{
    [SerializeField] TileData tileData;
    public TileData Data() { return tileData; }
    
    public int x;
    public int z;

    public Actor occupiedActor;
    // maybe var here for coordinates actually

    private void Awake() {
    }


    // get the set of tiles that can be reached with range X
    public HashSet<Tile> getTilesInRange(int range, Tile[,] grid) {
        HashSet<Tile> tilesInRange = new HashSet<Tile>();
        //Debug.Log("Checking: " + z + ", " + x);
        int startX = Mathf.Max(0, x - range);
        int endX = Mathf.Min(grid.GetLength(0) - 1, x + range);
        int startZ = Mathf.Max(0, z - range);
        int endZ = Mathf.Min(grid.GetLength(1) - 1, z + range);

        for (int x = startX; x <= endX; x++) {
            for (int y = startZ; y <= endZ; y++) {
                if (Mathf.Abs(x - x) + Mathf.Abs(y - y) <= range) {
                    tilesInRange.Add(grid[y, x]);
                }
            }
        }

        return tilesInRange;
    }

    public HashSet<Tile> GetAdjacentTiles() {

        HashSet<Tile> adjacentTiles = new HashSet<Tile>();
        
        //NORTH
        Tile[,] grid = GridManager.Instance.GetGrid();
        if (x > 0 && grid[z, x - 1].tileData.isWalkable) adjacentTiles.Add(grid[z, x - 1]);
        
        //SOUTH
        if (x < grid.GetLength(1) - 1 && grid[z, x + 1].tileData.isWalkable) adjacentTiles.Add(grid[z, x + 1]);
        
        //EAST
        if (z > 0 && grid[z - 1, x].tileData.isWalkable) adjacentTiles.Add(grid[z - 1, x]);
        
        //WEST
        if (z < grid.GetLength(0) - 1 && grid[z + 1, x].tileData.isWalkable) adjacentTiles.Add(grid[z + 1, x]);

        return adjacentTiles;
    }
    
    #region Pathfinding

    /// <summary>
    /// Distance from starting node
    /// </summary>
    public float gCost;

    /// <summary>
    /// Distance from ending cell node
    /// </summary>
    public float hCost;

    /// <summary>
    /// Sum of g & h cost
    /// </summary>
    public float fCost { get { return gCost + hCost; } }

    public Tile parent;

    #endregion Pathfinding
}
