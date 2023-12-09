using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;
        public  NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidBody;
        public CharacterStats currentTarget;
        public LayerMask detectionLayer;
        
        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockerCollider;


        public float distanceFromTarget;
        public float stoppingDistance = 5f;
        public float rotationSpeed = 15;

        private void Awake()
        {   
            
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            
            enemyRigidBody = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true);
        }



        public void HandleMoveToTarget()
        {

        }


    }
}
