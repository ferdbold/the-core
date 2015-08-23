using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */

    public float _timeLeft = 30.0f;

    /* COMPONENTS */

    private HUD _hud;

    /* CONSTRUCTOR */

	void Awake() {
        FindComponents();
    }

    private void FindComponents() {
        _hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
    }
	
    /* METHODS */

	void Update() {
        TickClock();
	}

    /// <summary>
    /// Tick the game clock.
    /// </summary>
    private void TickClock() {
        _timeLeft -= Time.deltaTime;
        _hud.SetClock(_timeLeft);

        if (_timeLeft < 0) {
            EndGame();
        }
    }

    private void EndGame() {

    }
}
