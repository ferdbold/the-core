using UnityEngine;
using System.Collections;

public class WorldBounds : MonoBehaviour {
	
    /* METHODS */

    void OnTriggerExit(Collider other) {
        GameObject.Destroy(other.gameObject);
    }
}
