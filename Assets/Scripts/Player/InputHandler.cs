using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG {

    public class InputHandler : MonoBehaviour
    {   
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_Input;
        public bool rb_Input;
        public bool rt_Input;
        public bool jump_Input;
        public bool right_Stick_Right_Input;
        public bool right_Stick_Left_Input;

        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Right;
        public bool d_Pad_Left;


        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag; 
        public bool lockOnFlag;
        public bool lockOnInput;
        public float rollInputTimer;
        public bool isInteracting;
        // Libreria-COmponente para recibir input del juagador
        PlayerControler inputActions;

        Lanzar lanzar;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        CameraHandler cameraHandler;
        AnimatorHandler animatorHandler;

     
        Vector2 movementInput;
        Vector2 cameraInput;

        public void Awake()
        {
            lanzar = GetComponent<Lanzar>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        public void OnEnable()
        {   
            if (inputActions == null)
            {   // se instancia el componente player controler
                inputActions = new PlayerControler(); 

                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                
                inputActions.PlayerActions.RB.performed += i => rb_Input = true;
                inputActions.PlayerActions.RT.performed += i => rt_Input = true;
            
                inputActions.PlayerQuickSlots.DPadRight.performed += i  => d_Pad_Right = true;
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true; 
                inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
                inputActions.PlayerActions.Roll.started += _ => b_Input = true;
                inputActions.PlayerActions.Roll.canceled += _ => b_Input = false;

                inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;
                inputActions.PlayerMovement.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true;


            }
            inputActions.Enable(); 
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }
        public void TickInput(float delta)
        {
            HandleMoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotsInput();
            HandleLockOnInput();
            HandleJumpInput();
        }
        private void HandleMoveInput(float delta)
        {  
            //Debug.Log("movementInput : " + movementInput );

            horizontal = movementInput.x;
           // Debug.Log("Horizontal: " + horizontal);
            vertical = movementInput.y;
           // Debug.Log("vertical: " + vertical);
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
           // Debug.Log("Movesmouny")
           // Debug.Log("MoveAmount: " + moveAmount);

            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
        private void HandleRollInput(float delta)
        {       
           // b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
                // No se refiere a la animacion sino a al control de inputs
           // b_Input = inputActions.PlayerActions.Roll.inProgress; 
            //b_Input.= inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            sprintFlag = b_Input;

            if(b_Input)
            {
                rollInputTimer += delta;
                sprintFlag = true;
                rollFlag = true;
            }
           else 
           {
                if(rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag  = true;

                }
                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            //RIGHT - LIGHT -   ARMAS BLANCAS
            if ( rt_Input)
            {   
                 if(playerManager.isInteracting)
                        return;
                    animatorHandler.anim.SetBool("isUsingLeftHand", true);
                    playerAttacker.HandleLightAttack(playerInventory.leftWeapon);
                
            }
            //LEFT - HEAVY - FUEGO
            if (rb_Input)
            {   Debug.Log("Shhoting<");
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
                  lanzar.Throw();
            }

        }
        
        private void HandleQuickSlotsInput()
        {
            
            //inputActions.PlayerQuickSlots.D_Pad_Right.performed += i => d_Pad_Right = true;
            //inputActions.PlayerQuickSlots.D_Pad_Left.performed += i => d_Pad_Left = true;

            if(d_Pad_Right)
            {   Debug.Log("cambio right");
                playerInventory.ChangeRightWeapon();
            }
            else if(d_Pad_Left)
            {   Debug.Log("cambio left");
                playerInventory.ChangeLeftWeapon();
            }
        }
        

        private void HandleLockOnInput()
        {
            if(lockOnInput && lockOnFlag == false)
            {
                Debug.Log("Entro a HandleLockoNiNPUT");
                //cameraHandler.ClearLockOnTargets();
                lockOnInput = false;
                cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                cameraHandler.HandleLockOn();
                if(cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag =true;
                }

            }
            else if(lockOnInput && lockOnFlag)
            {   Debug.Log("lockOnInput && lockOnFlag)");
                lockOnInput = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();
            }
            if(lockOnFlag && right_Stick_Left_Input)
            {
                right_Stick_Left_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.leftLockTarget != null)  
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
                } 
            }
            if(lockOnFlag && right_Stick_Right_Input)
            {
                right_Stick_Right_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.rightLockTarget != null)  
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
                }
            }
            cameraHandler.SetCameraHeight();


        }
    
        public void HandleJumpInput()
        {
            inputActions.PlayerActions.Jump.performed += i => jump_Input   = true;
        }
    
    }
 
}

