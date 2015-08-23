using UnityEngine;
using System.Collections;

public class PlayerPan : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        transform.parent.parent.SendMessage("OnTriggerEnter", other);
    }
}
