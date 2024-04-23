using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actor Data")]
public class ActorData : ScriptableObject {
    //istg if someone turns these into properties >:O
    // ;)))) (jk)
    // >_>
    public float attackPower = 10f;
    public float maxHealth = 10f;
    public int attackRange = 1;
    public int moveRange = 3;
    
    //THIS IS SCUFFED BUT I DONT CARE ANYMORE HAHAHAHAHAHAHAHAHAHA - crhis
    [Header("Spawn Coordinates")] public int x, y, z = 0;
}
