using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class EnemyChase : MonoBehaviour {
        Enemy me;

        private void Awake() {
            me = GetComponent<Enemy>();
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void Chase() {
            if(me.state != me.lastState) {
                me.SwapAgent(EnemyAgents.chaseAgent);
            }

            me.target = Global.Plr.transform;

            me.agent.destination = me.target.position;
        }
    }
}