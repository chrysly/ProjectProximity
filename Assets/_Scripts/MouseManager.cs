using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    // ig store currently highlighted tiles

    public void HighlightTiles(HashSet<Tile> moveTiles, HashSet<Tile> attackTiles) {
        //foreach(Tile t in moveTiles) {
        //    t.GetComponent<SpriteRenderer>().color = Color.blue;
        //}
        //foreach (Tile t in attackTiles) {
        //    t.GetComponent<SpriteRenderer>().color = Color.red;
        //}
    }

    public void UnhighlightTiles() {

    }

    public void CharacterSelected(Actor currActor) {

    }
}
