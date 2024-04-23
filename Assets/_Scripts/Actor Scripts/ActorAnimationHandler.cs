using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using DG.Tweening;
using UnityEngine;

public class ActorAnimationHandler : MonoBehaviour
{
    private MouseManager _mouseManager;

    private void Awake() {
        _mouseManager = FindObjectOfType<MouseManager>();
        _mouseManager.OnMovedUnit += TranslateActor;
    }

    private void TranslateActor(Tile source, Tile target) {
        Actor actor = source.occupiedActor;
        Pathfinding pathfinding = new Pathfinding();
        List<Tile> path = pathfinding.CalculatePath(source, target, GridManager.Instance.GetGrid());
        StartCoroutine(TranslateAction(actor, path));
    }

    private IEnumerator TranslateAction(Actor actor, List<Tile> path) {
        float delay = 1.8f / path.Count;
        foreach (Tile tile in path) {
            actor.transform.DOMove(tile.transform.position, delay - 0.1f);
            yield return new WaitForSeconds(delay);
        }

        yield return null;
    }
}
