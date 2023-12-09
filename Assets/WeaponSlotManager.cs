using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponSlotManager : MonoBehaviour
    {
        PlayerManager playerManager;

        public WeaponItem attackingWeapon;
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;
       // WeaponItem currentWeapon;

        Animator animator;

        DamageCollider leftDamageCollider;
        DamageCollider rightDamageCollider;
        PlayerStats playerStats;
        private void Awake()
        {
            //currentWeapon = 
            playerManager = GetComponentInParent<PlayerManager>();
            animator = GetComponent<Animator>();
            playerStats = GetComponentInParent<PlayerStats>();

            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }

            }
        }

        #region Handle Weapon's Damage Collider
        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
            }
            else {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
            }
        }
        public void LoadLeftWeaponDamageCollider()
        {
            leftDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void LoadRightWeaponDamageCollider()
        {
            rightDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }
        public void OpenDamageCollider()
        {
            if(playerManager.isUsingRightHand)
            {
                rightDamageCollider.EnableDamageCollider();
            }
            else
            {
                leftDamageCollider.EnableDamageCollider();
            }
            
        }
 
        public void CloseDamageCollider()
        {
            rightDamageCollider.DisableDamageCollider();
            leftDamageCollider.DisableDamageCollider();
        }
 
    
       #endregion

#region Handle Weapons Stamina Damge
        public void DrainStaminaLightAtack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));

        }
        public void DrainStaminaHeavyAtack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));

        }
#endregion
    
    }
}
