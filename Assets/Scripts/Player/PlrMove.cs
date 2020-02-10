using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public enum PlayerStates {
        Idle,
        Walking,
        Running,
        Jumping,
        Aerial
    }

    public class PlrMove : MonoBehaviour {
        public PlayerStates state;

        [Header("Inputs")]
        public string HorizontalInput = "Horizontal";
        public string VerticalInput = "Vertical";
        public string RunInput = "Run";
        public string JumpInput = "Jump";
        public string CrouchInput = "Crouch";

        [Header("Input Axes")]
        public float hInput;
        public float vInput;
        public bool runPressed;
        public bool jumpPressed;
        public bool crouchPressed;

        //Things ranged 0f - 1f are percentages used to scale their corresponding value
        //(eg, walkAccel of 0.1 means the player accelerates 10% of the walkMaxSpeed value every update)
        [Header("Player Motor")]
        [Range(0f, 15f)]
        public float walkMaxSpeed = 4f;
        [Range(0f, 1f)]
        public float walkAccel = 0.1f;
        [Range(0f, 15f)]
        public float runMaxSpeed = 8f;
        [Range(0f, 1f)]
        public float runAccel = 0.2f;
        [Range(0f, 1f)]
        public float groundResistance = 0.33f;  //How fast your speed decays when not pushing a direction
        [Range(0f, 40f)]
        public float jumpForce = 8f;
        [Range(0f, 1f)]
        public float reverseSpeed = 0.5f;  //How fast you can change your direction (eg left to right)

        [Range(0f, 15f)]
        public float airMaxSpeed = 1f; //The max speed the player can push itself in the air
        [Range(0f, 1f)]
        public float airAccel = 0.8f;
        [Range(0f, 1f)]
        public float airResistance = 0.1f;  //How fast your speed decays when flying through the air (1 = instant stop, 0 = no wind resistance)
        [Range(0f, 1f)]
        public float airReverseSpeed = 0.05f;  //How fast you can change your direction in air (eg left to right)

        [Range(0.001f, 1f)]
        public float crouchHeight;
        float standingHeight;
        public float crouchSpeed = 0.3f;

        public float gravity = 20f;

        public Vector3 inputVector;
        public Vector3 moveVector;
        float jumpVector;

        public bool grounded;

        CharacterController charControl;

        private void Awake() {
            charControl = GetComponent<CharacterController>();
            standingHeight = transform.localScale.y;
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            //Grounded is just so you can check in the inspector if the player is grounded.
            grounded = charControl.isGrounded;
            HandleInput();
            HandleState();

            Crouch();
            charControl.Move(UpdateMoveVector() * Time.deltaTime);
        }

        Vector3 UpdateMoveVector() {
            Vector3 newMove = Vector3.zero;

            //Set variables based on if the char is in the air or not. This means the same code can be used for aerial and ground movement.
            float accel = !charControl.isGrounded ? airAccel : runPressed ? runAccel : walkAccel;
            float maxSpeed = !charControl.isGrounded ? airMaxSpeed : runPressed ? runMaxSpeed : walkMaxSpeed;
            float speedDecay = charControl.isGrounded ? groundResistance : airResistance;
            float reverseScalar = charControl.isGrounded ? reverseSpeed : airReverseSpeed;

            moveVector = Vector3.MoveTowards(moveVector, inputVector * maxSpeed, maxSpeed * accel);

            //jumpVector is separate from moveVector because inputVector.y is only 1 for one frame, so MoveTowards will always scale it back to 0 instantly.
            if(inputVector.y == 1) {
                jumpVector = jumpForce;
            }
            //Gravity has to be applied every frame or player constantly switches between grounded and not grounded.
            jumpVector -= Time.deltaTime * gravity;

            //If moving one direction but pushing another, decay the speed in the opposite direction very quickly.
            //This makes controls more responsive by allowing sudden changes in direction.
            if((inputVector.x > 0 && moveVector.x < 0) || (inputVector.x < 0 && moveVector.x > 0)) {
                moveVector.x *=  1 - reverseScalar;
            }
            //Same as above
            if((inputVector.z > 0 && moveVector.z < 0) || (inputVector.z < 0 && moveVector.z > 0)) {
                moveVector.z *= 1 - reverseScalar;
            }

            //Decay speed when not pushing an input button
            if(charControl.isGrounded) {
                if(inputVector.x == 0) { moveVector.x *= 1 - speedDecay; }
                if(inputVector.z == 0) { moveVector.z *= 1 - speedDecay; }
            }
            
            newMove += transform.right * moveVector.x;
            newMove += transform.up * jumpVector;
            newMove += transform.forward * moveVector.z;
            return newMove;
        }

        void Crouch() {
            Vector3 newHeight = transform.localScale;

            newHeight.y = crouchPressed ? crouchHeight : standingHeight;

            transform.localScale = Vector3.MoveTowards(transform.localScale, newHeight, crouchSpeed);
        }

        void HandleInput() {
            inputVector.x = Input.GetAxisRaw(HorizontalInput);
            inputVector.z = Input.GetAxisRaw(VerticalInput);

            if(!charControl.isGrounded) {
                inputVector.y = -1;
            } else if(Input.GetButtonDown("Jump")) {
                inputVector.y = 1;
            } else {
                inputVector.y = 0;
            }

            runPressed = Input.GetButton(RunInput);
            jumpPressed = Input.GetButtonDown(JumpInput);
            crouchPressed = Input.GetButton(CrouchInput);
        }

        void HandleState() {
            if(charControl.isGrounded) {
                if(inputVector.x != 0 || inputVector.z != 0) {
                    if(runPressed) {
                        state = PlayerStates.Running;
                    } else {
                        state = PlayerStates.Walking;
                    }
                } else {
                    state = PlayerStates.Idle;
                }
            } else {
                state = PlayerStates.Aerial;
            }
        }
    }
}
