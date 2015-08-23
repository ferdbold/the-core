using UnityEngine;
using System.Collections;

public class PlayerCharacter : Character {

    /* COMPONENTS */

    private ParticleSystem _reticle;

    /* CONSTRUCTOR */

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
        Debug.Log(LayerMask.LayerToName(other.gameObject.layer));
        if (LayerMask.NameToLayer("Enemy") == other.gameObject.layer) {
            GameMode.Instance.KillPlayer();
        }
    }
}
