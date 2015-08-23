using UnityEngine;
using System.Collections;

public class PlayerCharacter : Character {

    /* METHODS */

    void OnTriggerEnter(Collider other) {
        Debug.Log(LayerMask.LayerToName(other.gameObject.layer));
        if (LayerMask.NameToLayer("Enemy") == other.gameObject.layer) {
            GameMode.Instance.KillPlayer();
        }
    }
}
