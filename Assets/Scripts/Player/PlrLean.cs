using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlrLean : MonoBehaviour {
        Player player;

        public string leanInput = "Lean";
        public float leanAxis;

        public float leanAngle = 10f;
        public float leanDistance = 0.8f;
        public float leanSpeed = 0.05f;
        public float leanRotSpeed = 0.05f;

        Vector3 leanPos;
        Vector3 leanRot;

        Vector3 lastUp;

        private void Awake() {
            player = GetComponent<Player>();
            lastUp = transform.up;
        }

        // Start is called before the first frame update
        void Start() {
            leanPos = player.Cam.transform.position + new Vector3(leanDistance, 0f, 0f);
            leanRot = player.Cam.transform.eulerAngles + new Vector3(0f, 0f, leanAngle * -1);
        }

        // Update is called once per frame
        void Update() {
            if(transform.up != lastUp) {
                //player.log("transform.up changed!");
            }

            lastUp = transform.up;


            leanAxis = Input.GetAxis(leanInput);

            player.Cam.transform.localPosition = Vector3.MoveTowards(player.Cam.transform.localPosition, leanPos * leanAxis, leanSpeed);
            player.Cam.transform.eulerAngles = Vector3.RotateTowards(player.Cam.transform.eulerAngles, leanRot * leanAxis, leanRotSpeed, leanSpeed);
        }
    }

}