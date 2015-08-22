using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    /* COMPONENTS */
    private Transform _pivot;
    private Transform _pawn;

    /* PUBLIC ATTRIBUTES */
    public float maxSpeed = 5.0f;
    public float accel = 3.0f;
    public float decay = 0.95f;
    public float deadZone = 0.01f;
    public float velocity;

    /* CONSTRUCTOR */
    void Awake() {
        FindComponents();
	}

    private void FindComponents() {
        _pivot = transform.Find("Pivot");
        _pawn = _pivot.Find("Pawn");
    }
	
    /* METHODS */
    public virtual void Update() {
        Move();
	}

    /// <summary>
    /// Apply a moving force to the character.
    /// </summary>
    /// <param name="factor">The move factor. Ranges from -1 to 1 to move from left to right respectively.</param>
    public void ApplyMoveFactor(float factor) {
        float moveAmount = -factor * this.accel * Time.deltaTime;

        this.velocity = Mathf.Clamp(this.velocity + moveAmount, -this.maxSpeed, this.maxSpeed);
    }

    /// <summary>
    /// Moves the character around the arena. Called every frame.
    /// </summary>
    private void Move() {
        // Decay the velocity
        this.velocity *= this.decay;

        // Kill velocity if it's beneath the deadzone
        this.velocity = (Mathf.Abs(this.velocity) > this.deadZone) ? this.velocity : 0;

        // Apply velocity to the character
        _pivot.Rotate(new Vector3(0, 1, 0), this.velocity);
    }
}
