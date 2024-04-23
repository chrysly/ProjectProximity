using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleStateMachine {
    public class AnimateState : BattleState {

        public override void Enter(BattleStateInput i) {
            base.Enter(i);
        }

        // yo i can def see this causing issues future me make sure to check this lol
        public override void Update() {
            base.Update();
            if (BattleStateMachine.Instance.CallAnimDelay()) {
                BattleState prevState = BattleStateMachine.Instance.CurrInput.lastRefState;
                if (prevState.GetType() == typeof(AllyTurn)) {
                    BattleStateMachine.Instance.SetState<AllyTurn>();   // i think i just set not transition
                } else if (prevState.GetType() == typeof(EnemyTurn)) {
                    BattleStateMachine.Instance.SetState<EnemyTurn>();
                }
            }
        }

        public override void Exit(BattleStateInput i) {
            base.Exit(i);
        }
    }
}
