using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleStateMachine {
    public class AllyTurn : BattleState {

        public override void Enter(BattleStateInput i) {
            Debug.Log("ally turn");
            MySM.gameLogic.AllyTurnStart();
        }

        public override void Update() {
            base.Update();
            MySM.gameLogic.AllyTurnLoop();
        }

        public override void Exit(BattleStateInput i) {
            base.Exit(i);
        }
    }
}
