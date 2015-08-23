using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour {

    /* METHODS */

    void OnTriggerEnter(Collider other) {
        if (LayerMask.NameToLayer("Projectile") == other.gameObject.layer) {
            GameObject.Destroy(other.gameObject);
        }
    }
}
