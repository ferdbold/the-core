using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    /* ATTRIBUTES */

    private static PlayerController _instance;

    /* COMPONENTS */

    private CameraBehaviour _camera;
    private PlayerCharacter _character;

    /* CONSTRUCTOR */
	
    void Awake() {
        FindComponents();
        _instance = this;
    }

    void Start() {
        _camera.Target = _character.Pawn;
    }

    private void FindComponents() {
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraBehaviour>();
        _character = GameObject.FindWithTag("PlayerCharacter").GetComponent<PlayerCharacter>();
    }
	
    /* METHODS */

	void Update() {
        if (GameMode.Instance.GameIsActive) {
            ApplyMoveInput();
            ApplyJumpInput();
            ApplyProjectileFire();
        } else {
            ApplyRetryInput();
        }
	}

    private void ApplyMoveInput() {
        float horizontalAxis = Input.GetAxis("Horizontal");

        if (horizontalAxis != 0) {
            _character.ApplyMoveFactor(horizontalAxis);
            _camera.ApplyOffsetFactor(horizontalAxis);
        }
    }

    private void ApplyJumpInput() {
        bool jumpButton = Input.GetButtonDown("Jump");

        if (jumpButton) {
            _character.Jump();
        }
    }

    private void ApplyProjectileFire() {
        bool fireButton = Input.GetButtonDown("Fire");

        if (fireButton) {
            _character.Fire();
        }
    }

    private void ApplyRetryInput() {
        bool retryButton = Input.GetButtonDown("Retry");

        if (retryButton) {
            GameMode.Instance.OnRetry();
        }
    }

    public void OnGameEnd() {
        _character.OnGameEnd();
    }

    /* PROPERTIES */

    public static PlayerController Instance {
        get { return _instance; }
    }
}
