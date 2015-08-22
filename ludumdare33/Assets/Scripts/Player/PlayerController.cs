using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    /* COMPONENTS */
    private CameraBehaviour _camera;
    private PlayerCharacter _character;

    /* CONSTRUCTOR */
	void Awake () {
        FindComponents();
	}

    private void FindComponents() {
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraBehaviour>();
        _character = GameObject.FindWithTag("PlayerCharacter").GetComponent<PlayerCharacter>();
    }
	
    /* METHODS */
	void Update () {
        ApplyMove();
	}

    private void ApplyMove() {
        float horizontalAxis = Input.GetAxis("Horizontal");

        _character.Move(horizontalAxis);

        Debug.Log(horizontalAxis);
    }
}
