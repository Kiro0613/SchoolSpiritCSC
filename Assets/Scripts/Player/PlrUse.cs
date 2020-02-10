using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlrUse : MonoBehaviour {
        public Player player;

        [Range(0f, 10f)]
        public float pickupRange = 6f;
        public string pickupInput = "Pickup";
        bool pickupPressed;

        private void Awake() {
            player = GetComponent<Player>();
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            pickupPressed = Input.GetButtonDown(pickupInput);

            if(pickupPressed) {
                //This detects things on layer 9. Change the bitmask (1 << 9) to change the layer it's active for.
                if(Physics.Raycast(player.Cam.transform.position, player.Cam.transform.forward, out RaycastHit hit, pickupRange, 1 << 9)) {
                    player.log("Used a thing.");
                }
            }
        }
    }
}
