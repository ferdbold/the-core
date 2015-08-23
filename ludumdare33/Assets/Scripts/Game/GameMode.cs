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
    private CameraBehaviour _camera;

    /* ATTRIBUTES */

    private static GameMode _instance;
    private float _timeUntilSpawn = 0;
    private bool _gameActive = false;

    /* CONSTRUCTOR */

	void Awake() {
        FindComponents();
        _instance = this;
    }

    void Start() {
        _gameActive = true;
    }

    private void FindComponents() {
        _hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraBehaviour>();
    }
	
    /* METHODS */

	void Update() {
        if (GameIsActive) {
            TickClock();
            TickSpawn();
        }
    }

    /// <summary>
    /// Tick the game clock.
    /// </summary>
    private void TickClock() {
        this.timeLeft -= Time.deltaTime;
        _hud.SetClock(this.timeLeft);

        if (this.timeLeft < 0) {
            Debug.Log("Time is up");
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
        _gameActive = false;
        _camera.ToggleLineMode(true);
        _hud.OnGameEnd();
        Debug.Log("Game over");
    }

    /// <summary>
    /// Kill the player and end the game.
    /// </summary>
    public void KillPlayer() {
        Debug.Log("Player died");
        EndGame();
    }

    /// <summary>
    /// Restart the game.
    /// </summary>
    public void Restart() {
        Application.LoadLevel("Game");
    }

    /* PROPERTIES */

    public static GameMode Instance {
        get { return _instance; }
    }

    public bool GameIsActive {
        get { return _gameActive; }
    }
}
