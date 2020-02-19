using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public enum RouteType {
        Loop,
        Line
    }

    public class EnemyPatrol : MonoBehaviour {
        Enemy me;
        public List<Node> patrolRoute;
        public RouteType routeType = RouteType.Loop;
        public Node targetNode;
        public int routeIndex = 0;

        public float remainingDist = 0f;

        private void Awake() {
            me = GetComponent<Enemy>();
        }

        // Start is called before the first frame update
        void Start() {
            targetNode = patrolRoute[routeIndex];
            me.target = targetNode.transform;
        }

        // Update is called once per frame
        void Update() {

        }

        public void Patrol() {
            remainingDist = me.agent.remainingDistance;

            if(me.agent.remainingDistance <= 1) {
                routeIndex = routeIndex < patrolRoute.Count - 1 ? routeIndex + 1 : 0;
                targetNode = patrolRoute[routeIndex];
            }

            me.target = targetNode.transform;

            me.agent.destination = me.target.position;
        }
    }
}