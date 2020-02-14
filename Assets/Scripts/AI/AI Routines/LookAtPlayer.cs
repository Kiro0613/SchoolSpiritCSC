using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : AIRoutine {
    Enemy.Enemy me;

    private void Awake() {
        me = GetComponent<Enemy.Enemy>();
    }

    public void Run(float turnSpeed = 8f) {
        Run(Routine(turnSpeed));
    }

    public IEnumerator Routine(float turnSpeed = 8f) {
        isRunning = true;

        if(me.seesPlayer == false) {
            Debug.Log("LookAtPlayer routine quit immediately; cannot see player.");
        }

        while(me.seesPlayer) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Global.Plr.transform.position - transform.position, transform.up), Time.deltaTime * turnSpeed);

            if(forceBreak) { break; }

            yield return null;
        }
        
        isRunning = false;
    }
}
