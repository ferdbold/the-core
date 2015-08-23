using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ClockWidget : MonoBehaviour {

    /* COMPONENTS */

    private Text _timeLeftText;
    private RectTransform _dotPivot;
    private Image _background;

    /* ATTRIBUTES */

    private int _lastTimeRecorded;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
    }

    private void FindComponents() {
        _timeLeftText = transform.Find("TimeLeft").GetComponent<Text>();
        _dotPivot = transform.Find("DotPivot").GetComponent<RectTransform>();
        _background = transform.Find("Background").GetComponent<Image>();
    }

    /* METHODS */

    void Update() {
        RefreshBackgroundColor();
    }

    private void RefreshBackgroundColor() {
        Color color = _background.color;
        float playerPos = PlayerCharacter.Instance.PlayerPosition;
        float delta = 180.0f - Mathf.Abs(playerPos - 180);
        float alpha = delta / 180.0f * 0.25f;
        
        Debug.Log(alpha);

        color.a = alpha;
        _background.color = color; 
    }

    private void PlayTickingEmphasis() {
        _timeLeftText.rectTransform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        _timeLeftText.rectTransform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.4f);
    }

    /* PROPERTIES */

    public float TimeLeft {
        set {
            if (_lastTimeRecorded != (int)value) {
                PlayTickingEmphasis();
            }

            float decimalPart = value - Mathf.Floor(value);

            _timeLeftText.text = Mathf.Ceil(value).ToString();
            _dotPivot.rotation = Quaternion.Euler(0, 0, decimalPart * 360f);

            _lastTimeRecorded = (int)value;
        }
    }
}
