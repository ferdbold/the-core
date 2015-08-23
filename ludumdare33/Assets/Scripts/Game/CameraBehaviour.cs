using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using DG.Tweening;

public class CameraBehaviour : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */
    
    [Header("Horizontal Offset")]
    public float maxOffset = 3.0f;
    public float offsetAccel = 0.2f;
    public float offsetDecay = 0.95f;
    public float offsetDeadZone = 0.01f;
    public Vector3 offset;

    [Header("Vertical Offset")]
    public float verticalCameraOffset = 0.5f;

    /* ATTRIBUTES */

    private Transform _target;
    private Vector3 _position;

    /* COMPONENTS */

    private EdgeDetection _edgeDetection;
    private Camera _camera;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
    }

    void Start() {
        _position = _camera.transform.localPosition;
    }

    private void FindComponents() {
        _edgeDetection = transform.Find("Camera").GetComponent<EdgeDetection>();
        _camera = transform.Find("Camera").GetComponent<Camera>();
    }

    /* METHODS */

	void Update() {
        Offset();
	}

    /// <summary>
    /// Apply an offset force to the camera.
    /// </summary>
    /// <param name="amount">The amount of offset to apply, from -1 to 1.</param>
    public void ApplyOffsetFactor(float amount) {
        float offsetAmount = amount * this.offsetAccel * Time.deltaTime;

        this.offset.x = Mathf.Clamp(this.offset.x + offsetAmount, -this.maxOffset, this.maxOffset);
    }

    /// <summary>
    /// Soft offset the camera.
    /// </summary>
    public void Offset() {
        
        this.offset.x *= this.offsetDecay;
        this.offset.x = (Mathf.Abs(this.offset.x) > this.offsetDeadZone) ? this.offset.x : 0;

        transform.DOLocalMoveY(_target.localPosition.y, this.verticalCameraOffset);
        transform.localRotation = Quaternion.Euler(0, this.offset.x, 0);
    }

    /// <summary>
    /// Shake the camera.
    /// </summary>
    public void Shake() {
        _camera.DOShakePosition(0.4f, 0.2f).OnComplete(OnShakeComplete);
    }
    
    public void OnShakeComplete() {
        _camera.transform.localPosition = _position;
    }

    /// <summary>
    /// Toggles on and off the line-only render mode.
    /// </summary>
    /// <param name="on">Whether to turn it on of off</param>
    /// <param name="immediate">Whether to immediately apply the effect or not</param>
    public void ToggleLineMode(bool on, bool immediate = false) {
        if (!immediate) {
            if (on) {
                DOTween.To(()=> _edgeDetection.edgesOnly, x => _edgeDetection.edgesOnly = x, 1.0f, 1.0f);
            } else {
                DOTween.To(()=> _edgeDetection.edgesOnly, x => _edgeDetection.edgesOnly = x, 0.0f, 1.0f);
            }
        } else {
            _edgeDetection.edgesOnly = on ? 1.0f : 0.0f;
        }
    }

    /* PROPERTIES */

    public Transform Target {
        get { return _target; }
        set { _target = value; }
    }
}
