using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// fuck imma come back to this AAAA

public class Tile : MonoBehaviour
{
    [SerializeField] TileData tileData;
    public GameObject unitOnTile;
    public int x;
    public int y;
    // maybe var here for coordinates actually


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
                    Debug.Log("added tile");
                }
            }
        }

        return tilesInRange;
    }
    
    #region Pathfinding

    /// <summary>
    /// Distance from starting node
    /// </summary>
    public int gCost;
    
    /// <summary>
    /// Distance from ending cell node
    /// </summary>
    public int hCost;
    
    /// <summary>
    /// Sum of g & h cost
    /// </summary>
    public int fCost { get { return gCost + hCost; } }

    #endregion Pathfinding
}
