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

        if (distanceX > distanceY) return 1;
    }
}
