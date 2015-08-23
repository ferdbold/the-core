using UnityEngine;
using System.Collections;

public class PlayerCharacter : Character {
    
    /* ATTRIBUTES */

    private static PlayerCharacter _instance;

    /* COMPONENTS */

    private ParticleSystem _reticle;

    /* CONSTRUCTOR */

    public override void Awake() {
        base.Awake();

        _instance = this;
    }

    protected override void FindComponents() {
        base.FindComponents();

        _reticle = _pawn.GetComponent<ParticleSystem>();
    }

    /* METHODS */

    public override void Update() {
 	    base.Update();

        if (!GameMode.Instance.GameIsActive) {
            _reticle.Stop();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (LayerMask.NameToLayer("Enemy") == other.gameObject.layer) {
            GameMode.Instance.KillPlayer();
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
