using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates {
    Patrolling,
    Suspicious,
    Searching,
    Chasing
}

public class EnemyOld : MonoBehaviour {
    public GameObject player;

    public EnemyStates state;
    private EnemyStates lastState;  //State enemy had on previous call
    AIConeDetection visCone;
    public GameObject indicator;
    MeshRenderer indicatorColor;   //Changes color to tell how enemy feels
    Vector3 targetPos;
    Quaternion targetRot;
    Vector3 originPos;
    Quaternion originRot;

    public float stateTimeout;
    
    public float moveSpeed;
    public float lookSpeed;

    [Header("Hearing Distance")]
    [Range(0f, 10f)]
    public float alertThreshold;
    [Range(0f, 10f)]
    public float searchThreshold;
    
    // Start is called before the first frame update
    void Start()
    {
        visCone = GetComponent<AIConeDetection>();
        indicatorColor = indicator.GetComponent<MeshRenderer>();
        state = EnemyStates.Patrolling;
        player = GameObject.FindGameObjectWithTag("Player");
        originPos = transform.position;
        originRot = transform.rotation;
    }

    // Update is called once per frame
    void Update() {
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, Time.deltaTime * lookSpeed);

        if(state == EnemyStates.Chasing) {
            Chase();
        } else if(state == EnemyStates.Suspicious) {
            LookAround();
        } else if(state == EnemyStates.Searching) {
            Search();
        } else {
            Patrol();
        }

        if(canSeePlayer()) {
            state = EnemyStates.Chasing;
            stateTimeout = 5;
            targetPlayer();
            visCone.m_fConeLength = 20;
        } else {
            visCone.m_fConeLength = 12;
        }

        //Debug.Log(visCone.GameObjectIntoCone.Contains(player));
    }

    private void FixedUpdate() {
        if(stateTimeout > 0) {
            stateTimeout -= Time.deltaTime;
        }
    }

    //Main state functions
    private void Patrol() {
        indicatorColor.material.color = Color.green;
    }

    float suspicionTimeout;
    private void LookAround() {
        indicatorColor.material.color = Color.blue;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * lookSpeed);

        if(timedOut()) {
            state = EnemyStates.Patrolling;
            targetHome();
        }
    }

    float searchTimeout;
    private void Search() {
        indicatorColor.material.color = Color.yellow;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * lookSpeed);

        if(timedOut()) {
            state = EnemyStates.Patrolling;
            targetHome();
        }
    }

    float movementTimeout;
    private void Chase() {
        indicatorColor.material.color = Color.red;
        
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * lookSpeed);

        if(timedOut()) {
            state = EnemyStates.Searching;
            stateTimeout = 8f;
        }
    }
    
    //State management functions
    public void hearSound(GameObject soundSource) {
        PlaySound soundInfo = soundSource.GetComponent<PlaySound>();

        if(soundInfo.volume > alertThreshold) {
            state = EnemyStates.Chasing;
            stateTimeout = 5f;
            targetPlayer();
            //Debug.Log("Heard you!!!");
        } else if(soundInfo.volume > searchThreshold) {
            state = EnemyStates.Suspicious;
            stateTimeout = 8f;
            targetRot = TargetRotation(soundSource.transform.position - transform.position);
            //Debug.Log("Heard something..?");
        } else {
            //Debug.Log("Barely heard a thing.");
        }
    }

    //Shorthands
    bool isNewState() {
        return lastState == state;
    }

    bool canSeePlayer() {
        return visCone.GameObjectIntoCone.Contains(player);
    }
    
    void targetPlayer() {
        targetPos = player.transform.position;
        Vector3 targetRotation = player.transform.position - transform.position;
        //targetRotation.y = 0.0f;
        targetRot = Quaternion.LookRotation(targetRotation);
    }

    void targetHome() {
        targetPos = originPos;
        targetRot = originRot;
    }

    Quaternion TargetRotation(Vector3 direction) {
        return Quaternion.LookRotation(direction);
    }

    bool timedOut() {
        return stateTimeout <= 0;
    }
}
