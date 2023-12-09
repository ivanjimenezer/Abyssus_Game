using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class EnemyManager : CharacterManager
    {
        // Start is called before the first frame update
        EnemyLocomotionManager enemyLocomotionManager;
        public EnemyAnimatorManager enemyAnimationManager;
        EnemyStats enemyStats;

       public State currentState;
        

         public CharacterStats currentTarget;
        public  NavMeshAgent navMeshAgent; 
        public Rigidbody enemyRigidBody;

       
        public bool isPerformingAction;
        public bool isInteracting;
        public float distanceFromTarget;
        public float stoppingDistance = 5f;
        public float rotationSpeed = 15;
        public float maximumAttackRange = 1.5f;
        
        /* EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttack; 
        */ 
        [Header("A.I. Settings")]
        public float detectionRadius = 20;
        // menor o mayor eel valor de estos angulos dependera el campo de vision de detections 
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;
        public float viewableAngle;

        public float currentRecoveryTime = 0;

        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyRigidBody =GetComponent<Rigidbody>();
            enemyStats = GetComponent<EnemyStats>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>(); 
            navMeshAgent.enabled = false;
        }
        private void Update()
        {
            
            HandleRecoveryTimer();
            // OnDrawGizmosSelected();
           //enemyLocomotionManager.NA
           isInteracting = enemyAnimationManager.anim.GetBool("isInteracting");
        }
                private void Start()
        {
            //navMeshAgent.enabled = false;
            enemyRigidBody.isKinematic = false;
        }
        private void FixedUpdate()
        { 
            HandleStateMachine();
        }

        private void HandleStateMachine()
        {
            /*
            if(enemyLocomotionManager.currentTarget != null)
            {
                enemyLocomotionManager.distanceFromTarget = 
                Vector3.Distance(enemyLocomotionManager.currentTarget.transform.position,transform.position);
            }
            if(enemyLocomotionManager.currentTarget == null)
            {
                enemyLocomotionManager.HandleDetection();
            }
            else if(enemyLocomotionManager.distanceFromTarget > enemyLocomotionManager.stoppingDistance)
            {
                enemyLocomotionManager.HandleMoveToTarget();
            }
            else if(enemyLocomotionManager.distanceFromTarget <= enemyLocomotionManager.stoppingDistance)
            {
                AttackTarget();
            }
            Debug.Log("Pasa de alto");
            */
            if(currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimationManager);
                if(nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }

        }
        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

 
        #region  Attacks
        private void HandleRecoveryTimer()
        {
            if(currentRecoveryTime >0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }
            if(isPerformingAction)
            {
                if(currentRecoveryTime<= 0)
                {
                    isPerformingAction= false;
                }
            }
        }

    }
   #endregion

}

