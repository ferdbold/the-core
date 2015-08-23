using UnityEngine;
using System.Collections;
using System.Timers;

public class GameMode : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */

    [Header("Rules")]
    public float timeLeft = 30.0f;
    public int targetGoal = 6;

    [Header("Target Spawn")]
    public GameObject targetPrefab;
    public float targetSpawnInterval = 2.0f;
    public float targetLifeSpan = 5.0f;
    public float[] heights;

    [Header("Messages")]
    public string deathEnding;
    public string timeEnding;
    public string badEnding;
    public string goodEnding;

    /* COMPONENTS */

    private HUD _hud;
    private CameraBehaviour _camera;
    private Core _core;

    /* ATTRIBUTES */

    private static GameMode _instance;
    private float _timeUntilSpawn = 0;
    private bool _gameActive = false;
    private int _targetsHit = 0;

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
        _core = GameObject.FindWithTag("Core").GetComponent<Core>();
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
            if (_targetsHit == 0) {
                Debug.Log("Good ending");
                EndGame(this.goodEnding);
            } else {
                Debug.Log("Time ending");
                EndGame(this.timeEnding);
            }
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
    /// Register target hit.
    /// </summary>
    public void OnTargetHit() {
        _targetsHit++;
        _hud.SetTargetsHit(_targetsHit);
        _camera.Shake();

        if (_targetsHit >= this.targetGoal) {
            WinGame();
        }
    }

    /// <summary>
    /// Trigger bad ending after clearing target goal.
    /// </summary>
    private void WinGame() {
        Debug.Log("Bad ending");
        _core.Kill();
        _hud.FlashScreen();
        EndGame(this.badEnding);
    }

    /// <summary>
    /// End the game.
    /// </summary>
    private void EndGame(string message) {
        _gameActive = false;
        StartCoroutine(ShowGameOverScreen(message, message == this.badEnding));
    }

    private IEnumerator ShowGameOverScreen(string message, bool wait) {
        if (wait) {
            yield return new WaitForSeconds(0.8f);
        }

        _camera.ToggleLineMode(true);
        _hud.EndGame(message);
    }

    /// <summary>
    /// Kill the player and end the game.
    /// </summary>
    public void KillPlayer() {
        Debug.Log("Death ending");
        EndGame(this.deathEnding);
    }

    /// <summary>
    /// Restart the game.
    /// </summary>
    public void Restart() {
        Time.timeScale = 1.0f;
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
