using UnityEngine;
using System.Collections;

public class PlayerPawn : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        transform.parent.parent.SendMessage("OnTriggerEnter", other);
    }
}
