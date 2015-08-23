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

    /* COMPONENTS */

    private EdgeDetection _edgeDetection;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
    }

    private void FindComponents() {
        _edgeDetection = transform.Find("Camera").GetComponent<EdgeDetection>();
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
    /// Toggles on and off the line-only render mode.
    /// </summary>
    /// <param name="on">Whether to turn it on of off</param>
    public void ToggleLineMode(bool on) {
        if (on) {
            DOTween.To(()=> _edgeDetection.edgesOnly, x => _edgeDetection.edgesOnly = x, 1.0f, 1.0f);
        } else {
            DOTween.To(()=> _edgeDetection.edgesOnly, x => _edgeDetection.edgesOnly = x, 0.0f, 1.0f);
        }
    }

    /* PROPERTIES */

    public Transform Target {
        get { return _target; }
        set { _target = value; }
    }
}
