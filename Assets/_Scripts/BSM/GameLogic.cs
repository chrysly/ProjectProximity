using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Logic for the actual game loop
/// </summary>
public class GameLogic : MonoBehaviour
{
    [SerializeField] private List<Actor> allies = new List<Actor>();
    [SerializeField] private List<Actor> enemies = new List<Actor>();
    [SerializeField] private Camera cam;

    private int numUnitsMoved = 0;
    private Actor selectedUnit;

    /// <summary>
    /// Resets ally units at the start of the turn
    /// </summary>
    public void AllyTurnStart() {
        numUnitsMoved = 0;
        foreach (Actor a in allies) {
            a.hasMoved = false;
        }
    }

    /// <summary>
    /// The ally turn game loop
    /// </summary>
    public void AllyTurnLoop() {
        if (Input.GetMouseButtonDown(0) && selectedUnit == null) {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null) {
                Debug.Log("bop");
                Actor unit = hit.collider.GetComponent<Actor>();
                if (unit != null) { 
                    selectedUnit = unit;
                    Debug.Log("unit selected: " + unit.name);
                }
            }
        } else if (Input.GetMouseButtonDown(0) && selectedUnit != null) {
            // if a tile is hit, send it to the move actor (it will check there if its a valid place to move
            // and if it is move the actor and 
        } else if (Input.GetMouseButtonDown(1)) {
            selectedUnit = null;

        }
        // move units and once all units have been moved end turn
        // if a valid unit is selected and then a valid tile is selected move there?

        // of the tile that was clicked on is in curr_moveRange 
    }

    /// <summary>
    /// the battle starts when the player presses space
    /// </summary>
    public bool BattleWait() {
        Debug.Log("please give me space");
        if (Input.GetKeyDown("space")) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get either ally units (0) or enemy units (1)
    /// </summary>
    public List<Actor> GetUnits(int t) {
        if (t == 0) return allies;
        return enemies;
    }

    // if an actor dies get event from the ActorHandler 
}
