using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClockWidget : MonoBehaviour {

    /* COMPONENTS */

    private Text _timeLeftText;
    private RectTransform _dotPivot;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
    }

    private void FindComponents() {
        _timeLeftText = GetComponent<Text>();
        _dotPivot = transform.Find("DotPivot").GetComponent<RectTransform>();
    }

    /* PROPERTIES */

    public float TimeLeft {
        set {
            float decimalPart = value - Mathf.Floor(value);

            _timeLeftText.text = Mathf.Ceil(value).ToString();
            _dotPivot.rotation = Quaternion.Euler(0, 0, decimalPart * 360f);
        }
    }
}
