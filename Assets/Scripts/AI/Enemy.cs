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

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}
