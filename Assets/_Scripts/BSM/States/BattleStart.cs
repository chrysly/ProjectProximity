using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleStateMachine {
    public class BattleStart : BattleState {

        public override void Enter(BattleStateInput i) {
            Debug.Log("battle start");
            GridManager.Instance.GenerateGrid();
        }

        public override void FixedUpdate() {
            base.Update();
        }

        public override void Update() {
            base.Update();
            bool pressed = MySM.gameLogic.BattleWait();
            if (pressed) { MySM.Transition<AllyTurn>();  }
        }

        public override void Exit(BattleStateInput i) {
            base.Exit(i);
        }
    }
}
