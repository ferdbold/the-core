using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    /* COMPONENTS */
    private CameraBehaviour _camera;
    private PlayerCharacter _character;

    /* CONSTRUCTOR */
	void Awake() {
        FindComponents();
	}

    private void FindComponents() {
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraBehaviour>();
        _character = GameObject.FindWithTag("PlayerCharacter").GetComponent<PlayerCharacter>();
    }
	
    /* METHODS */
	void FixedUpdate() {
        ApplyMoveInput();
	}

    private void ApplyMoveInput() {
        float horizontalAxis = Input.GetAxis("Horizontal");

        if (horizontalAxis != 0) {
            _character.ApplyMoveFactor(horizontalAxis);
            _camera.SetOffset(horizontalAxis);
        }
    }
}
