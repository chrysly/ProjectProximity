using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
    [SerializeField] private ActorData data;
    private float _health;
    private Vector2 _gridPosition;

    public void MoveActor(Vector2 position) {
        //Conditional to check if position is within valid range
    }
}
