using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// fuck imma come back to this AAAA

public class Tile : MonoBehaviour
{
    [SerializeField] TileData tileData;
    public TileData Data() { return tileData; }
    
    public GameObject unitOnTile;
    public int x;
    public int y;
    // maybe var here for coordinates actually

    private void Awake() {
        InitDisplay();
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

    public Tile parent;

    #endregion Pathfinding
    
    #region Select Operations

    private Selectable _publisher;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;

    private void InitDisplay() {
        _publisher = transform.GetComponent<Selectable>();
        _publisher.OnClick += SelectTile;
        _publisher.OnEnter += EnterTile;
        _publisher.OnExit += DeselectTile;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    private void SelectTile() {
        _spriteRenderer.color = Color.cyan;
    }

    private void EnterTile() {
        _spriteRenderer.color = Color.yellow;
        Pathfinding pathfinding = new Pathfinding();
        List<Tile> path =
            pathfinding.CalculatePath(GridManager.Instance.GetGrid()[0, 0], this, GridManager.Instance.GetGrid());
        
    }

    private void DeselectTile() {
        _spriteRenderer.color = _originalColor;
    }
    
    #endregion Select Operations
}
