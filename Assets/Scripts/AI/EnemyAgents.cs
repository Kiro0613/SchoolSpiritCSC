using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy {
    public class EnemyAgents : MonoBehaviour {

        /*  These are all dummy objects so Enemies can quickly switch
         *  the values in their NavMeshAgent (changing things like speed)
         *  based on their state. These don't do anything on their own.
         *  They're attached to the Global Globe in the scene, but they can
         *  be pretty much anywhere.
         */

        public static NavMeshAgent patrolAgent;
        public static NavMeshAgent chaseAgent;
        public static NavMeshAgent searchAgent;
        public static NavMeshAgent idleAgent;

        // Start is called before the first frame update
        void Start() {
            patrolAgent = GameObject.Find("PatrolAgent").GetComponent<NavMeshAgent>();
            chaseAgent = GameObject.Find("ChaseAgent").GetComponent<NavMeshAgent>();
            searchAgent = GameObject.Find("SearchAgent").GetComponent<NavMeshAgent>();
            idleAgent = GameObject.Find("IdleAgent").GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update() {

        }
    }
}