using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    /**
     * A* pathfinding implementation for actors.
     */

    private int CalculateDistance(Tile tileA, Tile tileB) {
        int distanceX = Mathf.Abs(tileA.x - tileB.x);
        int distanceY = Mathf.Abs(tileA.y - tileB.y);

        if (distanceX > distanceY) return distanceY * (distanceX - distanceY);
        return distanceX * (distanceY - distanceX);
    }

    private List<Tile> TracePath(Tile start, Tile target) {
        List<Tile> path = new List<Tile>();
        Tile currTile = target;

        while (currTile != start) {
            path.Add(currTile);
        }

        return null;
    }
}
