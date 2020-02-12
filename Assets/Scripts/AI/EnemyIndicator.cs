using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class EnemyIndicator : MonoBehaviour {
        Enemy me;
        public MeshRenderer mesh;

        private void Awake() {
            mesh = GetComponent<MeshRenderer>();
        }

        // Start is called before the first frame update
        void Start() {
            me = GetComponent<Enemy>();
        }

        // Update is called once per frame
        void Update() {
            switch(me.state) {
                case EnemyStates.Patrolling:
                    mesh.material.color = Color.green;
                    break;
                case EnemyStates.Chasing:
                    mesh.material.color = Color.red;
                    break;
                case EnemyStates.Searching:
                    mesh.material.color = Color.yellow;
                    break;
                default:
                    mesh.material.color = Color.white;
                    break;
            }
        }
    }
}
