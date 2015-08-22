using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */
    
    public int enemyLayer;
    public float moveSpeed = 2.0f;
	
	void Update () {
        Move();
	}

    private void Move() {
        transform.Translate(Vector3.forward * this.moveSpeed * Time.deltaTime);
    }
}
