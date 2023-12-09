 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
     public class AttackState : State
    {

        public CombatStancesState combatStanceState;

        public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttack;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {

          Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
          float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
          float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
           Debug.Log("AttackTarget");
          if(enemyManager.isPerformingAction)
            return combatStanceState;

        // GetNewAttack(enemyManager);
          
          if(currentAttack != null)
          { // si estamos cerca del enemigo, es necesario atacar
              if(distanceFromTarget< currentAttack.minimumDistanceNeededToAttack)
              {
                return this;
              }
              else if (distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
              {
                if( viewableAngle <= currentAttack.maximumAttackAngle &&
                   viewableAngle >= currentAttack.minimumAttackAngle)
                { 
                    if(enemyManager.currentRecoveryTime <= 0 && enemyManager.isPerformingAction == false)
                    {
                      enemyAnimatorManager.anim.SetFloat("Vertical", 0,0.1f, Time.deltaTime);
                      enemyAnimatorManager.anim.SetFloat("Horizontal", 0,0.1f, Time.deltaTime);
                      enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
                      enemyManager.isPerformingAction = true;
                      enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                      currentAttack = null;
                      return combatStanceState;
                    }
                }
                
              }
          }
          else{
            GetNewAttack(enemyManager);
          }
          return combatStanceState;
      }


        private void GetNewAttack(EnemyManager enemyManager)
        {
             
            Vector3 targetsDirection = enemyManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, 
                                                        transform.position);

            int maxScore = 0;

            for (int i=0; i< enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if( distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                &&  distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if(viewableAngle <= enemyAttackAction.maximumAttackAngle &&
                    viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }
             int randomValue = Random.Range(0,maxScore);
            int temporaryScore = 0;
            for (int j =0; j< enemyAttacks.Length; j++)
            {
                EnemyAttackAction enemyAttackAction2 = enemyAttacks[j];

                if(enemyManager.distanceFromTarget <= enemyAttackAction2.maximumDistanceNeededToAttack
                && enemyManager.distanceFromTarget >= enemyAttackAction2.minimumDistanceNeededToAttack)
                {
                    if(viewableAngle <= enemyAttackAction2.maximumAttackAngle 
                    && viewableAngle >= enemyAttackAction2.minimumAttackAngle)
                    {
                        if(currentAttack != null)
                          
                            return;
                        
                        temporaryScore += enemyAttackAction2.attackScore;
                        if (temporaryScore > randomValue)
                        { 
                           currentAttack = enemyAttackAction2;
                         }
                    } 
                }
            
            }  

        }

    }

}
