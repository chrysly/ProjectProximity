using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public partial class
    BattleStateMachine : StateMachine<BattleStateMachine, BattleStateMachine.BattleState, BattleStateInput> {

    [SerializeField] private GameLogic gameLogic;

    #region Singleton
    private static BattleStateMachine instance;
    public static BattleStateMachine Instance => instance;
    #endregion

    protected override void Awake() {
        base.Awake();
        if (instance != null ) { Destroy(this.gameObject); }
        else { instance = this; }

        // game manager update state event
        GameManager.OnStateTransition += ChangeGameState;
        ActorHandler.OnUnitDefeated += UnitDefeated;
        
    }

    protected override void SetInitialState() {
        Transition<BattleStart>();
    }

    /// <summary>
    /// gets info from game manager to change the game state
    /// </summary>
    private void ChangeGameState(GameManager.GameState nextState) {
        switch (nextState) {
            case GameState.Menu:
                break;
            case GameState.Options:
                break;
            case GameState.Game:
                break;
            case GameState.Lose:
                break;
            case GameState.Win:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// when ally turn ends switch to enemy turn
    /// </summary>
    private void AllyTurnEnd() {

    }



    /// <summary>
    /// if a unit is defeated update the battle state input
    /// </summary>
    private void UnitDefeated(Actor a) {
        if (a.GetType() == typeof(AllyActor)) {
            CurrInput.aliveAllies.Remove(a);
        } else if (a.GetType() == typeof(EnemyActor)) {
            CurrInput.aliveEnemies.Remove(a);
        }
    }

    // method that checks for state
    // send data wahoo
    // event from BSM to GM whther it wins

    // selection state
    // animation state
    // state to check for input
    // another state as buffer
}
