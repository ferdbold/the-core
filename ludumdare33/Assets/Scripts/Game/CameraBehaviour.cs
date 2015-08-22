using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */
    public float offsetWeight = 0.2f;

    /* CONSTRUCTOR */
	void Start () {

    }
	
    /* METHODS */
	void Update () {
	
	}

    /// <summary>
    /// Set the offset of the camera.
    /// </summary>
    /// <param name="amount">The amount of offset to apply, from -1 to 1.</param>
    public void SetOffset(float amount) {
        float offsetAmount = offsetWeight * -amount;
        Vector3 origPosition = transform.localPosition;

        origPosition.x = offsetAmount;
        transform.localPosition = origPosition;
    }
}
