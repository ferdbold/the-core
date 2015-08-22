using UnityEngine;
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

    /* METHODS */

	void Update () {
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

    /* PROPERTIES */

    public Transform Target {
        get { return _target; }
        set {
            _target = value;
        }
    }
}
