﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class GameOverWidget : MonoBehaviour {

    /* COMPONENTS */

    private Text _titleText;
    private Text _retryText;

    /* ATTRIBUTES */

    private float _textAlpha = 0.0f;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
    }

    private void FindComponents() {
        _titleText = transform.Find("TitleText").GetComponent<Text>();
        _retryText = transform.Find("RetryText").GetComponent<Text>();
    }

    /* METHODS */

    void Update() {
        Color titleColor = _titleText.color;
        Color retryColor = _retryText.color;

        titleColor.a = _textAlpha;
        retryColor.a = _textAlpha;

        _titleText.color = titleColor;
        _retryText.color = retryColor;
    }

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