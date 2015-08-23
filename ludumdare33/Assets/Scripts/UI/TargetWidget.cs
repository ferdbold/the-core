using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class TargetWidget : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */

    public Sprite hitSprite;
    public Sprite unhitSprite;

    /* COMPONENTS */

    private Image[] _images;
    private RectTransform _tweeningImage;

    /* ATTRIBUTES */

    private int _lastTargetsHit = 0;
    private Vector3 _tweeningScale;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
    }

    private void FindComponents() {
        _images = GetComponentsInChildren<Image>();
    }

    /* METHODS */

    void Update() {
        _tweeningImage.localScale = _tweeningScale;
    }

    /* PROPERTIES */

    public int TargetsHit {
        set {
            for (int i = 0; i < _images.Length; i++) {
                if (i < value) {
                    _images[i].sprite = this.hitSprite;
                } else {
                    _images[i].sprite = this.unhitSprite;
                }

                if (i == _lastTargetsHit) {
                    _tweeningImage = _images[i].rectTransform;

                    _tweeningScale = new Vector3(2.5f, 2.5f, 2.5f);
                    _tweeningImage.localScale = _tweeningScale;

                    DOTween.To(
                        () => _tweeningScale,
                        x => _tweeningScale = x,
                        new Vector3(1.0f, 1.0f, 1.0f),
                        0.5f
                    );
                }
            }

            _lastTargetsHit = value;
        }
    }
}
