using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class HUD : MonoBehaviour {

    /* COMPONENTS */

    private IntroWidget _intro;
    private ClockWidget _clock;
    private GameOverWidget _gameOver;
    private TargetWidget _targets;
    private Image _flash;

    /* ATTRIBUTES */

    private float _flashAlpha = 0.0f;

    /* CONSTRUCTOR */

	void Awake () {
        FindComponents();
	}

    private void FindComponents() {
        _intro = transform.Find("Intro").GetComponent<IntroWidget>();
        _clock = transform.Find("Clock").GetComponent<ClockWidget>();
        _gameOver = transform.Find("GameOver").GetComponent<GameOverWidget>();
        _targets = transform.Find("Targets").GetComponent<TargetWidget>();
        _flash = GetComponent<Image>();
    }

    /* METHODS */

    void Update() {
        ApplyFlash();
    }

    /// <summary>
    /// Apply the screen flashing effect.
    /// </summary>
    private void ApplyFlash() {
        Color color = _flash.color;
        color.a = _flashAlpha;

        _flash.color = color;
    }

    /// <summary>
    /// Toggle the intro screen.
    /// </summary>
    /// <param name="on">Whether to turn in on or off.</param>
    public void ToggleIntro(bool on) {
        _intro.Toggle(on);
        _clock.Toggle(!on);
        _targets.Toggle(!on);
    }

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

    /// <summary>
    /// Flash the screen.
    /// </summary>
    public void FlashScreen() {
        _flashAlpha = 1.0f;

        DOTween.To(() => _flashAlpha, x => _flashAlpha = x, 0.0f, 0.5f);
    }
}
