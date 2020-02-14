using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : AIRoutine {
    public void Run(Vector3 target, float turnSpeed = 8f) {
        Run(Routine(target, turnSpeed));
    }

    public IEnumerator Routine(Vector3 target, float turnSpeed = 8f) {
        isRunning = true;

        while(Vector2.Angle(transform.forward.flat(), (target - transform.position).flat()) >= 0.1f) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target - transform.position, transform.up), Time.deltaTime * turnSpeed);

            if(forceBreak) { break; }

            yield return null;
        }

        isRunning = false;
    }
}
