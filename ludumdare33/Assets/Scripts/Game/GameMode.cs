using UnityEngine;
using System.Collections;
using System.Timers;

public class GameMode : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */

    public float timeLeft = 30.0f;

    [Header("Target Spawn")]
    public GameObject targetPrefab;
    public float targetSpawnInterval = 2.0f;
    public float targetLifeSpan = 5.0f;
    public float[] heights;

    /* COMPONENTS */

    private HUD _hud;

    /* ATTRIBUTES */

    private float _timeUntilSpawn = 0;

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
        TickSpawn();
	}

    /// <summary>
    /// Tick the game clock.
    /// </summary>
    private void TickClock() {
        this.timeLeft -= Time.deltaTime;
        _hud.SetClock(this.timeLeft);

        if (this.timeLeft < 0) {
            EndGame();
        }
    }

    /// <summary>
    /// Tick the target spawning clock.
    /// </summary>
    private void TickSpawn() {
        _timeUntilSpawn += Time.deltaTime;

        if (_timeUntilSpawn > this.targetSpawnInterval) {
            _timeUntilSpawn = 0;
            SpawnTarget();
        }
    }

    /// <summary>
    /// Spawn a target around the core.
    /// </summary>
    private void SpawnTarget() {
        // Randomize target position
        float targetHeight = this.heights[Random.Range(0, this.heights.Length)];
        Vector3 targetPos = new Vector3(0, targetHeight, 0);
        Quaternion targetRot = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);

        // Create new target
        GameObject newTargetObj = (GameObject)GameObject.Instantiate(this.targetPrefab, targetPos, targetRot);
        Target newTarget = newTargetObj.GetComponent<Target>();
        
        // Initialize new target
        newTarget.LifeSpan = this.targetLifeSpan;
    }

    /// <summary>
    /// End the game.
    /// </summary>
    private void EndGame() {

    }
}
