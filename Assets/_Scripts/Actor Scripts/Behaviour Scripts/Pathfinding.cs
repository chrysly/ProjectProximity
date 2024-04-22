using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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

    public List<Tile> CalculatePath(Tile start, Tile target, Tile[,] grid) {
        List<Tile> openSet = new List<Tile>();
        HashSet<Tile> closedSet = new HashSet<Tile>();
        openSet.Add(start);

        foreach (Tile tile in grid) {
            tile.gCost = int.MaxValue;
            tile.parent = null;
        }

        start.gCost = 0;
        start.hCost = CalculateDistance(start, target);

        while (openSet.Count > 0) {
            Tile currTile = openSet[0];
            for (int i = 1; i < openSet.Count; i++) {
                if (openSet[i].fCost < currTile.fCost ||
                    openSet[i].fCost == currTile.fCost &&
                    openSet[i].hCost < currTile.hCost) {

                    currTile = openSet[i];
                }
            }

            openSet.Remove(currTile);
            closedSet.Add(currTile);

            if (currTile == target) {
                return TracePath(start, target);
            }

            foreach (Tile neighbour in currTile.getTilesInRange(1, grid)) {
                if (!neighbour.Data().isWalkable || closedSet.Contains(neighbour)) continue;
                int costToNeighbour = currTile.gCost + CalculateDistance(currTile, neighbour);
                if (costToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = costToNeighbour;
                    neighbour.hCost = CalculateDistance(neighbour, target);
                    neighbour.parent = currTile;
                    
                    if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                }
            }
        }

        return null;
    }
}
