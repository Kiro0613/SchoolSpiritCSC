using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class EnemyMove : MonoBehaviour {
        public float walkSpeed = 3f;

        CharacterController charControl;

        private void Awake() {
            charControl = GetComponent<CharacterController>();
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            charControl.SimpleMove(Vector3.zero);
        }
    }
}