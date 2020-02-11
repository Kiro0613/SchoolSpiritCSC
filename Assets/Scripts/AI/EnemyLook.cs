using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class EnemyLook : MonoBehaviour {
        public bool showVis = false;

        public float visRange = 16f;
        public float visAngle = 30f;
        public float lookAngle = 0f;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        private void OnDrawGizmosSelected() {
            if(showVis) {
                Vector3 fwdBound = transform.forward * visRange;
                Vector3 leftBound = Quaternion.Euler(0f, visAngle / -2, 0f) * fwdBound;
                Vector3 rightBound = Quaternion.Euler(0f, visAngle / 2, 0f) * fwdBound;

                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + fwdBound);

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + leftBound);

                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, transform.position + rightBound);
            }
        }
    }
}