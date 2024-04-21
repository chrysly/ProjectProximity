using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// fuck imma come back to this AAAA

public class Tile : MonoBehaviour
{
    [SerializeField] TileData tileData;
    public GameObject unitOnTile;
    // maybe var here for coordinates actually


    // get the set of tiles that can be reached with range X
    public HashSet<Tile> getTilesInRange(int range, Tile t) {
        return null;
    }
}
