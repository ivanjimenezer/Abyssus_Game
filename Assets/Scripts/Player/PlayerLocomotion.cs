using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
    public class PlayerLocomotion : MonoBehaviour
    {   
        CameraHandler cameraHandler;
        PlayerManager playerManager;
        public PlayerStats playerStats;

        public AnimatorHandler animatorHandler;
        Transform cameraObject;
        InputHandler inputHandler;
        public Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        

        [Header("Ground & Air detection stats")]
        [SerializeField]
        // Para detectar el suelo
        float groundDetectionRayStartPoint = 0.51f;
        [SerializeField]
        // distancia minima para comenzar la animacion de caida
        float minimumDistanceNeededToBeginFall = 1f;
        [SerializeField]
        // un offset de cuando comenzara a activarse el ray
        float groundDirectionRayDistance = 0.2f;
        LayerMask ignoreForGroundCheck;
        public float inAirTimer;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Movement Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float walkingSpeed = 1;
        [SerializeField]
        float sprintSpeed = 8;
        [SerializeField]
        float rotationSpeed = 10; 
        [SerializeField]
        float fallingSpeed= 45;
        [SerializeField]
        float jumpForce = 10; // Adjust this value to control the jump height
        [SerializeField]
        float jumpHorizontalForce = 5; // Adjust this value to control the jump width
        [Header("Teleport")]
        [SerializeField]
        private float teleportForwardDistance = 5f;

        [SerializeField]
        private float teleportUpwardDistance = 2f;

        [SerializeField]
        float pushForceFall =  20f;

        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockerCollider;



        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
        }

        // Start is called before the first frame update
        void Start()
        {   playerManager = GetComponent<PlayerManager>();
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();

            playerManager.isGrounded = true;
            ignoreForGroundCheck = ~(1 << 9 | 1 << 11);
            Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true);
            
        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float delta)
        {   
            if(inputHandler.lockOnFlag)
            {
                if(inputHandler.lockOnFlag && inputHandler.sprintFlag == false)
                {
                    Vector3 targetDirection = Vector3.zero;
                    targetDirection = cameraHandler.cameraTransform.forward * inputHandler.vertical;
                    targetDirection += cameraHandler.cameraTransform.right * inputHandler.horizontal;

                    targetDirection.Normalize();
                    targetDirection.y = 0;

                    if(targetDirection == Vector3.zero)
                    {
                        targetDirection = transform.forward;
                    }
                    Quaternion tr = Quaternion.LookRotation(targetDirection);
                    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);
                    
                    transform.rotation = targetRotation;

                }
                else
                {
                    Vector3 rotationDirection = moveDirection;
                    rotationDirection = cameraHandler.currentLockOnTarget.position - transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);
                    transform.rotation = targetRotation;
                }
            }
            else{
                Vector3 targetDir = Vector3.zero;
                float moveOverride = inputHandler.moveAmount;

                targetDir = cameraObject.forward * inputHandler.vertical;
                targetDir += cameraObject.right * inputHandler.horizontal;
                targetDir.Normalize();
                targetDir.y = 0;
                if(targetDir == Vector3.zero)
                {
                    targetDir = myTransform.forward;
                }
                float rs = rotationSpeed;
                Quaternion tr = Quaternion.LookRotation(targetDir);
                Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs* delta);

                myTransform.rotation = targetRotation;
            }
        }
        public void HandleMovement(float delta)
        {
            if(inputHandler.rollFlag)
            {   return;
            }
            
            if(playerManager.isInteracting)
            {
                return;
            }
                

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = movementSpeed;

            if(inputHandler.sprintFlag && inputHandler.moveAmount > 0.5)
            {
                speed = sprintSpeed;
                playerManager.isSprinting=true;
                moveDirection *= speed;   
            }
            else
            {
                if(inputHandler.moveAmount > 0.5)
                {
                    moveDirection *= walkingSpeed;
                    playerManager.isSprinting = false;
                }
                else{
                    moveDirection *= speed; 
                    playerManager.isSprinting = false;
                }
            }
        /*Added code
                // Apply jump force
            if (inputHandler.jump_Input && playerManager.isGrounded)
            {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                // Apply additional horizontal force for jump width
                Vector3 horizontalForce = (cameraObject.forward * inputHandler.vertical + cameraObject.right * inputHandler.horizontal).normalized;
                rigidbody.AddForce(horizontalForce * jumpHorizontalForce, ForceMode.Impulse);

                animatorHandler.PlayTargetAnimation("Jump", true);
            }
        */

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            if(inputHandler.lockOnFlag && inputHandler.sprintFlag == false)
            {
                animatorHandler.UpdateAnimatorValues(inputHandler.vertical, inputHandler.horizontal, playerManager.isSprinting);

            }
            else{
                animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount,0, playerManager.isSprinting);

            }
            
            if (animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }
        }
        
        public void HandleRollingAndSprinting(float delta)
        {
            if(animatorHandler.anim.GetBool("isInteracting"))
                return;

            if(inputHandler.rollFlag)
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection  += cameraObject.right * inputHandler.horizontal;

                if (inputHandler.moveAmount > 0)
                {
                    animatorHandler.PlayTargetAnimation("CoolRolling", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                }
                else{
                    animatorHandler.PlayTargetAnimation("RunBackFunny", true);
                }
            }
        }
        
        public void HandleFalling(float delta, Vector3 moveDirection)
        {
            playerManager.isGrounded = false;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y +=groundDetectionRayStartPoint;

            if(Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
            {
                moveDirection = Vector3.zero;
            }

            if(playerManager.isInAir)
            {   
                rigidbody.AddForce(-Vector3.up * fallingSpeed);
                //Te empujara hacia abajo
                rigidbody.AddForce(moveDirection * fallingSpeed / pushForceFall);
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groundDirectionRayDistance;

            targetPosition = myTransform.position;

            Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);



            if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
            {
                normalVector = hit.normal;
                Vector3 tp = hit.point;
                playerManager.isGrounded = true;
                targetPosition.y = tp.y;

                if(playerManager.isInAir)
                {
                    if(inAirTimer > 0.5f)
                    {
                        Debug.Log("You were in the air for :"+inAirTimer);
                        animatorHandler.PlayTargetAnimation("Landing", true);
                        inAirTimer = 0;
                    }
                    else
                    {
                        animatorHandler.PlayTargetAnimation("Empty", false); 
                        inAirTimer = 0;
                    }
                    playerManager.isInAir = false;
                }
            }
             else {
                if(playerManager.isGrounded)
                {
                    playerManager.isGrounded = false;
                }

                if(playerManager.isInAir == false)
                {
                    if(playerManager.isInteracting == false)
                    {
                        animatorHandler.PlayTargetAnimation("Falling", true);
                    }
                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (movementSpeed/2);
                    playerManager.isInAir = true;
                }
            }
            if(playerManager.isGrounded)
            {
                if(playerManager.isInteracting || inputHandler.moveAmount > 0)
                
                {
                    myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
                }
                else
                {
                    myTransform.position = targetPosition;
                }
            }
            

        }

        #endregion
public void HandleJumping()
{
    if (playerManager.isInteracting)
    {
        return;
    }

    /*
    if (inputHandler.jump_Input && playerManager.isGrounded)
    {
        // Apply an impulse force in the upward direction for jump height
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Apply additional horizontal force for jump width
        Vector3 horizontalForce = (cameraObject.forward * inputHandler.vertical + cameraObject.right * inputHandler.horizontal).normalized;
        rigidbody.AddForce(horizontalForce * jumpHorizontalForce, ForceMode.Impulse);

        animatorHandler.PlayTargetAnimation("Jump", true);
    }*/
        if (inputHandler.jump_Input && playerManager.isGrounded)
    {
        // Teleport a few units above and in front of the player
       Vector3 teleportOffset = myTransform.forward * teleportForwardDistance + Vector3.up * teleportUpwardDistance;
        Vector3 teleportPosition = myTransform.position + teleportOffset;

        // Set the player's position to the teleport position
        myTransform.position = teleportPosition;

        // Optionally, you can also play a teleportation animation here if needed

        // Clear any existing velocity to prevent additional movement
        rigidbody.velocity = Vector3.zero;
        playerStats.TakeStaminaDamage(20);
        // Update the animator with the new position
        animatorHandler.UpdateAnimatorValues(0, 0, false);

        // Play the jump animation
        animatorHandler.PlayTargetAnimation("Jump", true);
    }
}


        float CalculateJumpVerticalSpeed()
        {
            // You can adjust this value to control the jump height
            return Mathf.Sqrt(2 * fallingSpeed * Mathf.Abs(Physics.gravity.y));
        }

    }
}