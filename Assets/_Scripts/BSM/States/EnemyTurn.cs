using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public partial class BattleStateMachine {
    public class EnemyTurn : BattleState {
        public override void Enter(BattleStateInput i) {
            base.Enter(i);
            Debug.Log("enemy turn start");
        }

        public override void Update() {
            base.Update();
            if (MySM.CallAnimDelay()) { MySM.Transition<AllyTurn>();  }
        }

        public override void Exit(BattleStateInput i) {
            Debug.Log("enemy turn end");
            base.Exit(i);
        }
    }
}
