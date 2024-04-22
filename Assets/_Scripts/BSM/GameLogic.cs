using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private List<Actor> allies = new List<Actor>();
    [SerializeField] private List<Actor> enemies = new List<Actor>();
    [SerializeField] private Camera cam;

    private int numUnitsMoved = 0;
    private Actor selectedUnit;

    private void AllyTurnStart() {
        numUnitsMoved = 0;
        foreach (Actor a in allies) {
            a.hasMoved = false;
        }
    }

    private void AllyTurnLoop() {

        if (Input.GetMouseButtonDown(0) && selectedUnit == null) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {

            }

        } else {

        }
        // move units and once all units have been moved end turn
        // if a valid unit is selected and then a valid tile is selected move there?

        // of the tile that was clicked on is in curr_moveRange 
    }

    public bool BattleWait() {
        Debug.Log("please give me space");
        if (Input.GetKeyDown("space")) {
            return true;
        }
        return false;
    }

    public List<Actor> GetUnits(int t) {
        if (t == 0) return allies;
        return enemies;
    }
}
