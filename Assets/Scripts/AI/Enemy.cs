using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy {
    public enum EnemyStates {
        Patrolling,
        Chasing,
        Searching,
        Idle
    }

    public class Enemy : MonoBehaviour {
        public EnemyMove Move;
        public EnemyLook Look;
        public EnemyPatrol Patrol;
        public EnemyChase Chase;

        public NavMeshAgent agent;

        public EnemyStates state;

        //State during previous frame
        public EnemyStates lastState;

        public bool seesPlayer = false;

        public Transform target;

        private void Awake() {
            Move = GetComponent<EnemyMove>();
            Look = GetComponent<EnemyLook>();
            Patrol = GetComponent<EnemyPatrol>();
            Chase = GetComponent<EnemyChase>();

            agent = GetComponent<NavMeshAgent>();

            state = EnemyStates.Idle;
            lastState = EnemyStates.Idle;
        }

        public KeyCode startLook = KeyCode.O;
        public KeyCode startLookPlr = KeyCode.P;

        AIRoutine[] routineQueue;
        LookAt lookAt;
        LookAtPlayer lookAtPlayer;

        // Start is called before the first frame update
        void Start() {
            lookAt = gameObject.AddComponent<LookAt>();
            lookAtPlayer = gameObject.AddComponent<LookAtPlayer>();
            target = transform;
        }

        // Update is called once per frame
        void Update() {
            if(Input.GetButtonDown("BigRedButton")) {
                Debug.Log("PUSHED THE BIG RED BUTTON!!!");
                StopAllCoroutines();
            }

            if(Input.GetKeyDown(startLook)) {
                lookAt.Run(Global.Plr.transform.position);
            }

            if(Input.GetKeyDown(startLookPlr)) {
                lookAtPlayer.Run();
            }


            HandleState();

            switch(state) {
                case EnemyStates.Patrolling:
                    Patrol.Patrol();
                    break;
                case EnemyStates.Chasing:
                    Chase.Chase();
                    break;
                case EnemyStates.Searching:
                    break;
                case EnemyStates.Idle:
                    break;
                default:
                    break;
            }

            lastState = state;
        }

        void HandleState() {
            //if(Mathf.Sign(Look.distToPlr) == -1) {
            //    state = EnemyStates.Idle;
            //} else if(Look.distToPlr <= Look.visRange / 2) {
            //    state = EnemyStates.Chasing;
            //} else if(Look.distToPlr <= Look.visRange) {
            //    state = EnemyStates.Searching;
            //} else {
            //    state = EnemyStates.Patrolling;
            //}

            if(seesPlayer) {
                state = EnemyStates.Chasing;
                Look.visAngle = 45f;
                Look.visRange = 30f;
            } else {
                state = EnemyStates.Patrolling;
                Look.visAngle = 30f;
                Look.visRange = 16f;
            }
        }

        public void SwapAgent(NavMeshAgent newAgent) {
            //System.Type type = agent.GetType();
            //System.Reflection.FieldInfo[] fields = type.GetFields();

            //foreach(System.Reflection.FieldInfo field in fields) {
            //    field.SetValue(agent, field.GetValue(newAgent));
            //}

            Debug.Log("Swapping Agent to " + newAgent.name);

            agent.speed = newAgent.speed;
            agent.angularSpeed = newAgent.angularSpeed;
            agent.acceleration = newAgent.acceleration;
            agent.stoppingDistance = newAgent.stoppingDistance;
            agent.autoBraking = newAgent.autoBraking;
        }
    }
}
