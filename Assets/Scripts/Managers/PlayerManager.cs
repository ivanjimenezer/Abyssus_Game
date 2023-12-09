using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG{
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        Animator anim;
        CameraHandler cameraHandler;

        [Header("Player Flags")]
        public bool isInteracting; 
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool isUsingRightHand;
        public bool isUsingLeftHand;




        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            
        }

        void Start()
        {
            //
            cameraHandler = CameraHandler.singleton;
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        // Update is called once per frame
        void Update()
        {   
            float delta = Time.deltaTime;  
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            

            isUsingRightHand = anim.GetBool("isUsingRightHand");
            isUsingLeftHand = anim.GetBool("isUsingLeftHand");
            anim.SetBool("isInAir", isInAir);

            inputHandler.TickInput(delta);
           // playerLocomotion.HandleRollingAndSprinting(delta);
            //playerLocomotion.HandleJumping();

        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
           playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleJumping();
            inputHandler.rollFlag = false;
            //inputHandler.sprintFlag  = false; 
            inputHandler.rb_Input  = false;
            inputHandler.rt_Input  = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.jump_Input = false;

        }
        private void LateUpdate()
        {
            
            float delta  = Time.deltaTime;
            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);

            }
            if (isInAir)
            { 
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;

            }
            
        }
    }
}

