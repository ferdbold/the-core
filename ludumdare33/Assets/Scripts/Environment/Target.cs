using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    /* ATTRIBUTES */

    private float _lifeSpan;
    private float _timeElapsed;

    /* COMPONENTS */

    private Animation _animation;
    private ParticleSystem _particles;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
    }

    private void FindComponents() {
        _animation = GetComponent<Animation>();
        _particles = transform.Find("Target").GetComponent<ParticleSystem>();
    }

    /* METHODS */

    void Update() {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed > _lifeSpan) {
            StartCoroutine(Disappear());
        }

        if (!GameMode.Instance.GameIsActive) {
            StartCoroutine(Disappear());
        }
    }

    public void OnHit() {
        Debug.Log("Hit");
    }

    private IEnumerator Disappear() {
        _particles.Stop();
        yield return new WaitForSeconds(2.0f);
        GameObject.Destroy(gameObject, 0.35f);
        _animation.Play();
    }

    /* COMPONENTS */

    public float LifeSpan {
        get { return _lifeSpan; }
        set { _lifeSpan = value; }
    }
}
