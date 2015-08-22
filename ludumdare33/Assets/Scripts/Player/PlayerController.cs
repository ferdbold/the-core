using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    /* ATTRIBUTES */
    private CameraBehaviour _camera;
    private PlayerCharacter _character;

    /* CONSTRUCTOR */
	void Start () {
        FindComponents();
	}

    private void FindComponents() {
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraBehaviour>();
        _character = GameObject.FindWithTag("Pawn").GetComponent<PlayerCharacter>();
    }
	
    /* METHODS */
	void Update () {
        ApplyMove();
	}

    private void ApplyMove() {
        float horizontalAxis = Input.GetAxis("Horizontal");

        if (horizontalAxis != 0) {
            
        }
    }
}
