﻿using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */
    [Header("Audio")]
    public AudioClip killSound;

    /* ATTRIBUTES */

    private float _lifeSpan;
    private float _timeElapsed;
    private bool _hit = false;

    /* COMPONENTS */

    private Animation _animation;
    private ParticleSystem _centeringParticles;
    private ParticleSystem _shards;
    private AudioSource _audio;

    /* CONSTRUCTOR */

    void Awake() {
        FindComponents();
    }

    private void FindComponents() {
        _animation = GetComponent<Animation>();
        _centeringParticles = transform.Find("Target").GetComponent<ParticleSystem>();
        _shards = transform.Find("Target").Find("Shards").GetComponent<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
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
        if (!_hit) {
            _hit = true;
            GameMode.Instance.OnTargetHit();
            StartCoroutine(Die());
        }
    }

    private IEnumerator Disappear() {
        _centeringParticles.Stop();
        
        yield return new WaitForSeconds(2.0f);
        
        GameObject.Destroy(gameObject, 0.35f);
        _animation.Play("TargetDisappear");
    }

    private IEnumerator Die() {
        _animation.Play("TargetKill");
        
        _centeringParticles.Stop();
        _shards.Play();

        _audio.PlayOneShot(this.killSound);
        
        yield return new WaitForSeconds(2.0f);

        GameObject.Destroy(gameObject);
    }

    /* COMPONENTS */

    public float LifeSpan {
        get { return _lifeSpan; }
        set { _lifeSpan = value; }
    }
}
