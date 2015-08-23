using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Character : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */

    [Header("Movement")]
    public float maxSpeed = 5.0f;
    public float accel = 3.0f;
    public float decay = 0.95f;
    public float deadZone = 0.01f;
    public float xVelocity;
    
    [Header("Jump")]
    public float gravity = 0.15f;
    public float jumpPower = 0.5f;

    [Header("Fire")]
    public Transform projectilePrefab;
    public float fireAnimKickback = 0.7f;
    public float fireAnimDuration = 0.3f;

    /* COMPONENTS */

    private Transform _pivot;
    private Transform _pawn;

    /* ATTRIBUTES */

    private float _pawnDistToGround;
    private float _initialPawnZ;
    private float _pawnZ;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
	}

    protected virtual void FindComponents() {
        Collider pawnCollider;

        _pivot = transform.Find("Pivot");
        _pawn = _pivot.Find("Pawn");
        _pawnZ = _pawn.localPosition.z;
        _initialPawnZ = _pawnZ;

        pawnCollider = _pawn.GetComponent<Collider>();
        _pawnDistToGround = pawnCollider.bounds.extents.y;
    }
	
    /* METHODS */

    public virtual void Update() {
        Move();
        ApplyFireAnimation();
	}

    /// <summary>
    /// Apply a moving force to the character.
    /// </summary>
    /// <param name="factor">The move factor. Ranges from -1 to 1 to move from left to right respectively.</param>
    public void ApplyMoveFactor(float factor) {
        float moveAmount = factor * this.accel * Time.deltaTime;

        this.xVelocity = Mathf.Clamp(this.xVelocity + moveAmount, -this.maxSpeed, this.maxSpeed);
    }

    /// <summary>
    /// Moves the character around the arena. Called every frame.
    /// </summary>
    private void Move() {
        // Apply decay
        this.xVelocity *= this.decay;

        // Handle deadzone
        this.xVelocity = (Mathf.Abs(this.xVelocity) > this.deadZone) ? this.xVelocity : 0;

        // Apply movement rotation
        _pivot.Rotate(new Vector3(0, 1, 0), -this.xVelocity);
    }

    /// <summary>
    /// Apply a vertical force when jumping.
    /// </summary>
    public void Jump() {
        if (!IsJumping) {
            Rigidbody pawnRb = _pawn.GetComponent<Rigidbody>();
            pawnRb.AddForce(transform.up * this.jumpPower);
        }
    }

    /// <summary>
    /// Fire a projectile.
    /// </summary>
    public void Fire() {
        Instantiate(this.projectilePrefab, _pawn.position, _pawn.rotation);

        
        DOTween.To(x => _pawnZ = x, _initialPawnZ - this.fireAnimKickback, _initialPawnZ, this.fireAnimDuration);
    }

    /// <summary>
    /// Apply attack anim kickback.
    /// </summary>
    private void ApplyFireAnimation() {
        _pawn.localPosition = new Vector3(
            _pawn.localPosition.x,
            _pawn.localPosition.y,
            _pawnZ
        );
    }

    /* PROPERTIES */

    private bool IsJumping {
        get {
            return !Physics.Raycast(_pawn.position, -Vector3.up, _pawnDistToGround + 0.1f);
        }
    }

    public Transform Pawn {
        get { return _pawn; }
    }
}
