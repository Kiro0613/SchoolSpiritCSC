using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class EnemyMove : MonoBehaviour {
        Enemy me;
        public GameObject targetObject;
        public float walkSpeed = 3f;
        public float angleToTarget;

        public float moveSpeed = 4f;
        public float turnSpeed = 8f;

        Vector3 dirToPlayer;

        public Coroutine chasePlayer;
        public bool chasePlayerRunning = false;

        public Coroutine followPlayer;
        public bool followPlayerRunning = false;

        public Coroutine moveTo;
        public bool moveToRunning = false;
        public Vector3 moveToTarget = Vector3.zero;

        public Coroutine lookAt;
        public bool lookAtRunning = false;
        public Vector3 lookAtTarget = Vector3.zero;

        EnemyStates lastState;


        CharacterController charControl;

        private void Awake() {
            me = GetComponent<Enemy>();
            charControl = GetComponent<CharacterController>();
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            dirToPlayer = Global.Plr.transform.position - transform.position;

            charControl.SimpleMove(Vector3.zero);

            //HandleCoroutines();

            if(Input.GetButtonDown("Test")) {
                //chasePlayer = StartCoroutine(ChasePlayer(moveSpeed, turnSpeed));
            }

            //Make sure this is LAST thing done before frame update
            lastState = me.state;
        }

        public void HandleCoroutines() {
            if(me.state != lastState) {
                switch(me.state) {
                    case EnemyStates.Patrolling:
                        moveTo = StartCoroutine(MoveTo(Global.Plr.transform.position));
                        break;
                    case EnemyStates.Chasing:
                        if(moveToRunning && moveTo != null) { StopCoroutine(moveTo); }
                        chasePlayer = StartCoroutine(ChasePlayer());
                        break;
                    case EnemyStates.Searching:
                        if(moveToRunning && moveTo != null) { StopCoroutine(moveTo); }
                        followPlayer = StartCoroutine(FollowPlayer());
                        break;
                    case EnemyStates.Idle:
                        //lookAt = StartCoroutine(LookAt(transform.forward * -1));
                        break;
                    default:
                        break;
                }
            }
        }

        public IEnumerator LookAt(Vector3 target, float turnSpeed = 8f) {
            if(lookAtRunning) {
                yield break;
            } else {
                lookAtRunning = true;
            }

            Vector2 targetDirection = (target - transform.position).flat();
            Vector2 flatForward = transform.forward.flat();

            while(Vector2.Angle(flatForward, targetDirection) >= 0.1f) {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target - transform.position, transform.up), Time.deltaTime * turnSpeed);
                
                yield return null;
            }

            lookAtRunning = false;
        }

        public IEnumerator MoveTo(Vector3 target, bool forceLook = false, float moveSpeed = 4f, float turnSpeed = 8f) {
            if(moveToRunning) {
                yield break;
            } else {
                moveToRunning = true;
            }

            if(forceLook) {
                if(lookAtRunning) {
                    StopCoroutine(lookAt);
                }

                StartCoroutine(LookAt(target, turnSpeed));
            }

            while(target.x - transform.position.x >= 0.1f || target.z - transform.position.z >= 0.1f) {
                moveToTarget = target;
                charControl.SimpleMove(target - transform.position);
                yield return null;
            }

            moveToRunning = false;
        }

        //FOLLOW PLAYER means follow with EYESIGHT. CHASE PLAYER actually moves to the player.
        public IEnumerator FollowPlayer(float turnSpeed = 8f) {
            if(followPlayerRunning) {
                yield break;
            } else {
                followPlayerRunning = true;
            }
            
            while(me.seesPlayer) {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirToPlayer, transform.up), Time.deltaTime * turnSpeed);

                yield return null;
            }

            followPlayerRunning = false;
        }

        public IEnumerator ChasePlayer(float moveSpeed = 4f, float turnSpeed = 8f) {
            if(chasePlayerRunning) {
                yield break;
            } else {
                chasePlayerRunning = true;
            }

            while(me.seesPlayer) {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Global.Plr.transform.position - transform.position, transform.up), Time.deltaTime * turnSpeed);

                charControl.SimpleMove(Global.Plr.transform.position - transform.position);
                yield return null;
            }

            chasePlayerRunning = false;
        }
    }
}