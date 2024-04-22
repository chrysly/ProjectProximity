using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for persistant data
public class BattleStateInput : StateInput
{
    #region Global Variables
    public List<Actor> allies;
    public List<Actor> enemies;

    private int turnNum = 0;
    #endregion
}
