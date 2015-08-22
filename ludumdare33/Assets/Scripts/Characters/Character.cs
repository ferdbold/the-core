using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    /* COMPONENTS */

    private Transform _pivot;
    private Transform _pawn;

    /* ATTRIBUTES */
    private float _pawnDistToGround;

    /* PUBLIC ATTRIBUTES */

    [Header("Movement")]
    public float maxSpeed = 5.0f;
    public float accel = 3.0f;
    public float decay = 0.95f;
    public float deadZone = 0.01f;
    public Vector3 velocity;
    
    [Header("Jump")]
    public float gravity = 0.15f;
    public float jumpPower = 0.5f;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
	}

    private void FindComponents() {
        Collider pawnCollider;

        _pivot = transform.Find("Pivot");
        _pawn = _pivot.Find("Pawn");

        pawnCollider = _pawn.GetComponent<Collider>();
        _pawnDistToGround = pawnCollider.bounds.extents.y;
    }
	
    /* METHODS */

    public virtual void Update() {
        Move();

        Rigidbody pawnRb = _pawn.GetComponent<Rigidbody>();
        Debug.Log(pawnRb.velocity.y);
	}

    /// <summary>
    /// Apply a moving force to the character.
    /// </summary>
    /// <param name="factor">The move factor. Ranges from -1 to 1 to move from left to right respectively.</param>
    public void ApplyMoveFactor(float factor) {
        float moveAmount = factor * this.accel * Time.deltaTime;

        this.velocity.x = Mathf.Clamp(this.velocity.x + moveAmount, -this.maxSpeed, this.maxSpeed);
    }

    /// <summary>
    /// Moves the character around the arena. Called every frame.
    /// </summary>
    private void Move() {
        Vector3 origPosition = _pawn.localPosition;

        // Apply decay and gravity
        this.velocity.x *= this.decay;

        // Handle deadzone
        this.velocity.x = (Mathf.Abs(this.velocity.x) > this.deadZone) ? this.velocity.x : 0;

        // Apply movement rotation
        _pivot.Rotate(new Vector3(0, 1, 0), -this.velocity.x);
        
        // Apply jumping translation
        origPosition.y += this.velocity.y;
        _pawn.localPosition = origPosition;
    }

    public void Jump() {
        Debug.Log(IsJumping);
        if (!IsJumping) {
            Rigidbody pawnRb = _pawn.GetComponent<Rigidbody>();
            pawnRb.AddForce(transform.up * this.jumpPower);
        }
    }

    /* PROPERTIES */

    private bool IsJumping {
        get {
            return !Physics.Raycast(_pawn.localPosition, -Vector3.up, _pawnDistToGround + 0.1f);
        }
    }
}
