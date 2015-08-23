using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class IntroWidget : MonoBehaviour {

    /* COMPONENTS */

    private Text _titleText;
    private Text _authorText;
    private Text _rulesText;

    /* ATTRIBUTES */

    private float _textAlpha = 0.0f;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
    }

    private void FindComponents() {
        _titleText = transform.Find("Title").GetComponent<Text>();
        _authorText = transform.Find("Author").GetComponent<Text>();
        _rulesText = transform.Find("Rules").GetComponent<Text>();
    }

    /* METHODS */

    void Update() {
        Color color = _titleText.color;

        color.a = _textAlpha;

        _titleText.color = color;
        _authorText.color = color;
        _rulesText.color = color;
    }

    /* METHODS */

    /// <summary>
    /// Toggles the display of this widget.
    /// </summary>
    /// <param name="on">Whether to turn on the widget or not</param>
    public void Toggle(bool on) {
        if (on) {
            DOTween.To(() => _textAlpha, x => _textAlpha = x, 1.0f, 1.5f);
        } else {
            DOTween.To(() => _textAlpha, x => _textAlpha = x, 0.0f, 1.5f);
        }
    }
}
