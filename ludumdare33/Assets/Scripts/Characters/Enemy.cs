using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */

    [Header("Movement")]
    public float moveSpeed = 2.0f;
    public float direction = 1.0f;

    /* METHODS */

    void Update() {
        if (GameMode.Instance.GameIsActive) {
            transform.Rotate(
                new Vector3(0, 1, 0),
                this.moveSpeed * direction * Time.deltaTime
            );
        }
    }
}
