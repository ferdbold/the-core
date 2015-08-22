using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	void Start () {
	    
	}
	
	void Update () {
        ApplyMove();
	}

    private void ApplyMove() {
        float horizontalAxis = Input.GetAxis("Horizontal");

        if (horizontalAxis != 0) {
            
        }
    }
}
