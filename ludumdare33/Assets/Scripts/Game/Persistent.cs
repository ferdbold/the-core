using UnityEngine;
using System.Collections;

public class Persistent : MonoBehaviour {

    /* PUBLIC ATTRIBUTES */

    public bool showIntro = true;

    /* ATTRIBUTES */

    private static Persistent _instance;
    
    /* CONSTRUCTOR */

    void Awake() {
        if (_instance == null) {
            _instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        } else {
            GameObject.Destroy(gameObject);
        }
    }

    /* PROPERTIES */

    public static Persistent Instance {
        get { return _instance; }
    }
}
