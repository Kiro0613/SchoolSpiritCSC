using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class EnemyLook : MonoBehaviour {
        Enemy me;

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

        }

        // Update is called once per frame
        void Update() {
            //Gets direction vector from enemy to player
            vectorToPlr = Global.Plr.transform.position - transform.position;
            angleToPlr = Vector3.Angle(transform.forward, vectorToPlr);
            
            LookForPlayer();
        }

        public void LookForPlayer() {
            if(
                angleToPlr <= visAngle / 2 &&   //Plr is in vis cone
                Physics.Raycast(transform.position, vectorToPlr, out RaycastHit hit, visRange) &&   //Enemy sees something in vis range
                hit.transform.CompareTag("Player")  //Thing that enemy sees is the player
            ) {
                distToPlr = hit.distance;
                me.seesPlayer = true;
            } else {
                distToPlr = -1f;
                me.seesPlayer = false;
            }
        }

        private void OnDrawGizmosSelected() {
            if(showVis) {
                vectorToPlr = Global.Plr.transform.position - transform.position;

                Vector3 fwdBound = transform.forward * visRange;
                Vector3 leftBound = Quaternion.Euler(0f, visAngle / -2, 0f) * fwdBound;
                Vector3 rightBound = Quaternion.Euler(0f, visAngle / 2, 0f) * fwdBound;

                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + fwdBound);

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + leftBound);

                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, transform.position + rightBound);

                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(transform.position, Global.Plr.transform.position);
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