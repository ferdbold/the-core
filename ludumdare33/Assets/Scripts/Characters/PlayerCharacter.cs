using UnityEngine;
using System.Collections;

public class PlayerCharacter : Character {

    /* PUBLIC ATTRIBUTES */

    [Header("Audio")]
    public AudioClip jumpSound;
    public AudioClip fireSound;
    public AudioClip enemyCollisionSound;

    /* ATTRIBUTES */

    private static PlayerCharacter _instance;

    /* COMPONENTS */

    private ParticleSystem _reticle;
    private AudioSource _audio;

    /* CONSTRUCTOR */

    public override void Awake() {
        base.Awake();

        _instance = this;
    }

    protected override void FindComponents() {
        base.FindComponents();

        _reticle = _pawn.GetComponent<ParticleSystem>();
        _audio = _pawn.GetComponent<AudioSource>();
    }

    /* METHODS */

    public override void Update() {
 	    base.Update();

        if (!GameMode.Instance.GameIsActive) {
            _reticle.Stop();
        }
    }

    public override void Jump() {
        base.Jump();

        if (!IsJumping) {
            _audio.PlayOneShot(this.jumpSound);
        }
    }

    public override void Fire() {
        base.Fire();

        _audio.PlayOneShot(this.fireSound);
    }

    void OnTriggerEnter(Collider other) {
        if (LayerMask.NameToLayer("Enemy") == other.gameObject.layer) {
            GameMode.Instance.KillPlayer();
            _audio.PlayOneShot(this.enemyCollisionSound);
        }
    }

    /* PROPERTIES */

    public static PlayerCharacter Instance {
        get { return _instance; }
    }

    public float PlayerPosition {
        get { return _pivot.rotation.eulerAngles.y; }
    }
}
