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
            currTile = currTile.parent;
        }
        
        path.Reverse();
        return path;
    }

    void CalculatePath(Tile start, Tile target) {
        List<Tile> openSet = new List<Tile>();
        HashSet<Tile> closedTile = new HashSet<Tile>();
        openSet.Add(start);

        while (openSet.Count > 0) {
            Tile currTile = openSet[0];
            for (int i = 1; i < openSet.Count; i++) {
                if (openSet[i].fCost < currTile.fCost ||
                    openSet[i].fCost == currTile.fCost &&
                    openSet[i].hCost < currTile.hCost) {

                    currTile = openSet[i];
                }
            }
        }
    }
}
