using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    /* COMPONENTS */

    private ClockWidget _clock;
    private GameOverWidget _gameOver;
    private TargetWidget _targets;

    /* CONSTRUCTOR */

	void Awake () {
        FindComponents();
	}

    private void FindComponents() {
        _clock = transform.Find("Clock").GetComponent<ClockWidget>();
        _gameOver = transform.Find("GameOver").GetComponent<GameOverWidget>();
        _targets = transform.Find("Targets").GetComponent<TargetWidget>();
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
    /// Set the amount of targets hit on the UI.
    /// </summary>
    /// <param name="amount">The amount of targets hit</param>
    public void SetTargetsHit(int amount) {
        _targets.TargetsHit = amount;
    }

    /// <summary>
    /// Display game over UI.
    /// </summary>
    /// <param name="message">The message to display to the user.</param>
    public void EndGame(string message) {
        _gameOver.EndingMessage = message;
        _gameOver.Toggle(true);
    }
}
