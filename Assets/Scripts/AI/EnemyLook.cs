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
            if(showVis) {
            }
        }

        private void OnDrawGizmosSelected() {
            Vector3 leftBound = Quaternion.Euler(0, (visAngle/2) * -1, 0) * (transform.position + transform.forward * visRange);
            Vector3 rightBound = Quaternion.Euler(0, visAngle / 2, 0) * (transform.position + transform.forward * visRange);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10f);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, leftBound);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, rightBound);
        }
    }

}