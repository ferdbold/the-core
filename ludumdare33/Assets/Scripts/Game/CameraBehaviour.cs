using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */
    public float maxOffset = 3.0f;
    public float offsetAccel = 0.2f;
    public float offsetDecay = 0.95f;
    public float offsetDeadZone = 0.01f;
    public float offset;
	
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

        this.offset = Mathf.Clamp(this.offset + offsetAmount, -this.maxOffset, this.maxOffset);
    }

    /// <summary>
    /// Soft offset the camera.
    /// </summary>
    public void Offset() {     
        this.offset *= this.offsetDecay;

        this.offset = (Mathf.Abs(this.offset) > this.offsetDeadZone) ? this.offset : 0;

        transform.localRotation = Quaternion.Euler(0, this.offset, 0);
    }
}
