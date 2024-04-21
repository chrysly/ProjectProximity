using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actor Data")]
public class ActorData : ScriptableObject {
    //istg if someone turns these into properties >:O
    public float attackPower = 10f;
    public float maxHealth = 10f;
    public float attackRange = 1f;
    public float moveRange = 3f;
}
