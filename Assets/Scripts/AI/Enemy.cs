using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public enum EnemyStates {
        Patrolling,
        Chasing,
        Searching,
        Idle
    }

    public class Enemy : MonoBehaviour {
        public EnemyStates state;

        //State during previous frame
        public EnemyStates lastState;

        public bool seesPlayer = false;

        private void Awake() {
            state = EnemyStates.Idle;
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            switch(state) {
                case EnemyStates.Patrolling:
                    break;
                case EnemyStates.Chasing:
                    break;
                case EnemyStates.Searching:
                    break;
                case EnemyStates.Idle:
                    break;
                default:
                    break;
            }
        }
    }
}
