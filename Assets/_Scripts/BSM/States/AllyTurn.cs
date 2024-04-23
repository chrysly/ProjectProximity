using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleStateMachine {
    public class AllyTurn : BattleState {

        public override void Enter(BattleStateInput i) {
            base.Enter(i);
            Debug.Log("ally turn start");
            MySM.CurrInput.lastRefState = this;
            MySM.allyTurnHandler.AllyTurnStart();
        }

        public override void Update() {
            base.Update();
            MySM.allyTurnHandler.AllyTurnUpdate();
        }

        public override void Exit(BattleStateInput i) {
            base.Exit(i);
        }
    }
}
