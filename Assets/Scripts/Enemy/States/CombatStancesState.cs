 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
     public class CombatStancesState : State
    {
      public AttackState attackState;
      public PursueTargetState pursueTargetState;
        //  
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
      {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        //Chase thetarget
        //check for attacj range
        if(enemyManager.isPerformingAction)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical",0,0.1f,Time.deltaTime);
            }
        if(enemyManager.currentRecoveryTime <= 0 && enemyManager.distanceFromTarget <= enemyManager.maximumAttackRange)
        {
            return attackState;
        }
        else if(distanceFromTarget > enemyManager.maximumAttackRange){
            return pursueTargetState;
          
        }
        else{
            return this;
        }
        
      }
    }
}
