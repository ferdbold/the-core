using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetWidget : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */

    public Sprite hitSprite;
    public Sprite unhitSprite;

    /* COMPONENTS */

    private Image[] _images;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
    }

    private void FindComponents() {
        _images = GetComponentsInChildren<Image>();
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
            }
        }
    }
}
