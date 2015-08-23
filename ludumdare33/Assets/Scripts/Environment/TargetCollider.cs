using UnityEngine;
using System.Collections;

public class TargetCollider : MonoBehaviour {

    /* METHODS */

    void OnTriggerEnter(Collider other) {
        if (LayerMask.NameToLayer("Projectile") == other.gameObject.layer) {
            GameObject.Destroy(other.gameObject);
            transform.parent.SendMessage("OnHit");
        }
    }
}
