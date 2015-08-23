using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */
    
    public float moveSpeed = 2.0f;
	
    /* METHODS */

	void Update () {
        Move();
	}

    private void Move() {
        transform.Translate(Vector3.forward * this.moveSpeed * Time.deltaTime);
    }

    /* COMPONENTS */

    public float MoveSpeed {
        get { return this.moveSpeed; }
        set { this.moveSpeed = value; }
    }
}
