using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    /* COMPONENTS */

    private ClockWidget _clock;

    /* CONSTRUCTOR */

	void Awake () {
        FindComponents();
	}

    private void FindComponents() {
        _clock = transform.Find("Clock").GetComponent<ClockWidget>();
    }

    /* METHODS */

    /// <summary>
    /// Set the game clock to a certain number.
    /// </summary>
    /// <param name="time">The time to set the clock to</param>
    public void SetClock(float time) {
        _clock.TimeLeft = time;
    }
}
