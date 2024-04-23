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
    public int y;

    public Actor occupiedActor;
    // maybe var here for coordinates actually

    private void Awake() {
    }


    // get the set of tiles that can be reached with range X
    public HashSet<Tile> getTilesInRange(int range, Tile[,] grid) {
        HashSet<Tile> tilesInRange = new HashSet<Tile>();

        int startX = Mathf.Max(0, x - range);
        int endX = Mathf.Min(grid.GetLength(0) - 1, x + range);
        int startY = Mathf.Max(0, y - range);
        int endY = Mathf.Min(grid.GetLength(1) - 1, y + range);

        for (int x = startX; x <= endX; x++) {
            for (int y = startY; y <= endY; y++) {
                if (Mathf.Abs(x - x) + Mathf.Abs(y - y) <= range) {
                    tilesInRange.Add(grid[x, y]);
                }
            }
        }

        return tilesInRange;
    }

    public HashSet<Tile> GetAdjacentTiles() {

        HashSet<Tile> adjacentTiles = new HashSet<Tile>();
        
        //NORTH
        Tile[,] grid = GridManager.Instance.GetGrid();
        if (y > 0) adjacentTiles.Add(grid[x, y - 1]);
        
        //SOUTH
        if (y < grid.GetLength(0) - 1) adjacentTiles.Add(grid[x, y + 1]);
        
        //EAST
        if (x > 0) adjacentTiles.Add(grid[x - 1, y]);
        
        //WEST
        if (x < grid.GetLength(1) - 1) adjacentTiles.Add(grid[x + 1, y]);
        
        Debug.Log("Curr Tile: " + x + ", " + y);
        foreach (Tile tile in adjacentTiles) {
            Debug.Log("Tile in range: " + tile.x + ", " + tile.y);
        }

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
