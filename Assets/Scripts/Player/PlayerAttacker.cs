using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        public string lastAttack; 
        public WeaponSlotManager weaponSlotManager;
        string[] LightAttacks_List= {"HeavyAttack_OneHand_02","SideAttack_OneHand_01", "OH_Heavy_Attack_01" };
        string[] HeavyAttacks_List= { "CoolSword"};
        string shoot = "CoolShooting";

        private void Awake()
        { 
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputHandler = GetComponent<InputHandler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {    
            if(inputHandler.comboFlag)
            {// desactiva el comboflag
                animatorHandler.anim.SetBool("canDoCombo", false);
                if(lastAttack == "SideAttack_OneHand_01" || lastAttack == "HeavyAttack_OneHand_02" )
                {
                    animatorHandler.PlayTargetAnimation("OH_Heavy_Attack_01",true);
                }
            
            }
            //COMBO 1
            //      LIGHT                   HEAVY
            // SideAttack_OneHand_01 + OH_Heavy_Attack_01  
            //COMBO 2
            //      LIGHT                   HEAVY
            // HeavyAttack_OneHand_02 + OH_Heavy_Attack_01 
          //  if(lastAttack ==weapon.)
        }


        public void HandleLightAttack(WeaponItem weapon)
        { // Ataque elegido al azar
            weaponSlotManager.attackingWeapon = weapon;
            string randomAttackAnimation = LightAttacks_List[Random.Range(0, LightAttacks_List.Length)];
             Debug.Log(randomAttackAnimation); 
            
            animatorHandler.PlayTargetAnimation(randomAttackAnimation, true);
            lastAttack = randomAttackAnimation;

        }


        public void HandleHeavyAttack(WeaponItem weapon)
        {   
           // weaponSlotManager.attackingWeapon = weapon;
            //weapon.HeavyAttacks_List;
           // string randomAttackAnimation = HeavyAttacks_List[Random.Range(0, HeavyAttacks_List.Length)];
            Debug.Log("-----------------------------------------------------");
            //Debug.Log(randomAttackAnimation);
            animatorHandler.PlayTargetAnimation(shoot, true);
          //  lastAttack = randomAttackAnimation;
            //weapon.HeavyAttacks_List;
            //animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
        }
    
    }
}





 
