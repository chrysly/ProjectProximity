using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Some external game logic for BSM States
/// </summary>
public class GameLogic : MonoBehaviour
{
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
}
