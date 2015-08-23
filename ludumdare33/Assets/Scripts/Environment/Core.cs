using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Core : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */

    [Header("Audio")]
    public AudioClip killSound;

    /* COMPONENTS */

    private Animation _animation;
    private ParticleSystem _shards;
    private AudioSource _audio;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
    }

    private void FindComponents() {
        _animation = GetComponent<Animation>();
        _shards = GetComponent<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
    }

    /* METHODS */

    void OnTriggerEnter(Collider other) {
        if (LayerMask.NameToLayer("Projectile") == other.gameObject.layer) {
            GameObject.Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// Trigger core kill animation.
    /// </summary>
    public void Kill() {
        _animation.Play();
        _shards.Play();

        _audio.PlayOneShot(this.killSound);

        // Slowo effect
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0.1f, 1.5f);
    }
}
