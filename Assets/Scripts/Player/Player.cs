using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public enum PlayerStates {
        Idle,
        Walking,
        Running,
        Jumping,
        Aerial
    }

    public class Player : MonoBehaviour {
        public PlrMove Move;
        public Camera Cam;
        public PlrUse Use;

        public PlayerStates state;

        public ILogger logger = Debug.unityLogger;
        
        // Start is called before the first frame update
        void Awake() {
            Cam = GetComponentInChildren<Camera>();
            Move = GetComponent<PlrMove>();
            Use = GetComponent<PlrUse>();
        }

        // Update is called once per frame
        void Update() {

        }

        public void log(object message) {
            logger.Log(message);
        }
    }
}