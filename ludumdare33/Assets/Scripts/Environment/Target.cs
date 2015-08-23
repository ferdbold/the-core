using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    /* ATTRIBUTES */

    private float _lifeSpan;
    private float _timeElapsed;

    /* COMPONENTS */

    private Animation _animation;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
    }

    private void FindComponents() {
        _animation = GetComponent<Animation>();
    }

    /* METHODS */

    void Update() {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed > _lifeSpan) {
            GameObject.Destroy(gameObject, 0.35f);
            _animation.Play();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (LayerMask.NameToLayer("Projectile") == other.gameObject.layer) {
            Debug.Log("Hit");
        }
    }

    /* COMPONENTS */

    public float LifeSpan {
        get { return _lifeSpan; }
        set { _lifeSpan = value; }
    }
}
