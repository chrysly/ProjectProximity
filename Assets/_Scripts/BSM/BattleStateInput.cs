using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For persistant data between states
/// </summary>
public class BattleStateInput : StateInput
{
    #region Global Variables
    public List<Actor> allies;
    public List<Actor> enemies;

    public List<Actor> aliveAllies;
    public List<Actor> aliveEnemies;

    private int turnNum = 0;
    #endregion

    public void IncrTurnNum() {
        turnNum++;
    }

    public void RemoveUnit() {
        // remove the unit from the list
        
    }
}
