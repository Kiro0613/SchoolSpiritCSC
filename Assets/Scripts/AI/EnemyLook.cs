using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class EnemyLook : MonoBehaviour {
        Enemy me;

        Player.Player player;
        public GameObject target;
        public bool seesPlayer = false;

        public bool showVis = false;
        public bool logAngle = false;

        public float visRange = 16f;
        public float visAngle = 30f;
        public Vector3 vectorToPlr = Vector3.zero;

        public float angleToPlr;
        public float distToPlr = 0f;

        public bool inVisCone = false;

        private void Awake() {
            me = GetComponent<Enemy>();
        }

        // Start is called before the first frame update
        void Start() {
            player = GameObject.Find("player").GetComponent<Player.Player>();
        }

        // Update is called once per frame
        void Update() {
            vectorToPlr = player.transform.position - transform.position;
            angleToPlr = Vector3.Angle(transform.forward, vectorToPlr);
            inVisCone = angleToPlr <= visAngle / 2;

            Debug.DrawRay(transform.position, player.transform.position, Color.red, 0.1f);

            if(angleToPlr <= visAngle / 2) {
                if(Physics.Raycast(transform.position, vectorToPlr, out RaycastHit hit, visRange)) {
                    if(hit.transform.CompareTag("Player")) {
                        distToPlr = hit.distance;
                        seesPlayer = true;
                    } else {
                        seesPlayer = false;
                        distToPlr = -1f;
                    }
                } else {
                    seesPlayer = false;
                    distToPlr = -1f;
                }
            } else {
                seesPlayer = false;
                distToPlr = -1f;
            }

            if(Mathf.Sign(distToPlr) == -1) {
                me.state = EnemyStates.Idle;
            } else if(distToPlr <= visRange / 2) {
                me.state = EnemyStates.Chasing;
            } else if(distToPlr <= visRange) {
                me.state = EnemyStates.Searching;
            } else {
                me.state = EnemyStates.Patrolling;
            }
        }

        private void OnDrawGizmos() {
            if(showVis) {
                player = GameObject.Find("player").GetComponent<Player.Player>();
                vectorToPlr = player.transform.position - transform.position;

                Vector3 fwdBound = transform.forward * visRange;
                Vector3 leftBound = Quaternion.Euler(0f, visAngle / -2, 0f) * fwdBound;
                Vector3 rightBound = Quaternion.Euler(0f, visAngle / 2, 0f) * fwdBound;

                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + fwdBound);

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + leftBound);

                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, transform.position + rightBound);

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, transform.position + vectorToPlr);

                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(transform.position, player.transform.position);
            }
        }

        private void OnValidate() {
            if(logAngle) {
                Debug.Log(Vector3.Angle(transform.forward, vectorToPlr));
            }

            logAngle = false;
        }
    }
}