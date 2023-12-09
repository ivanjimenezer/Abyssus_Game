using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {
    public class IdleState : State
    {
        public LayerMask detectionLayer;
        public CharacterStats currentTarget;
        public PursueTargetState pursueTargetState;

      public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
      {
        
        #region  Handle Enemy Trget DETECION
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i =0; i< colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if(characterStats != null)
                {
                    //CHECK FOR ID
                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if(viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = characterStats;
                        return pursueTargetState;
                    }
                }
            }
            // hANDLE SWITCHING TO NEXT STATE
            if (enemyManager.currentTarget != null )   
            {
                return pursueTargetState;
            }
            else{
                return this;
            }
        #endregion
      }
    }
}
