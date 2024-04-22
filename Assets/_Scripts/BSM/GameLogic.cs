using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{

    void Update()
    {
        
    }

    public bool BattleWait() {
        Debug.Log("please give me space");
        if (Input.GetKeyDown("space")) {
            return true;
        }
        return false;
    }
}
