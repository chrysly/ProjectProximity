using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public partial class
    BattleStateMachine : StateMachine<BattleStateMachine, BattleStateMachine.BattleState, BattleStateInput> {

    [SerializeField] private GameLogic gameLogic;
    [SerializeField] private AllyTurnHandler allyTurnHandler;
    [SerializeField] private ActorHandler actorHandler;

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
        actorHandler.OnUnitDefeated += UnitDefeated;
        actorHandler.OnToAnimateState += EnterAnimateState;
        
    }

    protected override void SetInitialState() {
        Transition<BattleStart>();
    }

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

    private void AllyTurnEnd() {

    }

    public void EnterAnimateState() {
        Transition<AnimateState>();
    }

    private void UnitDefeated(Actor a) {
        if (a.GetType() == typeof(AllyActor)) {
            CurrInput.aliveAllies.Remove(a);
        } else if (a.GetType() == typeof(EnemyActor)) {
            CurrInput.aliveEnemies.Remove(a);
        }
    }

    protected bool CallAnimDelay() {
        StartCoroutine(WaitForAnim(CurrInput.AnimationDelay));
        return true;
    }

    IEnumerator WaitForAnim(int sec) {
        yield return new WaitForSeconds(sec);
    }
}
