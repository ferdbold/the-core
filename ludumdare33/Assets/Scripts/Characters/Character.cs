using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    /* COMPONENTS */
    private Transform _pivot;
    private Transform _pawn;

    /* PUBLIC ATTRIBUTES */
    public float moveSpeed = 1.0f;

    /* CONSTRUCTOR */
    void Awake () {
        FindComponents();
	}

    private void FindComponents() {
        _pivot = transform.Find("Pivot");
        _pawn = _pivot.Find("Pawn");
    }
	
    /* METHODS */
    void Update () {
	    
	}

    /// <summary>
    /// Move the character along the arena.
    /// </summary>
    /// <param name="factor">The move factor. Ranges from -1 to 1 to move from left to right respectively.</param>
    public void Move(float factor) {
        float moveAmount = -factor * moveSpeed * Time.deltaTime;

        _pivot.Rotate(new Vector3(0, 1, 0), moveAmount);
    }
}
