using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    /* COMPONENTS */

    private ClockWidget _clock;
    private GameOverWidget _gameOver;

    /* CONSTRUCTOR */

	void Awake () {
        FindComponents();
	}

    private void FindComponents() {
        _clock = transform.Find("Clock").GetComponent<ClockWidget>();
        _gameOver = transform.Find("GameOver").GetComponent<GameOverWidget>();
    }

    /* METHODS */

    /// <summary>
    /// Set the game clock to a certain number.
    /// </summary>
    /// <param name="time">The time to set the clock to</param>
    public void SetClock(float time) {
        _clock.TimeLeft = time;
    }

    /// <summary>
    /// Display game over UI.
    /// </summary>
    public void OnGameEnd() {
        _gameOver.Toggle(true);
    }
}
